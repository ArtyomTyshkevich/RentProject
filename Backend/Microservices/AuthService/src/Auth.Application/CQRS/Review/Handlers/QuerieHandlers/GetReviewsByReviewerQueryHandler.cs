using AutoMapper;
using Library.Application.Interfaces;
using MediatR;
using User.BLL.CQRS.Reviews.Queries;
using User.BLL.DTOs.Review;

namespace User.BLL.CQRS.Reviews.Handlers.QueryHandlers
{
    public class GetReviewsByReviewerQueryHandler
        : IRequestHandler<GetReviewsByReviewerQuery, IReadOnlyList<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReviewsByReviewerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ReviewDto>> Handle(
            GetReviewsByReviewerQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.ReviewRepository
                .GetByReviewerIdAsync(request.ReviewerId, cancellationToken);
            return _mapper.Map<List<ReviewDto>>(reviews);
        }
    }
}