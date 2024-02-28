using Domain.Entities;
using MediatR;

namespace Application.Persons.Commands.CreatePerson;

public class CreatePersonCommand : IRequest<Person>
{
    public string? Name { get; set; }
    public string? Document { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}