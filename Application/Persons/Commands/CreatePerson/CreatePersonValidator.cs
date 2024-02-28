using FluentValidation;

namespace Application.Persons.Commands.CreatePerson;

public sealed class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(150);
        RuleFor(x => x.Document).NotEmpty().Length(11);
        RuleFor(x => x.Email).NotEmpty().MinimumLength(3).MaximumLength(150).EmailAddress();
        RuleFor(x => x.Phone).NotEmpty().MinimumLength(10).MaximumLength(11);
    }
}