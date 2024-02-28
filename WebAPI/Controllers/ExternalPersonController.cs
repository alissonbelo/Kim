using Application.Persons.Queries.GetExternalPersonData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExternalPersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExternalPersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllExternalData()
    {
        var query = new GetExternalDataQuery();
        var data = await _mediator.Send(query);

        return Ok(data);
    }
}