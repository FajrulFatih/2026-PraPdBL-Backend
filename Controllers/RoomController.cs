using Microsoft.AspNetCore.Mvc;
using PraPdBL_Backend.Common.Responses;
using PraPdBL_Backend.DTOs;
using PraPdBL_Backend.Models;
using PraPdBL_Backend.Services.Interfaces;

[ApiController]
[Route("api/rooms")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _service;

    public RoomController(IRoomService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (room, conflict) = await _service.CreateAsync(dto);
        if (conflict) return Conflict(new { message = "Room code sudah digunakan." });
        if (room == null) return BadRequest();
        return CreatedAtAction(nameof(Get), new { id = room.Id }, room);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (total, data) = await _service.GetAllAsync(page, pageSize);
        return Ok(new PagedResponse<Room>(total, page, pageSize, data));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var room = await _service.GetByIdAsync(id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoomCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var (updated, conflict) = await _service.UpdateAsync(id, dto);
        if (conflict) return Conflict(new { message = "Room code sudah digunakan." });
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpPut("{id}/restore")]
    public async Task<IActionResult> Restore(int id)
    {
        var (restored, conflict) = await _service.RestoreAsync(id);
        if (conflict) return Conflict(new { message = "Room code sudah digunakan." });
        if (!restored) return NotFound();
        return Ok();
    }
}
