using MediatR;
using User.BLL.DTOs.Review;

namespace User.BLL.CQRS.Reviews.Queries
{
    public class GetReviewByIdQuery : IRequest<ReviewDto?>
    {
        public Guid Id { get; set; }
    }
}