using Library.Application.Interfaces;
using MediatR;
using User.BLL.CQRS.Reviews.Commands;
using User.BLL.Interfaces;

namespace User.BLL.CQRS.Reviews.Handlers.CommandHandlers
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Review {request.Id} not found");

            await _unitOfWork.ReviewRepository.DeleteAsync(review, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}