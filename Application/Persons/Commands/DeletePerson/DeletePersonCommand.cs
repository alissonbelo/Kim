using Domain.Entities;
using MediatR;

namespace Application.Persons.Commands.DeletePerson;

public class DeletePersonCommand : IRequest<Person>
{
    public Guid Id { get; set; }
}