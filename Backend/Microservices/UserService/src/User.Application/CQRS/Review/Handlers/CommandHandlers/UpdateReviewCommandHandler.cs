using AutoMapper;
using Library.Application.Interfaces;
using MediatR;
using User.BLL.CQRS.Reviews.Commands;
using User.BLL.DTOs.Review;
using User.BLL.Interfaces;

namespace User.BLL.CQRS.Reviews.Handlers.CommandHandlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Review {request.Id} not found");

            if (request.Score.HasValue)
                review.Score = request.Score.Value;

            if (request.Text != null)
                review.Text = request.Text;

            if (request.ProsIds != null)
            {
                var pros = await _unitOfWork.ProsRepository.GetByIdsAsync(request.ProsIds, cancellationToken);
                if (pros.Count != request.ProsIds.Distinct().Count())
                    throw new ArgumentException("Некоторые Pros не найдены");

                review.Pros = pros.ToList();
            }

            review.UpdatedAt = DateTimeOffset.UtcNow;

            await _unitOfWork.ReviewRepository.UpdateAsync(review, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ReviewDto>(review);
        }
    }
}