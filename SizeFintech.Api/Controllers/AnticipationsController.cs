using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SizeFintech.Application.UseCases.Anticipations.GetAll;
using SizeFintech.Application.UseCases.Anticipations.GetById;
using SizeFintech.Application.UseCases.Anticipations.Register;
using SizeFintech.Communication.Requests;
using SizeFintech.Communication.Responses;

namespace SizeFintech.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AnticipationsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterAnticipationUseCase useCase,
        [FromBody] RequestRegisterAnticipationJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAnticipationsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAnticipations([FromServices] IGetAllAnticipationsUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Anticipations.Count != 0) return Ok(response);

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseAnticipationJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetAnticipationByIdUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }
}
