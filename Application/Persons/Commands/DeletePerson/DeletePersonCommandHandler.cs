using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Persons.Commands.DeletePerson;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Person>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonRepository _personRepository;

    public DeletePersonCommandHandler(IUnitOfWork unitOfWork, IPersonRepository personRepository)
    {
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
    }

    public async Task<Person> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var deletePerson = await _personRepository.Get(request.Id, cancellationToken);

        if (deletePerson is null)
            throw new InvalidOperationException("Pessoa não existe!");
            
        _personRepository.Delete(deletePerson);

        await _unitOfWork.Commit(cancellationToken);

        return deletePerson;
    }
}