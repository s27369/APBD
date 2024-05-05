using FluentValidation;

namespace APBD_cw6;

public class CreateAnimalValidator : AbstractValidator<AnimalDTO.CreateAnimalRequest>
{
    public CreateAnimalValidator()
    {
        RuleFor(e => e.Name).MaximumLength(200).NotNull();
        RuleFor(e => e.Desc).MaximumLength(200);
        RuleFor(e => e.Cat).MaximumLength(200).NotNull();
        RuleFor(e => e.Area).MaximumLength(200).NotNull();
    }
}
public class EditAnimalRequestValidator : AbstractValidator<AnimalDTO.EditAnimalRequest>
{
    public EditAnimalRequestValidator()
    {
        RuleFor(e => e.Name).MaximumLength(200).NotNull();
        RuleFor(e => e.Desc).MaximumLength(200);
        RuleFor(e => e.Cat).MaximumLength(200).NotNull();
        RuleFor(e => e.Area).MaximumLength(200).NotNull();
    }
}