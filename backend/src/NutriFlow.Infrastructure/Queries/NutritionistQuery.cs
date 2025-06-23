using Microsoft.EntityFrameworkCore;
using NutriFlow.Domain.Queries.Interfaces;
using NutriFlow.Domain.Queries.Requests;
using NutriFlow.Domain.Queries.Responses.Nutritionist;
using NutriFlow.Domain.Queries.Responses.NutritionistPatient;

namespace NutriFlow.Infrastructure.Queries
{
    public class NutritionistQuery : INutritionistQuery
    {
        private readonly NutriFlowDbContext _context;

        public NutritionistQuery(NutriFlowDbContext context)
        {
            _context = context;
        }

        public async Task<NutritionistDashboardResponse> GetDashBoardAsync(Guid nutritionistId)
        {
            var totalLinkedPatients = await _context.NutritionistsPatients
            .CountAsync(x => x.NutritionistId == nutritionistId && x.IsActive == true);

            var totalMealsRegistered = await _context.NutritionistRecipes.CountAsync(x => x.NutritionistId == nutritionistId);

            var bodyMeasurements = await (
             from np in _context.NutritionistsPatients
             where np.NutritionistId == nutritionistId && np.IsActive
             join bm in _context.BodyMeasurements on np.PatientId equals bm.PatientId
             join p in _context.Patients on np.PatientId equals p.Id
             select new
             {
                 PatientId = p.Id,
                 FirstName = p.FirstName,
                 LastName = p.LastName,
                 Email = p.Email.Adress,
                 Gender = p.Gender,
                 StartDate = np.StartDate
             }).ToListAsync();

            var topPatientInfo = bodyMeasurements
                .GroupBy(x => new { x.PatientId, x.FirstName, x.LastName, x.Email, x.Gender, x.StartDate })
                .OrderByDescending(g => g.Count())
                .Select(g => new
                {
                    FullName = g.Key.FirstName + " " + g.Key.LastName,
                    Email = g.Key.Email,
                    Gender = g.Key.Gender,
                    StartDate = g.Key.StartDate
                })
                .FirstOrDefault();

            var firstName = await _context.Nutritionists
                .Where(n => n.Id == nutritionistId)
                .Select(n => n.FirstName)
                .FirstOrDefaultAsync();

            return new NutritionistDashboardResponse
            {
                FirstName = firstName,
                TopUpdatedPatient = topPatientInfo?.FullName,
                PatientEmail = topPatientInfo?.Email,
                PatientGender = topPatientInfo?.Gender.ToString(),
                PatientLinkStartDate = topPatientInfo?.StartDate.ToLocalTime().ToString("dd/MM/yyyy"),
                TotalLinkedPatients = totalLinkedPatients,
                TotalMealsRegistered = totalMealsRegistered
            };
        }

        public async Task<NutritionistResponse?> GetByIdAsync(Guid patientId)
        {
            var query = await (from np in _context.NutritionistsPatients
                               where np.PatientId == patientId && np.IsActive == true
                               select new NutritionistResponse
                               {
                                   FirstName = np.Nutritionist.FirstName,
                                   LastName = np.Nutritionist.LastName,
                                   Gender = np.Nutritionist.Gender,
                                   Crn = np.Nutritionist.Crn.Value.ToString(),
                                   Email = np.Nutritionist.Email.Adress.ToString(),
                                   StartDate = np.StartDate
                               }).FirstOrDefaultAsync();

            return query;
        }

        public async Task<NutritionistProfileResponse?> GetProfileAsync(Guid nutritionistId)
        {
            var query = await (
                from np in _context.NutritionistsPatients
                join n in _context.Nutritionists on np.NutritionistId equals n.Id
                join p in _context.Patients on np.PatientId equals p.Id
                where np.NutritionistId == nutritionistId && np.IsActive
                select new
                {
                    Nutritionist = n,
                    Patient = p,
                    StartDate = np.StartDate.ToLocalTime().ToString("dd/MM/yyyy")
                }).ToListAsync();

            var patientIds = query.Select(x => x.Patient.Id).Distinct().ToList();

            var latestMeasurements = await _context.BodyMeasurements
                .Where(b => patientIds.Contains(b.PatientId))
                .GroupBy(b => b.PatientId)
                .Select(g => g.OrderByDescending(b => b.CreationTime).First())
                .ToListAsync();

            var response = new NutritionistProfileResponse
            {
                FirstName = query.First().Nutritionist.FirstName,
                LastName = query.First().Nutritionist.LastName,
                Crn = query.First().Nutritionist.Crn.ToString(),
                LinkedPatients = query.Select(x =>
                {
                    var m = latestMeasurements.FirstOrDefault(b => b.PatientId == x.Patient.Id);
                    return new LinkedPatientResponse
                    {
                        Id = x.Patient.Id,
                        FirstName = x.Patient.FirstName,
                        LastName = x.Patient.LastName,
                        Gender = x.Patient.Gender.ToString(),
                        Email = x.Patient.Email.Adress,
                        Height = m?.Height ?? 0,
                        Weight = m?.Weight ?? 0,
                        StartDate = x.StartDate
                    };
                }).ToList()
            };

            return response;
        }

        public async Task<NutritionistRecipeResponse?> GetRecipeAsync(Guid id, Guid nutritionistId, GetRecipeRequest request)
        {
            var query = _context.NutritionistRecipes
            .Where(r => r.Id == id && r.NutritionistId == nutritionistId);

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
                .Select(x => new NutritionistRecipeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PreparationTime = x.PreparationTime,
                    Calories = x.Calories,
                    Protein = x.Protein,
                    Fat = x.Fat,
                    Carbs = x.Carbs,
                    Meal = x.Meal,
                    Links = x.NutritionistPatientRecipes.Select(l => new RecipeLinkItem
                    {
                        PatientId = l.Patient.Id,
                        PatientName = l.Patient.FirstName + " " + l.Patient.LastName,
                        RecipeLinkId = l.Id
                    })
                }).FirstOrDefaultAsync();

            return recipe;
        }

        public async Task<IEnumerable<NutritionistRecipeResponse>> GetRecipesAsync(Guid nutritionistId, GetRecipeRequest request)
        {
            var query = _context.NutritionistRecipes
                .Where(r => r.NutritionistId == nutritionistId);

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
                .Select(x => new NutritionistRecipeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PreparationTime = x.PreparationTime,
                    Calories = x.Calories,
                    Protein = x.Protein,
                    Fat = x.Fat,
                    Carbs = x.Carbs,
                    Meal = x.Meal,
                    Links = x.NutritionistPatientRecipes.Select(l => new RecipeLinkItem
                    {
                        PatientId = l.Patient.Id,
                        PatientName = l.Patient.FirstName + " " + l.Patient.LastName,
                        RecipeLinkId = l.Id
                    })
                }).ToListAsync();

            return recipes;
        }
    }
}