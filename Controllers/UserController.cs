using Microsoft.AspNetCore.Mvc;
using PraPdBL_Backend.DTOs;
using PraPdBL_Backend.Services.Interfaces;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (user, conflict) = await _service.CreateAsync(dto);
        if (conflict) return Conflict(new { message = "Email sudah digunakan." });
        if (user == null) return BadRequest();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
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
        var user = await _service.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var (updated, conflict) = await _service.UpdateAsync(id, dto);
        if (conflict) return Conflict(new { message = "Email sudah digunakan." });
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
        if (conflict) return Conflict(new { message = "Email sudah digunakan." });
        if (!restored) return NotFound();
        return Ok();
    }
}
