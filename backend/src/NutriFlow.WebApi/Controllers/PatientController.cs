using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriFlow.Application.Requests.Patient;
using NutriFlow.Domain.Queries.Interfaces;
using NutriFlow.Domain.Queries.Requests;
using NutriFlow.Domain.Queries.Responses;
using NutriFlow.Domain.Queries.Responses.Patient;

namespace NutriFlow.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientQuery _patientQuery;
        private readonly IMediator _mediator;

        public PatientController(IPatientQuery patientQuery, IMediator mediator)
        {
            _patientQuery = patientQuery;
            _mediator = mediator;
        }

        [HttpGet("{patientId}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientByIdAsync(Guid id)
        {
            var response = await _patientQuery.GetPatientByIdAsync(id);

            if (response == null)
                return NotFound("Paciente não encontrado");

            return Ok(response);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientByEmailAsync(string email)
        {
            var response = await _patientQuery.GetPatientByEmailAsync(email);

            if (response == null)
                return NotFound("Paciente não encontrado");

            return Ok(response);
        }

        [HttpGet("dashboard/{patientId}")]
        [ProducesResponseType(typeof(DashboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashBoardAsync(Guid patientId)
        {
            var response = await _patientQuery.GetDashBoardAsync(patientId);

            if (response == null)
                return NotFound("Paciente não encontrado");

            return Ok(response);
        }

        #region Evolution
        [HttpPost("body-measurements/{patientId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBodyMeasurementAsync(Guid patientId, [FromBody] AddBodyMeasurementRequest request)
        {
            request.PatientId = patientId;

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("body-measurements/{patientId}")]
        [ProducesResponseType(typeof(BodyMeasurementResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBodyMeasurementByRecentDateAsync(Guid patientId)
        {
            var response = await _patientQuery.GetBodyMeasurementByRecentDateAsync(patientId);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpPut("body-measurements/{patientId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBodyMeasurementAsync(Guid patientId, [FromBody] UpdateBodyMeasurementRequest request)
        {
            request.PatientId = patientId;

            var result = await _mediator.Send(request);

            if (result.IsFailed)
                return BadRequest(result);

            return Ok();
        }

        [HttpGet("body-measurements-historic/{patientId}")]
        [ProducesResponseType(typeof(BodyMeasurementHistoricResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBodyMeasurementHistoricAsync(Guid patientId)
        {
            var response = await _patientQuery.GetBodyMeasurementHistoricAsync(patientId);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("tmb/{patientId}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTMBAsync(Guid patientId)
        {
            var response = await _patientQuery.GetTmbAsync(patientId);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("body-measurements-evolution/{patientId}")]
        [ProducesResponseType(typeof(BodyMeasurementEvolutionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBodyMeasurementEvolutionAsync(Guid patientId)
        {
            var response = await _patientQuery.GetBodyMeasurementEvolutionAsync(patientId);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }
        #endregion

        #region Recipes
        [HttpPost("recipes/{patientId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRecipesAsync(Guid patientId, [FromBody] AddPatientRecipeRequest request)
        {
            request.PatientId = patientId;

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("recipes/{id}/{patientId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRecipeAsync(Guid id, Guid patientId, [FromBody] UpdatePatientRecipeRequest request)
        {
            request.PatientId = patientId;
            request.Id = id;

            var result = await _mediator.Send(request);

            if (result.IsFailed)
                return BadRequest(result);

            return Ok();
        }

        [HttpGet("recipes/{id}/{patientId}")]
        [ProducesResponseType(typeof(RecipeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecipeAsync(Guid id, Guid patientId, [FromQuery] GetRecipeRequest request)
        {
            var response = await _patientQuery.GetRecipeAsync(id, patientId, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("recipes/{patientId}")]
        [ProducesResponseType(typeof(IEnumerable<RecipeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecipesAsync(Guid patientId, [FromQuery] GetRecipeRequest request)
        {
            var response = await _patientQuery.GetRecipesAsync(patientId, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }


        [HttpDelete("recipes/{id}/{patientId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRecipeAsync(Guid id, Guid patientId)
        {
            var command = new DeletePatientRecipeRequest
            {
                Id = id,
                PatientId = patientId
            };

            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest(result);

            return Ok();
        }

        [HttpGet("plan-recipes/{id}/{patientId}")]
        [ProducesResponseType(typeof(RecipeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPlanRecipesByIdAsync(Guid id, Guid patientId, [FromQuery] GetRecipeRequest request)
        {
            var response = await _patientQuery.GetPlanRecipesByIdAsync(id, patientId, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("plan-recipes/{patientId}")]
        [ProducesResponseType(typeof(IEnumerable<RecipeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPlanRecipesAsync(Guid patientId, [FromQuery] GetRecipeRequest request)
        {
            var response = await _patientQuery.GetPlanRecipesAsync(patientId, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        #endregion
    }
}
