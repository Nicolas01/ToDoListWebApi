using FluentValidation;
using WebApi.Requests;

namespace WebApi.Validators;

public class GetJobsRequestValidator : AbstractValidator<GetJobsRequest>
{
    public GetJobsRequestValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(2)
            .When(x => x.Title is not null);

        RuleFor(x => (int)x.Limit)
            .LessThanOrEqualTo(100);
    }
}
