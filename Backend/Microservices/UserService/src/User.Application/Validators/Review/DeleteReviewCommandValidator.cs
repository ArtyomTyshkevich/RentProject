using FluentValidation;
using User.BLL.CQRS.Reviews.Commands;

namespace User.BLL.Validators.Review
{
    public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}