using FluentValidation;
using ToDoListWebApi.Requests;

namespace ToDoListWebApi.Validators;

public class GetTasksRequestValidator : AbstractValidator<GetTasksRequest>
{
    public GetTasksRequestValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(2)
            .When(x => x.Title is not null);

        RuleFor(x => (int)x.Limit)
            .LessThanOrEqualTo(100);
    }
}
