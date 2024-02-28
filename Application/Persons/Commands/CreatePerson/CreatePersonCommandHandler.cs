using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Persons.Commands.CreatePerson;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Person>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonRepository _personRepository;
    private readonly IPersonRegistrationQueue _personRegistrationQueue;

    public CreatePersonCommandHandler(IUnitOfWork unitOfWork, IPersonRepository personRepository, IPersonRegistrationQueue personRegistrationQueue)
    {
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
        _personRegistrationQueue = personRegistrationQueue;
    }

    public async Task<Person> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var newPerson = new Person(request.Name, request.Document, request.Email, request.Phone);

         _personRepository.Create(newPerson);
        
        await _unitOfWork.Commit(cancellationToken);
        
        await _personRegistrationQueue.EnqueueAsync(newPerson, cancellationToken);

        return newPerson;
    }
}