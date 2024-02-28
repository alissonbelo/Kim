using Application.Persons.Queries.GetAllPerson;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Persons.Queries.GetPerson;

public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, Person>
{
    private readonly IPersonRepository _personRepository;
    
    public GetPersonQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Person> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.Get(request.Id, cancellationToken);
        return person;
    }
}