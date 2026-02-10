using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TesteItau
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperacoesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OperacoesController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operacao>>> Get() => await _context.Operacoes.ToListAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<Operacao>> Get(int id)
        {
            var item = await _context.Operacoes.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }
        [HttpPost]
        public async Task<ActionResult<Operacao>> Post(Operacao obj)
        {
            _context.Operacoes.Add(obj);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = obj.PkOperacao }, obj);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Operacao obj)
        {
            if (id != obj.PkOperacao) return BadRequest();
            _context.Entry(obj).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Operacoes.Any(e => e.PkOperacao == id)) return NotFound();
                else throw;
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Operacoes.FindAsync(id);
            if (item == null) return NotFound();
            _context.Operacoes.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<Operacao>>> ObterTodos()
        {
            return await _context.Operacoes.ToListAsync();
        }
    }
}
