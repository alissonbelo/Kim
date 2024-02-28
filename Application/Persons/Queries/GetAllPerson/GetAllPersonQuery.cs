using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Persons.Queries.GetAllPerson;

public class GetAllPersonQuery : IRequest<IEnumerable<Person>>
{
    public class GetAllPeronQueryHandler : IRequestHandler<GetAllPersonQuery, IEnumerable<Person>>
    {
        private readonly IPersonRepository _personRepository;

        public GetAllPeronQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<Person>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetAll(cancellationToken);
            
            return persons;
        }
    }
}