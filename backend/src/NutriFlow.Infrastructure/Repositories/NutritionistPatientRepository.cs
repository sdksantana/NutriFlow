using Microsoft.EntityFrameworkCore;
using NutriFlow.Domain.Entities;
using NutriFlow.Domain.ValueObjects;
using NutriFlow.Infrastructure;

namespace NutriFlow.Domain.Repositories
{
    public class NutritionistPatientRepository : INutritionistPatientRepository
    {
        private readonly NutriFlowDbContext _context;

        public NutritionistPatientRepository(NutriFlowDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> GetByPairAsync(Guid nutritionistId, Email email)
        //{
        //    return await _context.NutritionistPatients
        //    .AnyAsync(p => p.NutritionistId == nutritionistId && p.Patient.Email == email);
        //}

        public async Task<NutritionistPatient?> GetByPairAsync(Guid id, Email email)
        {
            return await _context.NutritionistsPatients
                .Where(p => p.NutritionistId == id && p.Patient.Email == email || p.PatientId == id && p.Nutritionist.Email == email)
                .Select(x => new NutritionistPatient(
                    x.Id,
                    new Nutritionist(
                        x.Nutritionist.Id,
                        x.Nutritionist.FirstName,
                        x.Nutritionist.LastName,
                        x.Nutritionist.Crn,
                        x.Nutritionist.Gender,
                        x.Nutritionist.Email
                    ),
                    new Patient(
                        x.Patient.Id,
                        x.Patient.FirstName,
                        x.Patient.LastName,
                        x.Patient.Gender,
                        x.Patient.Email,
                        x.Patient.IsMeasurementComplete
                    )
                ))
                .FirstOrDefaultAsync();
        }

        public async Task SaveAsync(NutritionistPatient nutritionistPatient)
        {
            if (nutritionistPatient.Id == Guid.Empty)
            {
                var entity = new Tables.NutritionistPatient
                {
                    Id = nutritionistPatient.Id,
                    PatientId = nutritionistPatient.Patient.Id,
                    NutritionistId = nutritionistPatient.Nutritionist.Id,
                    IsActive = nutritionistPatient.IsActive,
                    StartDate = nutritionistPatient.StartDate
                };

                _context.NutritionistsPatients.Add(entity);
            }
            else
            {
                var entity = new Tables.NutritionistPatient
                {
                    Id = nutritionistPatient.Id,
                    IsActive = nutritionistPatient.IsActive,
                    StartDate = DateTime.Now,
                };

                _context.Attach(entity);
                _context.Entry(entity).Property(p => p.IsActive).IsModified = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveRecipeAsync(NutritionistPatientRecipe nutritionistPatientRecipe)
        {
            var entity = new Infrastructure.Tables.NutritionistPatientRecipe
            {
                Id = nutritionistPatientRecipe.Id,
                PatientId = nutritionistPatientRecipe.Patient.Id,
                NutritionistRecipeId = nutritionistPatientRecipe.NutritionistRecipe.Id,
            };

            _context.NutritionistPatientRecipes.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(NutritionistPatientRecipe nutritionistPatientRecipe)
        {
            var entity = new Infrastructure.Tables.NutritionistPatientRecipe
            {
                Id = nutritionistPatientRecipe.Id
            };

            _context.NutritionistPatientRecipes.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<NutritionistPatientRecipe?> GetLinkByIdAsync(Guid id)
        {
            return await _context.NutritionistPatientRecipes
              .Where(p => p.Id == id)
              .Select(x => new NutritionistPatientRecipe(
                                     x.Id,
                                     new NutritionistRecipe(x.Id,
                                         x.NutritionistRecipe.Name,
                                         x.NutritionistRecipe.Description,
                                         x.NutritionistRecipe.PreparationTime,
                                         x.NutritionistRecipe.Calories,
                                         x.NutritionistRecipe.Protein,
                                         x.NutritionistRecipe.Fat,
                                         x.NutritionistRecipe.Carbs,
                                         x.NutritionistRecipe.Meal, new Nutritionist(
                                             x.NutritionistRecipe.Nutritionist.Id,
                                             x.NutritionistRecipe.Nutritionist.FirstName,
                                             x.NutritionistRecipe.Nutritionist.LastName,
                                             x.NutritionistRecipe.Nutritionist.Crn,
                                             x.NutritionistRecipe.Nutritionist.Gender,
                                             x.NutritionistRecipe.Nutritionist.Email)),
                                     new Patient(
                                        x.Patient.Id,
                                        x.Patient.FirstName,
                                        x.Patient.LastName,
                                        x.Patient.Gender,
                                        new Email(x.Patient.Email.Adress),
                                        x.Patient.IsMeasurementComplete))).FirstOrDefaultAsync();

        }
    }
}