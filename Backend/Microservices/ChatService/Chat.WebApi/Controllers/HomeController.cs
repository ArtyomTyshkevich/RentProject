using Chat.Application.DTOs;
using Chat.Application.Services;
using Chat.Applicaton.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatRoomsController : ControllerBase
    {
        private readonly IChatRoomService _service;

        public ChatRoomsController(IChatRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatRoomDto>>> GetAll(CancellationToken cancellationToken)
        {
            var rooms = await _service.GetAllAsync(cancellationToken);
            return Ok(rooms);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ChatRoomDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var room = await _service.GetByIdAsync(id, cancellationToken);
            if (room is null)
                return NotFound();

            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<ChatRoomDto>> Create(ChatRoomDto dto, CancellationToken cancellationToken)
        {
            var created = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ChatRoomDto>> Update(Guid id, ChatRoomDto dto, CancellationToken cancellationToken)
        {
            dto.Id = id;
            var updated = await _service.UpdateAsync(dto, cancellationToken);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _service.DeleteAsync(id, cancellationToken);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}