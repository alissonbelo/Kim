using Domain.Entities;
using MediatR;

namespace Application.Persons.Queries.GetPerson;

public class GetPersonQuery : IRequest<Person>
{
    public Guid Id { get; set; }
}