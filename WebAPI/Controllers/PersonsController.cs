using Application.Persons.Commands.CreatePerson;
using Application.Persons.Commands.DeletePerson;
using Application.Persons.Commands.UpdatePerson;
using Application.Persons.Queries.GetAllPerson;
using Application.Persons.Queries.GetExternalPersonData;
using Application.Persons.Queries.GetPerson;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPersons()
    {
        var query = new GetAllPersonQuery();
        
        var persons = await _mediator.Send(query);
        return Ok(persons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(Guid id)
    {
        var query = new GetPersonQuery() { Id = id }; 
        var person = await _mediator.Send(query);
        
        return person != null ? Ok(person) : NotFound("Pessoa não existe!");
    }

    [HttpPost]
    public async Task<ActionResult> CreatePerson(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var createPerson = await _mediator.Send(command, cancellationToken);

        return Ok(createPerson);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(Guid id, UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var updatedPerson = await _mediator.Send(command, cancellationToken);

        return updatedPerson != null ? Ok(updatedPerson) : NotFound("Pessoa não existe!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeletePersonCommand { Id = id };

        var deletedPerson = await _mediator.Send(command, cancellationToken);

        return deletedPerson != null ? Ok(deletedPerson) : NotFound("Pessoa não existe!");
    }
}