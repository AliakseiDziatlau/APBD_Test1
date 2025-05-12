using APBD_Test1.Application.Commands;
using APBD_Test1.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Test1.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitsController :ControllerBase
{
    private readonly IMediator _mediator;

    public VisitsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVisitById(int id)
    {
        var result = await _mediator.Send(new GetVisitQuery{Id = id});
        return result.isSuccess ? Ok(result.dto) : BadRequest(new {error = "Error"});
    }

    [HttpPost]
    public async Task<IActionResult> CreateVisit([FromBody] CreateVisitCommand command)
    {
        var result = await _mediator.Send(command);
        return result.success ? StatusCode(201, result.message) : BadRequest(new {error = result.message});
    }
}