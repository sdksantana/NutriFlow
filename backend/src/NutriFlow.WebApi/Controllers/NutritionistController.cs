

using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriFlow.Application.Requests.Nutritionist;
using NutriFlow.Domain.Queries.Interfaces;
using NutriFlow.Domain.Queries.Requests;
using NutriFlow.Domain.Queries.Responses.Nutritionist;
using NutriFlow.Domain.Queries.Responses.NutritionistPatient;

namespace NutriFlow.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/nutritionists")]
    public class NutritionistController : ControllerBase
    {
        private readonly INutritionistQuery _nutritionistQuery;
        private readonly IMediator _mediator;

        public NutritionistController(INutritionistQuery nutritionistQuery, IMediator mediator)
        {
            _nutritionistQuery = nutritionistQuery;
            _mediator = mediator;
        }

        [HttpGet("dashboard/{nutritionistId}")]
        [ProducesResponseType(typeof(NutritionistDashboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashBoardAsync(Guid nutritionistId)
        {
            var response = await _nutritionistQuery.GetDashBoardAsync(nutritionistId);

            if (response == null)
                return NotFound("Nutricionista não encontrado");

            return Ok(response);
        }

        [HttpGet("{nutritionistId}")]
        [ProducesResponseType(typeof(NutritionistResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(Guid nutritionistId)
        {
            var response = await _nutritionistQuery.GetByIdAsync(nutritionistId);

            if (response == null)
                return NotFound("Nutricionista não encontrado");

            return Ok(response);
        }


        [HttpGet("profile/{nutritionistId}")]
        [ProducesResponseType(typeof(NutritionistProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProfileAsync(Guid nutritionistId)
        {
            var response = await _nutritionistQuery.GetProfileAsync(nutritionistId);

            if (response == null)
                return NotFound("Nutricionista não encontrado");

            return Ok(response);
        }

        #region Recipes
        [HttpPost("recipes/{nutritionistId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRecipesAsync(Guid nutritionistId, [FromBody] AddNutritionistRecipeRequest request)
        {
            request.NutritionistId = nutritionistId;

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("recipes/{id}/{nutritionistId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRecipeAsync(Guid id, Guid nutritionistId, [FromBody] UpdateNutritionistRecipeRequest request)
        {
            request.NutritionistId = nutritionistId;
            request.Id = id;

            var result = await _mediator.Send(request);

            if (result.IsFailed)
                return BadRequest(result);

            return Ok();
        }

        [HttpGet("recipes/{id}/{nutritionistId}")]
        [ProducesResponseType(typeof(NutritionistRecipeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecipeAsync(Guid id, Guid nutritionistId, [FromQuery] GetRecipeRequest request)
        {
            var response = await _nutritionistQuery.GetRecipeAsync(id, nutritionistId, request);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("recipes/{nutritionistId}")]
        [ProducesResponseType(typeof(IEnumerable<NutritionistRecipeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecipesAsync(Guid nutritionistId, [FromQuery] GetRecipeRequest request)
        {
            var response = await _nutritionistQuery.GetRecipesAsync(nutritionistId, request);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpDelete("recipes/{id}/{nutritionistId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRecipeAsync(Guid id, Guid nutritionistId)
        {
            var response = new DeleteNutritionistRecipeRequest
            {
                Id = id,
                NutritionistId = nutritionistId
            };

            var result = await _mediator.Send(response);

            if (result.IsFailed)
                return BadRequest(result);

            return Ok();
        }
        #endregion
    }
}
