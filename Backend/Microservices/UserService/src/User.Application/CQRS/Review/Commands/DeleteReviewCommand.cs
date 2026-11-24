using MediatR;

namespace User.BLL.CQRS.Reviews.Commands
{
    public class DeleteReviewCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}