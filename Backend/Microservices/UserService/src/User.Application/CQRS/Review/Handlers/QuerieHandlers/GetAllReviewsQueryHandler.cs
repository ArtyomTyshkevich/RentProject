using AutoMapper;
using Library.Application.Interfaces;
using MediatR;
using User.BLL.CQRS.Reviews.Queries;
using User.BLL.DTOs.Review;
using User.BLL.Interfaces;

namespace User.BLL.CQRS.Reviews.Handlers.QueryHandlers
{
    public class GetAllReviewsQueryHandler
        : IRequestHandler<GetAllReviewsQuery, PagedResult<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReviewDto>> Handle(
            GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            var all = await _unitOfWork.ReviewRepository.GetAllAsync(cancellationToken);
            var total = all.Count;
            var items = all
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new PagedResult<ReviewDto>
            {
                Items = _mapper.Map<List<ReviewDto>>(items),
                TotalCount = total,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}