using MediatR;
using User.BLL.DTOs.Review;

namespace User.BLL.CQRS.Reviews.Commands
{
    public class UpdateReviewCommand : IRequest<ReviewDto>
    {
        public Guid Id { get; set; }
        public double? Score { get; set; }
        public string? Text { get; set; }
        public List<Guid>? ProsIds { get; set; }
    }
}