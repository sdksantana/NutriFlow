using Microsoft.EntityFrameworkCore;
using NutriFlow.Domain.Enums;
using NutriFlow.Domain.Queries.Interfaces;
using NutriFlow.Domain.Queries.Requests;
using NutriFlow.Domain.Queries.Responses;
using NutriFlow.Domain.Queries.Responses.Patient;

namespace NutriFlow.Infrastructure.Queries
{
    public class PatientQuery : IPatientQuery
    {
        private readonly NutriFlowDbContext _context;

        public PatientQuery(NutriFlowDbContext context)
        {
            _context = context;
        }

        public async Task<BodyMeasurementResponse?> GetBodyMeasurementByRecentDateAsync(Guid patientId)
        {
            var result = await _context.BodyMeasurements
                .Where(bm => bm.PatientId == patientId)
                .OrderByDescending(bm => bm.CreationTime)
                .Select(bm => new BodyMeasurementResponse
                {
                    Age = bm.Age,
                    Height = bm.Height,
                    Weight = bm.Weight,
                    Neck = bm.Neck,
                    Chest = bm.Chest,
                    Waist = bm.Waist,
                    Abdomen = bm.Abdomen,
                    Hip = bm.Hip,
                    RightArm = bm.RightArm,
                    LeftArm = bm.LeftArm,
                    RightForearm = bm.RightForearm,
                    LeftForearm = bm.LeftForearm,
                    RightThigh = bm.RightThigh,
                    LeftThigh = bm.LeftThigh,
                    RightCalf = bm.RightCalf,
                    LeftCalf = bm.LeftCalf
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<BodyMeasurementHistoricResponse?>> GetBodyMeasurementHistoricAsync(Guid patientId)
        {
            var query = await _context.BodyMeasurements
                .Where(bm => bm.PatientId == patientId)
                .OrderBy(x => x.CreationTime)
                .ToListAsync();

            if (query.Count < 2)
                return null;

            var initial = query.First();
            var current = query.Last();

            var response = new List<BodyMeasurementHistoricResponse?>
            {
                CreateMeasurement("Peso", initial.Weight, current.Weight),
                CreateMeasurement("Circuferência do Pescoço", initial.Neck, current.Neck),
                CreateMeasurement("Circuferência do Torax", initial.Chest, current.Chest),
                CreateMeasurement("Cintura", initial.Waist, current.Waist),
                CreateMeasurement("Circuferência Abdômen", initial.Abdomen, current.Abdomen),
                CreateMeasurement("Circuferência Quadril", initial.Hip, current.Hip),
                CreateMeasurement("Circuferência Braço Direito", initial.RightArm, current.RightArm),
                CreateMeasurement("Circuferência Braço Esquerdo", initial.LeftArm, current.LeftArm),
                CreateMeasurement("Circuferência Antebraço Direito", initial.RightForearm, current.RightForearm),
                CreateMeasurement("Circuferência Antebraço Esquerdo", initial.LeftForearm, current.LeftForearm),
                CreateMeasurement("Circuferência Coxa Direita", initial.RightThigh, current.RightThigh),
                CreateMeasurement("Circuferência Coxa Esquerda", initial.LeftThigh, current.LeftThigh),
                CreateMeasurement("Circuferência Panturrilha Direita", initial.RightCalf, current.RightCalf),
                CreateMeasurement("Circuferência Panturrilha Esquerda", initial.LeftCalf, current.LeftCalf)
            };

            return response.Where(x => x != null).ToList()!;
        }

        private BodyMeasurementHistoricResponse? CreateMeasurement(string label, decimal? initial, decimal? current)
        {
            if (initial == null && current == null)
                return null;

            var safeInitial = initial ?? 0;
            var safeCurrent = current ?? 0;
            var diff = safeCurrent - safeInitial;

            return new BodyMeasurementHistoricResponse
            {
                Label = label,
                Initial = safeInitial,
                Current = safeCurrent,
                Diff = diff
            };
        }

        public async Task<PatientResponse?> GetPatientByIdAsync(Guid Id)
        {
            var patient = await _context.Patients
                .Where(p => p.Id == Id)
                .Select(p => new PatientResponse
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email.Adress.ToString(),
                }).FirstOrDefaultAsync();

            return patient;
        }

        public async Task<int?> GetTmbAsync(Guid patientId)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == patientId);

            if (patient == null)
                return null;

            var measurement = await _context.BodyMeasurements
                  .Where(m => m.PatientId == patientId)
                  .OrderByDescending(m => m.CreationTime)
                  .FirstOrDefaultAsync();

            if (measurement == null)
                return null;

            return CalculateTMB(
                (int)measurement.Age,
                (decimal)measurement.Height,
                (decimal)measurement.Weight,
                patient.Gender
            );
        }

        private int CalculateTMB(decimal age, decimal height, decimal weight, EGender gender)
        {
            decimal tmb = 0;

            if (gender == EGender.MALE)
            {
                tmb = 66.5m + (13.75m * weight) + (5.003m * height) - (6.75m * age);
            }
            else if (gender == EGender.FEMALE)
            {
                tmb = 655.1m + (9.563m * weight) + (1.850m * height) - (4.676m * age);
            }

            return (int)Math.Round(tmb);
        }

        public async Task<DashboardResponse> GetDashBoardAsync(Guid patientId)
        {
            var totalMeals = await _context.PatientRecipes.CountAsync(x => x.PatientId == patientId);

            var dashboard = await (
                from p in _context.Patients
                join bm in _context.BodyMeasurements on p.Id equals bm.PatientId
                where p.Id == patientId
                orderby bm.CreationTime descending
                join np in _context.NutritionistsPatients.Where(n => n.IsActive)
                on p.Id equals np.PatientId into npLeft
                from np in npLeft.DefaultIfEmpty()
                select new
                {
                    p.FirstName,
                    bm.Weight,
                    NutritionistName = np.Nutritionist != null ? np.Nutritionist.FirstName + " " + np.Nutritionist.LastName : null,
                    Email = np.Nutritionist != null ? np.Nutritionist.Email.Adress : null,
                    Crn = np.Nutritionist != null ? np.Nutritionist.Crn.Value : null,
                    StartDate = np.StartDate.ToLocalTime().Date.ToString("dd/MM/yyyy")
                }
            ).FirstOrDefaultAsync();

            return new DashboardResponse
            {
                FirstName = dashboard?.FirstName,
                LastWeightRegistered = dashboard?.Weight ?? 0,
                TotalMealsRegistered = totalMeals,
                Nutritionist = dashboard?.NutritionistName,
                NutritionistEmail = dashboard?.Email,
                NutritionistCrn = dashboard?.Crn,
                NutritionistLinkStartDate = dashboard?.StartDate
            };
        }

        public async Task<RecipeResponse?> GetRecipeAsync(Guid id, Guid patientId, GetRecipeRequest request)
        {
            var query = _context.PatientRecipes
            .Where(r => r.Id == id && r.PatientId == patientId);

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(r => r.Name.ToLower().Contains(request.Name.ToLower()));

            if (!string.IsNullOrWhiteSpace(request.Calories))
                query = query.Where(r => r.Calories == request.Calories);

            if (!string.IsNullOrWhiteSpace(request.Protein))
                query = query.Where(r => r.Protein == request.Protein);

            if (!string.IsNullOrWhiteSpace(request.Fat))
                query = query.Where(r => r.Fat == request.Fat);

            if (!string.IsNullOrWhiteSpace(request.Carbs))
                query = query.Where(r => r.Carbs == request.Carbs);

            if (!string.IsNullOrWhiteSpace(request.PreparationTime))
                query = query.Where(r => r.PreparationTime == request.PreparationTime);

            if (!string.IsNullOrWhiteSpace(request.Meal))
                query = query.Where(r => r.Meal == request.Meal);

            var recipe = await query
                .OrderByDescending(r => r.CreationTime)
                .Select(r => new RecipeResponse
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    PreparationTime = r.PreparationTime,
                    Calories = r.Calories,
                    Protein = r.Protein,
                    Fat = r.Fat,
                    Carbs = r.Carbs,
                    Meal = r.Meal
                }).FirstOrDefaultAsync();

            return recipe;
        }

        public async Task<IEnumerable<RecipeResponse>> GetRecipesAsync(Guid patientId, GetRecipeRequest request)
        {
            var query = _context.PatientRecipes
                .Where(r => r.PatientId == patientId);

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(r => r.Name.ToLower().Contains(request.Name.ToLower()));

            if (!string.IsNullOrWhiteSpace(request.Calories))
                query = query.Where(r => r.Calories == request.Calories);

            if (!string.IsNullOrWhiteSpace(request.Protein))
                query = query.Where(r => r.Protein == request.Protein);

            if (!string.IsNullOrWhiteSpace(request.Fat))
                query = query.Where(r => r.Fat == request.Fat);

            if (!string.IsNullOrWhiteSpace(request.Carbs))
                query = query.Where(r => r.Carbs == request.Carbs);

            if (!string.IsNullOrWhiteSpace(request.PreparationTime))
                query = query.Where(r => r.PreparationTime == request.PreparationTime);

            if (!string.IsNullOrWhiteSpace(request.Meal))
                query = query.Where(r => r.Meal == request.Meal);

            var recipes = await query
                .Select(x => new RecipeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PreparationTime = x.PreparationTime,
                    Calories = x.Calories,
                    Protein = x.Protein,
                    Fat = x.Fat,
                    Carbs = x.Carbs,
                    Meal = x.Meal
                }).ToListAsync();

            return recipes;
        }

        public async Task<List<BodyMeasurementEvolutionResponse>> GetBodyMeasurementEvolutionAsync(Guid patientId)
        {
            var query = await _context.BodyMeasurements
                .Where(bm => bm.PatientId == patientId)
                .OrderBy(bm => bm.CreationTime)
                .Select(bm => new BodyMeasurementEvolutionResponse
                {
                    Date = bm.CreationTime.Date,
                    Weight = bm.Weight,
                    Neck = bm.Neck,
                    Chest = bm.Chest,
                    Waist = bm.Waist,
                    Abdomen = bm.Abdomen,
                    Hip = bm.Hip,
                    RightArm = bm.RightArm,
                    LeftArm = bm.LeftArm,
                    RightForearm = bm.RightForearm,
                    LeftForearm = bm.LeftForearm,
                    RightThigh = bm.RightThigh,
                    LeftThigh = bm.LeftThigh,
                    RightCalf = bm.RightCalf,
                    LeftCalf = bm.LeftCalf
                }).ToListAsync();

            return query;
        }

        public async Task<PatientResponse?> GetPatientByEmailAsync(string email)
        {
            var patient = await _context.Patients
               .Where(p => p.Email.ToString().ToLower().Contains(email.ToLower()))
               .Select(p => new PatientResponse
               {
                   Id = p.Id,
                   FirstName = p.FirstName,
                   LastName = p.LastName,
                   Email = p.Email.Adress.ToString(),
               }).FirstOrDefaultAsync();

            return patient;
        }

        public async Task<IEnumerable<RecipeResponse?>> GetPlanRecipesAsync(Guid patientId, GetRecipeRequest request)
        {
            var query = _context.NutritionistPatientRecipes
                    .Include(n => n.NutritionistRecipe)
                    .Where(np => np.PatientId == patientId && np.NutritionistRecipe != null)
                    .Select(n => n.NutritionistRecipe)
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(n => n.Name.ToLower().Contains(request.Name.ToLower()));

            if (!string.IsNullOrWhiteSpace(request.Calories))
                query = query.Where(r => r.Calories == request.Calories);

            if (!string.IsNullOrWhiteSpace(request.Protein))
                query = query.Where(r => r.Protein == request.Protein);

            if (!string.IsNullOrWhiteSpace(request.Fat))
                query = query.Where(r => r.Fat == request.Fat);

            if (!string.IsNullOrWhiteSpace(request.Carbs))
                query = query.Where(r => r.Carbs == request.Carbs);

            if (!string.IsNullOrWhiteSpace(request.PreparationTime))
                query = query.Where(r => r.PreparationTime == request.PreparationTime);

            if (!string.IsNullOrWhiteSpace(request.Meal))
                query = query.Where(r => r.Meal == request.Meal);

            var recipes = await query
                .Select(x => new RecipeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PreparationTime = x.PreparationTime,
                    Calories = x.Calories,
                    Protein = x.Protein,
                    Fat = x.Fat,
                    Carbs = x.Carbs,
                    Meal = x.Meal
                }).ToListAsync();

            return recipes;
        }

        public async Task<RecipeResponse?> GetPlanRecipesByIdAsync(Guid id, Guid patientId, GetRecipeRequest request)
        {
            var query = _context.NutritionistPatientRecipes
                    .Include(n => n.NutritionistRecipe)
                    .Where(np => np.PatientId == patientId && np.NutritionistRecipe.Id == id)
                    .Select(n => n.NutritionistRecipe)
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(n => n.Name.ToLower().Contains(request.Name.ToLower()));

            if (!string.IsNullOrWhiteSpace(request.Calories))
                query = query.Where(r => r.Calories == request.Calories);

            if (!string.IsNullOrWhiteSpace(request.Protein))
                query = query.Where(r => r.Protein == request.Protein);

            if (!string.IsNullOrWhiteSpace(request.Fat))
                query = query.Where(r => r.Fat == request.Fat);

            if (!string.IsNullOrWhiteSpace(request.Carbs))
                query = query.Where(r => r.Carbs == request.Carbs);

            if (!string.IsNullOrWhiteSpace(request.PreparationTime))
                query = query.Where(r => r.PreparationTime == request.PreparationTime);

            if (!string.IsNullOrWhiteSpace(request.Meal))
                query = query.Where(r => r.Meal == request.Meal);

            var recipes = await query
                .Select(x => new RecipeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PreparationTime = x.PreparationTime,
                    Calories = x.Calories,
                    Protein = x.Protein,
                    Fat = x.Fat,
                    Carbs = x.Carbs,
                    Meal = x.Meal
                }).FirstOrDefaultAsync();

            return recipes;
        }
    }
}
