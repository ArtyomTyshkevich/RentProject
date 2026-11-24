using FluentValidation;
using User.BLL.CQRS.Reviews.Commands;

namespace User.BLL.Validators.Review
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Score).InclusiveBetween(0, 5).When(x => x.Score.HasValue);
            RuleFor(x => x.Text).MaximumLength(2000).When(x => x.Text != null);
            RuleFor(x => x.ProsIds)
                .Must(ids => ids == null || ids.Distinct().Count() == ids.Count)
                .WithMessage("ProsIds содержит дубликаты");
        }
    }
}