using FluentValidation;
using ToDoListWebApi.Requests;

namespace ToDoListWebApi.Validators;

public class CreateUpdateTaskRequestValidator : AbstractValidator<CreateUpdateTaskRequest>
{
    public CreateUpdateTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(4)
            .MaximumLength(16);
    }
}
