using FluentValidation;
using WebApi.Requests;

namespace WebApi.Validators;

public class GetTasksRequestValidator : AbstractValidator<GetJobsRequest>
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
