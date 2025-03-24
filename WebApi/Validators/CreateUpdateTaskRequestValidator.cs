using FluentValidation;
using WebApi.Requests;

namespace WebApi.Validators;

public class CreateUpdateTaskRequestValidator : AbstractValidator<CreateUpdateJobRequest>
{
    public CreateUpdateTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(4)
            .MaximumLength(16);
    }
}
