using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TesteItau
{
    [ApiController]
    [Route("api/[controller]")]
    public class PosicoesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PosicoesController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posicao>>> Get() => await _context.Posicoes.ToListAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<Posicao>> Get(int id)
        {
            var item = await _context.Posicoes.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }
        [HttpPost]
        public async Task<ActionResult<Posicao>> Post(Posicao obj)
        {
            _context.Posicoes.Add(obj);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = obj.PkPosicao }, obj);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Posicao obj)
        {
            if (id != obj.PkPosicao) return BadRequest();
            _context.Entry(obj).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Posicoes.Any(e => e.PkPosicao == id)) return NotFound();
                else throw;
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Posicoes.FindAsync(id);
            if (item == null) return NotFound();
            _context.Posicoes.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<Posicao>>> ObterTodos()
        {
            return await _context.Posicoes.ToListAsync();
        }
    }
}
