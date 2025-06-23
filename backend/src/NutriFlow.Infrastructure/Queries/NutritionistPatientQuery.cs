using Microsoft.EntityFrameworkCore;
using NutriFlow.Domain.Queries.Interfaces;
using NutriFlow.Domain.Queries.Responses.Nutritionist;
using NutriFlow.Domain.Queries.Responses.Patient;

namespace NutriFlow.Infrastructure.Queries
{
    public class NutritionistPatientQuery : INutritionistPatientQuery
    {
        private readonly NutriFlowDbContext _context;

        public NutritionistPatientQuery(NutriFlowDbContext context)
        {
            _context = context;
        }

        public async Task<NutritionistResponse?> GetNutritionistByIdAsync(Guid patientId)
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

        public async Task<List<RecipeFilterResponse>> GetRecipeFiltersAsync()
        {
            var filters = await _context.ReciperFilters.ToListAsync();

            var response = filters
                .GroupBy(f => f.Category)
                .Select(g => new RecipeFilterResponse
                {
                    Category = g.Key,
                    Filters = g.Select(f => new FilterItem
                    {
                        Value = f.Value,
                        Label = f.Value
                    }).ToList()
                }).ToList();

            return response;
        }
    }
}
