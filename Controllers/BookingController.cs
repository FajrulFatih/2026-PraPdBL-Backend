using Microsoft.AspNetCore.Mvc;
using PraPdBL_Backend.DTOs;
using PraPdBL_Backend.Services.Interfaces;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _service;

    public BookingController(IBookingService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookingCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var booking = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = booking.Id }, booking);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (total, data) = await _service.GetAllAsync(page, pageSize);
        return Ok(new { total, page, pageSize, data });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var booking = await _service.GetByIdAsync(id);
        if (booking == null) return NotFound();
        return Ok(booking);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookingCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, dto);
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
}
