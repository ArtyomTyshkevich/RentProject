using AutoMapper;
using Library.Application.Interfaces;
using MediatR;
using User.BLL.CQRS.Reviews.Commands;
using User.BLL.DTOs.Review;
using User.BLL.Interfaces;
using UserService.DAL.Entities;

namespace User.BLL.CQRS.Reviews.Handlers.CommandHandlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewer = await _unitOfWork.Users.GetByIdAsync(request.ReviewerId, cancellationToken)
                ?? throw new KeyNotFoundException($"User {request.ReviewerId} not found");

            var pros = await _unitOfWork.ProsRepository.GetByIdsAsync(request.ProsIds, cancellationToken);
            if (pros.Count != request.ProsIds.Distinct().Count())
            {
                var found = pros.Select(p => p.Id).ToHashSet();
                var missing = request.ProsIds.Where(id => !found.Contains(id));
                throw new ArgumentException($"Pros not found: {string.Join(", ", missing)}");
            }

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Score = request.Score,
                Text = request.Text,
                Pros = pros.ToList(),
                Reviewer = reviewer,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            await _unitOfWork.ReviewRepository.AddAsync(review, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ReviewDto>(review);
        }
    }
}