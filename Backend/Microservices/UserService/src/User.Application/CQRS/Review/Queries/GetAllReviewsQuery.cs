using MediatR;
using User.BLL.DTOs.Review;

namespace User.BLL.CQRS.Reviews.Queries
{
    public class GetAllReviewsQuery : IRequest<PagedResult<ReviewDto>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}