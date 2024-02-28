using Domain.Entities;
using MediatR;

namespace Application.Persons.Commands.UpdatePerson;

public class UpdatePersonCommand : IRequest<Person>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Document { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}