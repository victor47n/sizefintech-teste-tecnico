using Microsoft.AspNetCore.Mvc;
using SizeFintech.Application.UseCases.Industries.GetAll;
using SizeFintech.Communication.Responses;

namespace SizeFintech.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class IndustriesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseIndustriesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllIndustries([FromServices] IGetAllIndustriesUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Industries.Count != 0) return Ok(response);

        return NoContent();
    }
}
