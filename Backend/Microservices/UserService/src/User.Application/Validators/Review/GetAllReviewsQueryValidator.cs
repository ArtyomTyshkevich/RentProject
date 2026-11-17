using FluentValidation;
using User.BLL.CQRS.Reviews.Queries;

namespace User.BLL.Validators.Review
{
    public class GetAllReviewsQueryValidator : AbstractValidator<GetAllReviewsQuery>
    {
        public GetAllReviewsQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}