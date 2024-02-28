using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Persons.Commands.UpdatePerson;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Person>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonRepository _personRepository;

    public UpdatePersonCommandHandler(IUnitOfWork unitOfWork, IPersonRepository personRepository)
    {
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
    }


    public async Task<Person> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.Get(request.Id, cancellationToken);

        if (person is null)
            throw new InvalidOperationException("Pessoa não existe!");

        person.Name = request.Name;
        person.Document = request.Document;
        person.Email = request.Email;
        person.Phone = request.Phone;

        _personRepository.Update(person);
        
        await _unitOfWork.Commit(cancellationToken);

        var updetedPerson = await _personRepository.Get(request.Id, cancellationToken);

        return updetedPerson;
    }
}