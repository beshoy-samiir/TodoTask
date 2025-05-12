using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo_backend.Data;
using Todo_backend.Models;

namespace Todo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Status? status)
        {
            var todos = _context.Todos.AsQueryable();

            if (status.HasValue)
                todos = todos.Where(t => t.Status == status);

            return Ok(await todos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoModel todo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            todo.Id = Guid.NewGuid();
            todo.CreatedAt = DateTime.UtcNow;
            todo.LastModifiedDate = DateTime.UtcNow;

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TodoModel updated)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return NotFound();

            todo.Title = updated.Title;
            todo.Description = updated.Description;
            todo.Status = updated.Status;
            todo.Priority = updated.Priority;
            todo.DueDate = updated.DueDate;
            todo.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return NotFound();

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> MarkComplete(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null) return NotFound();

            todo.Status = Status.Completed;
            todo.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


