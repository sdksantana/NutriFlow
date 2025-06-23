using Microsoft.EntityFrameworkCore;
using NutriFlow.Domain.Entities;
using NutriFlow.Domain.Repositories;
using NutriFlow.Domain.ValueObjects;

namespace NutriFlow.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly NutriFlowDbContext _context;

        public PatientRepository(NutriFlowDbContext context)
        {
            _context = context;
        }

        #region Evolution
        public async Task<BodyMeasurement?> GetBodyMeasurementByRecentDateAsync(Guid patientId)
        {
            return await _context.BodyMeasurements
               .Where(bm => bm.PatientId == patientId)
               .OrderByDescending(bm => bm.CreationTime)
               .Select(x => new BodyMeasurement(
                                x.Id,
                                x.Age,
                                x.Height,
                                x.Weight,
                                x.Neck,
                                x.Chest,
                                x.Waist,
                                x.Abdomen,
                                x.Hip,
                                x.RightArm,
                                x.LeftArm,
                                x.RightForearm,
                                x.LeftForearm,
                                x.RightThigh,
                                x.LeftThigh,
                                x.RightCalf,
                                x.LeftCalf,
                                new Patient(
                                    x.Patient.Id,
                                    x.Patient.FirstName,
                                    x.Patient.LastName,
                                    x.Patient.Gender,
                                    x.Patient.Email,
                                    x.Patient.IsMeasurementComplete
                                    )
                                )
               ).FirstOrDefaultAsync();
        }

        public async Task UpdateMeasurementStatusAsync(Guid patientId, bool isComplete)
        {
            var entity = new Tables.Patient
            {
                Id = patientId,
                IsMeasurementComplete = isComplete
            };

            _context.Attach(entity);
            _context.Entry(entity).Property(x => x.IsMeasurementComplete).IsModified = true;

            await _context.SaveChangesAsync();
        }
        public async Task<Guid> SaveBodyMeasurementAsync(BodyMeasurement bodyMeasurement)
        {
            var userName = bodyMeasurement.Patient.Email.Adress;

            var entity = new Tables.BodyMeasurement
            {
                Id = bodyMeasurement.Id,
                Age = bodyMeasurement.Age,
                Height = bodyMeasurement.Height,
                Weight = bodyMeasurement.Weight,
                Neck = bodyMeasurement.Neck,
                Chest = bodyMeasurement.Chest,
                Waist = bodyMeasurement.Waist,
                Abdomen = bodyMeasurement.Abdomen,
                Hip = bodyMeasurement.Hip,
                RightArm = bodyMeasurement.RightArm,
                LeftArm = bodyMeasurement.LeftArm,
                RightForearm = bodyMeasurement.RightForearm,
                LeftForearm = bodyMeasurement.LeftForearm,
                RightThigh = bodyMeasurement.RightThigh,
                LeftThigh = bodyMeasurement.LeftThigh,
                RightCalf = bodyMeasurement.RightCalf,
                LeftCalf = bodyMeasurement.LeftCalf,
                PatientId = bodyMeasurement.Patient.Id,
                CreationTime = DateTime.UtcNow,
                CreatorUserName = userName
            };

            await _context.BodyMeasurements.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        #endregion

        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await _context.Patients
                 .Where(p => p.Id == id)
                 .Select(x => new Patient(
                                        x.Id,
                                        x.FirstName,
                                        x.LastName,
                                        x.Gender,
                                        new Email(x.Email.Adress),
                                        x.IsMeasurementComplete
                 )).FirstOrDefaultAsync();

        }

        public async Task<Patient?> GetByEmailAsync(Email email)
        {
            return await _context.Patients
                 .Where(p => p.Email == email)
                 .Select(x => new Patient(
                                        x.Id,
                                        x.FirstName,
                                        x.LastName,
                                        x.Gender,
                                        new Email(x.Email.Adress),
                                        x.IsMeasurementComplete
                 )).FirstOrDefaultAsync();
        }

        public async Task<Guid> SaveAsync(Patient patient)
        {
            var exists = await _context.Patients.AnyAsync(p => p.Id == patient.Id);

            if (!exists)
            {
                var entity = new Tables.Patient
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Gender = patient.Gender,
                    Email = patient.Email,
                    IsMeasurementComplete = patient.IsMeasurementComplete
                };

                _context.Patients.Add(entity);
            }
            else
            {
                var entity = new Tables.Patient
                {
                    Id = patient.Id,
                    IsMeasurementComplete = patient.IsMeasurementComplete
                };

                _context.Attach(entity);
                _context.Entry(entity).Property(p => p.IsMeasurementComplete).IsModified = true;
            }

            await _context.SaveChangesAsync();
            return patient.Id;
        }

        public async Task<bool> HasMeasurementsAsync(Guid patientId)
        {
            return await _context.BodyMeasurements
             .AnyAsync(p => p.PatientId == patientId);
        }

        public async Task<Guid> SaveRecipeAsync(PatientRecipe patientRecipe)
        {
            var entity = new Tables.PatientRecipe
            {
                Id = patientRecipe.Id,
                Name = patientRecipe.Name,
                Description = patientRecipe.Description,
                PreparationTime = patientRecipe.PreparationTime,
                Calories = patientRecipe.Calories,
                Protein = patientRecipe.Protein,
                Fat = patientRecipe.Fat,
                Carbs = patientRecipe.Carbs,
                Meal = patientRecipe.Meal,
                CreationTime = DateTime.UtcNow,
                PatientId = patientRecipe.Patient.Id,
            };

            if (patientRecipe.Id == Guid.Empty)
            {
                await _context.AddAsync(entity);
            }
            else
            {
                entity.Id = patientRecipe.Id;

                var entry = _context.Entry(entity);

                entry.Property(p => p.Name).IsModified = true;
                entry.Property(p => p.Description).IsModified = true;
                entry.Property(p => p.PreparationTime).IsModified = true;
                entry.Property(p => p.Calories).IsModified = true;
                entry.Property(p => p.Protein).IsModified = true;
                entry.Property(p => p.Fat).IsModified = true;
                entry.Property(p => p.Carbs).IsModified = true;
                entry.Property(p => p.Meal).IsModified = true;
            }

            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<PatientRecipe?> GetRecipeByIdAsync(Guid patientId, Guid id)
        {
            return await _context.PatientRecipes.Where(x => x.PatientId == patientId || x.Id == id)
                 .Select(x => new PatientRecipe(
                     x.Id,
                     x.Name,
                     x.Description,
                     x.PreparationTime,
                     x.Calories,
                     x.Protein,
                     x.Fat,
                     x.Carbs,
                     x.Meal,
                     new Patient(
                         x.Patient.Id,
                         x.Patient.FirstName,
                         x.Patient.LastName,
                         x.Patient.Gender,
                         x.Patient.Email,
                         x.Patient.IsMeasurementComplete)))
                 .FirstOrDefaultAsync();
        }

        public async Task DeleteRecipeAsync(PatientRecipe patientRecipe)
        {
            var entity = new Tables.PatientRecipe
            {
                Id = patientRecipe.Id
            };

            _context.PatientRecipes.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
