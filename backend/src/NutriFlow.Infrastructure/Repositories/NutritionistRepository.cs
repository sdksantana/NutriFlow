using Microsoft.EntityFrameworkCore;
using NutriFlow.Domain.Entities;
using NutriFlow.Domain.Repositories;
using NutriFlow.Domain.ValueObjects;

namespace NutriFlow.Infrastructure.Repositories
{
    public class NutritionistRepository : INutritionistRepository
    {
        private readonly NutriFlowDbContext _context;

        public NutritionistRepository(NutriFlowDbContext context)
        {
            _context = context;
        }

        public async Task<Nutritionist?> GetByEmailAsync(Email email)
        {
            return await _context.Nutritionists
            .Where(p => p.Email == email)
            .Select(x => new Nutritionist(
                                  x.Id,
                                  x.FirstName,
                                  x.LastName,
                                  new Crn(x.Crn.Value),
                                  x.Gender,
                                  new Email(x.Email.Adress)
           )).FirstOrDefaultAsync();
        }

        public async Task<Nutritionist?> GetByIdAsync(Guid id)
        {
            return await _context.Nutritionists
               .Where(p => p.Id == id)
               .Select(x => new Nutritionist(
                                      x.Id,
                                      x.FirstName,
                                      x.LastName,
                                      new Crn(x.Crn.Value),
                                      x.Gender,
                                      new Email(x.Email.Adress)
               )).FirstOrDefaultAsync();
        }

        public async Task<Guid> SaveAsync(Domain.Entities.Nutritionist nutritionist)
        {
            var entity = new Tables.Nutritionist
            {
                Id = nutritionist.Id,
                FirstName = nutritionist.FirstName,
                LastName = nutritionist.LastName,
                Gender = nutritionist.Gender,
                Crn = nutritionist.Crn,
                Email = nutritionist.Email,
            };

            _context.Nutritionists.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> SaveRecipeAsync(NutritionistRecipe nutritionistRecipe)
        {
            var entity = new Tables.NutritionistRecipe
            {
                Id = nutritionistRecipe.Id,
                Name = nutritionistRecipe.Name,
                Description = nutritionistRecipe.Description,
                PreparationTime = nutritionistRecipe.PreparationTime,
                Calories = nutritionistRecipe.Calories,
                Protein = nutritionistRecipe.Protein,
                Fat = nutritionistRecipe.Fat,
                Carbs = nutritionistRecipe.Carbs,
                Meal = nutritionistRecipe.Meal,
                CreationTime = DateTime.UtcNow,
                NutritionistId = nutritionistRecipe.Nutritionist.Id,
            };

            if (nutritionistRecipe.Id == Guid.Empty)
            {
                await _context.AddAsync(entity);
            }
            else
            {
                entity.Id = nutritionistRecipe.Id;

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

        public async Task<NutritionistRecipe?> GetRecipeByIdAsync(Guid nutritionistId, Guid id)
        {
            return await _context.NutritionistRecipes.Where(x => x.NutritionistId == nutritionistId && x.Id == id)
                 .Select(x => new NutritionistRecipe(
                     x.Id,
                     x.Name,
                     x.Description,
                     x.PreparationTime,
                     x.Calories,
                     x.Protein,
                     x.Fat,
                     x.Carbs,
                     x.Meal,
                     new Nutritionist(
                         x.Nutritionist.Id,
                         x.Nutritionist.FirstName,
                         x.Nutritionist.LastName,
                         x.Nutritionist.Crn,
                         x.Nutritionist.Gender,
                         x.Nutritionist.Email)))
                 .FirstOrDefaultAsync();
        }

        public async Task DeleteRecipeAsync(NutritionistRecipe nutritionistRecipe)
        {
            var entity = new Tables.NutritionistRecipe
            {
                Id = nutritionistRecipe.Id
            };

            _context.NutritionistRecipes.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
