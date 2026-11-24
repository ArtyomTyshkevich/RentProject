using AutoMapper;
using Library.Application.Interfaces;
using MediatR;
using User.BLL.CQRS.Reviews.Queries;
using User.BLL.DTOs.Review;
using User.BLL.Interfaces;

namespace User.BLL.CQRS.Reviews.Handlers.QueryHandlers
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReviewByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReviewDto?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(request.Id, cancellationToken);
            return review is null ? null : _mapper.Map<ReviewDto>(review);
        }
    }
}