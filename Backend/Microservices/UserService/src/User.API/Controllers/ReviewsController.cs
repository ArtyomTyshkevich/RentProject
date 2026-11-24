using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using User.BLL.CQRS.Reviews.Commands;
using User.BLL.CQRS.Reviews.Queries;
using User.BLL.DTOs.Review;

namespace User.API.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Список отзывов с пагинацией.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ReviewDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<ReviewDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
                new GetAllReviewsQuery { Page = page, PageSize = pageSize },
                cancellationToken);

            return Ok(result);
        }

        /// <summary>Отзыв по Id.</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewDto>> GetById(
            Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
                new GetReviewByIdQuery { Id = id }, cancellationToken);

            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Отзывы конкретного пользователя.</summary>
        [HttpGet("by-reviewer/{reviewerId:guid}")]
        [ProducesResponseType(typeof(IReadOnlyList<ReviewDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ReviewDto>>> GetByReviewer(
            Guid reviewerId, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
                new GetReviewsByReviewerQuery { ReviewerId = reviewerId }, cancellationToken);

            return Ok(result);
        }

        /// <summary>Создать отзыв. Автор берётся из токена.</summary>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReviewDto>> Create(
            [FromBody] CreateReviewCommand command,
            CancellationToken cancellationToken = default)
        {
            var reviewerId = GetCurrentUserId();
            if (reviewerId is null)
                return Unauthorized();

            // Автора всегда берём из токена — игнорируем то, что пришло в теле
            command.ReviewerId = reviewerId.Value;

            var result = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Обновить отзыв. Только автор.</summary>
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<ReviewDto>> Update(
            Guid id,
            [FromBody] UpdateReviewCommand command,
            CancellationToken cancellationToken = default)
        {
            if (id != command.Id)
                return BadRequest("Id в маршруте и в теле не совпадают");

            // Проверка владельца — в хендлере или здесь
            if (!await IsOwnerOrAdminAsync(id, cancellationToken))
                return Forbid();

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>Удалить отзыв. Только автор или админ.</summary>
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(
            Guid id, CancellationToken cancellationToken = default)
        {
            if (!await IsOwnerOrAdminAsync(id, cancellationToken))
                return Forbid();

            await _mediator.Send(new DeleteReviewCommand { Id = id }, cancellationToken);
            return NoContent();
        }

        // ---------- helpers ----------

        private Guid? GetCurrentUserId()
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub");

            return Guid.TryParse(value, out var id) ? id : null;
        }

        private async Task<bool> IsOwnerOrAdminAsync(Guid reviewId, CancellationToken ct)
        {
            if (User.IsInRole("Admin"))
                return true;

            var currentUserId = GetCurrentUserId();
            if (currentUserId is null)
                return false;

            var review = await _mediator.Send(new GetReviewByIdQuery { Id = reviewId }, ct);
            if (review is null)
                return true;

            return review.Reviewer.Id == currentUserId.Value;
        }
    }
}