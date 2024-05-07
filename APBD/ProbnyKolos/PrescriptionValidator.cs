namespace ProbnyKolos;
using FluentValidation;

public class PrescriptionValidator: AbstractValidator<Prescription>
{
    public PrescriptionValidator()
    {
        RuleFor(e => e._dueDate).GreaterThan(e => e._date).WithMessage(":)");
        
    }
}