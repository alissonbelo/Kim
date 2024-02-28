using FluentValidation;

namespace Application.Persons.Commands.UpdatePerson;

public sealed class UpdatePersonValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(150);
        RuleFor(x => x.Document).NotEmpty().Length(11);
        RuleFor(x => x.Email).NotEmpty().MinimumLength(3).MaximumLength(150).EmailAddress();
        RuleFor(x => x.Phone).NotEmpty().MinimumLength(10).MaximumLength(11);
    }
}