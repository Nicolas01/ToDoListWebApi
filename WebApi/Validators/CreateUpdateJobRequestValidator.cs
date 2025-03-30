using FluentValidation;
using WebApi.Requests;

namespace WebApi.Validators;

public class CreateUpdateJobRequestValidator : AbstractValidator<CreateUpdateJobRequest>
{
    public CreateUpdateJobRequestValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(4)
            .MaximumLength(16);
    }
}
