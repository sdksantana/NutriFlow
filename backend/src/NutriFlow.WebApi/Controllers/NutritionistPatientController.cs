using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriFlow.Application.Requests.NutritionistPatient;
using NutriFlow.Domain.Queries.Interfaces;
using NutriFlow.Domain.Queries.Responses.NutritionistPatient;
using NutriFlow.Domain.Queries.Responses.Patient;
using NutriFlow.Domain.Requests.NutritionistPatient;

namespace NutriFlow.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/nutricionists-patients")]
    public class NutritionistPatientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly INutritionistPatientQuery _nutritionistPatientQuery;

        public NutritionistPatientController(IMediator mediator, INutritionistPatientQuery nutritionistPatientQuery)
        {
            _mediator = mediator;
            _nutritionistPatientQuery = nutritionistPatientQuery;
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddLinkAsync(Guid userId, [FromBody] AddLinkRequest request)
        {
            request.UserId = userId;

            var response = await _mediator.Send(request);

            if (response.IsFailed)
                return BadRequest(new
                {
                    Errors = response.Errors.Select(e => e.Message)
                });

            return Ok(response);
        }


        [HttpGet("recipe-filters")]
        [ProducesResponseType(typeof(RecipeFilterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecipeFiltersAsync()
        {
            var response = await _nutritionistPatientQuery.GetRecipeFiltersAsync();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpPost("recipes-link/{nutritionistId}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRecipeLinkAsync(Guid nutritionistId, [FromBody] AddRecipeLinkRequest request)
        {
            request.NutritionistId = nutritionistId;

            var response = await _mediator.Send(request);

            if (response.IsFailed)
                return BadRequest(new
                {
                    Errors = response.Errors.Select(e => e.Message)
                });

            return Ok(response);
        }


        [HttpDelete("recipes-link/{id}/{nutritionistId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRecipeAsync(Guid id, Guid nutritionistId)
        {
            var response = new DeleteRecipeLinkRequest
            {
                Id = id,
                NutritionistId = nutritionistId
            };

            var result = await _mediator.Send(response);

            if (result.IsFailed)
                return BadRequest(result);

            return Ok();
        }


        [HttpGet("recipes-link/{nutritionistRecipeId}")]
        [ProducesResponseType(typeof(NutritionistRecipeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNutritionistByIdAsync(Guid nutritionistRecipeId)
        {
            var response = await _nutritionistPatientQuery.GetNutritionistByIdAsync(nutritionistRecipeId);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        //[HttpPut("desvincular/{nutricionistaId}/{pacienteId}")]
        //public async Task<IActionResult> Unlink(Guid nutricionistaId, Guid pacienteId)
        //{

        //}

    }
}
