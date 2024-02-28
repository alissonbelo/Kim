using FluentValidation;

namespace Application.Persons.Commands.DeletePerson;

public sealed class DeletePersonValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonValidator() 
    {
        RuleFor(x => x.Id).NotNull();
    }
}