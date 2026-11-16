using MediatR;
using User.BLL.DTOs.Review;

namespace User.BLL.CQRS.Reviews.Commands
{
    public class CreateReviewCommand : IRequest<ReviewDto>
    {
        public double Score { get; set; }
        public string? Text { get; set; }
        public List<Guid> ProsIds { get; set; } = new();
        public Guid ReviewerId { get; set; }
    }
}