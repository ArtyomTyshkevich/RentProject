using MediatR;
using User.BLL.DTOs.Review;

namespace User.BLL.CQRS.Reviews.Queries
{
    public class GetReviewsByReviewerQuery : IRequest<IReadOnlyList<ReviewDto>>
    {
        public Guid ReviewerId { get; set; }
    }
}