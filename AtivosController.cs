using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TesteItau
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtivosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AtivosController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ativo>>> Get() => await _context.Ativos.ToListAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<Ativo>> Get(int id)
        {
            var item = await _context.Ativos.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }
        [HttpPost]
        public async Task<ActionResult<Ativo>> Post(Ativo obj)
        {
            _context.Ativos.Add(obj);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = obj.PkAtivo }, obj);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Ativo obj)
        {
            if (id != obj.PkAtivo) return BadRequest();
            _context.Entry(obj).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Ativos.Any(e => e.PkAtivo == id)) return NotFound();
                else throw;
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Ativos.FindAsync(id);
            if (item == null) return NotFound();
            _context.Ativos.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<Ativo>>> ObterTodos()
        {
            return await _context.Ativos.ToListAsync();
        }
        [HttpGet("UltimaCotacao/{codigoAtivo}")]
        public async Task<ActionResult<Cotacao>> UltimaCotacao(string codigoAtivo)
        {
            var ativo = await _context.Ativos.FirstOrDefaultAsync(a => a.DsCodigo == codigoAtivo);
            if (ativo == null)
                return NotFound($"Ativo '{codigoAtivo}' não encontrado.");
            var cotacao = await _context.Cotacoes
                .Where(c => c.FkAtivo == ativo.PkAtivo)
                .OrderByDescending(c => c.DtCotacao)
                .FirstOrDefaultAsync();
            if (cotacao == null)
                return NotFound($"Nenhuma cotação encontrada para o ativo '{codigoAtivo}'.");
            return Ok(cotacao);
        }

        [HttpGet("PrecoMedioPorAtivo/{usuarioId}/{codigoAtivo}")]
        public async Task<ActionResult<decimal>> PrecoMedioPorAtivo(int usuarioId, string codigoAtivo)
        {
            var ativo = await _context.Ativos.FirstOrDefaultAsync(a => a.DsCodigo == codigoAtivo);
            if (ativo == null)
                return NotFound($"Ativo '{codigoAtivo}' não encontrado.");
            var posicao = await _context.Posicoes.FirstOrDefaultAsync(p => p.FkUsuario == usuarioId && p.FkAtivo == ativo.PkAtivo);
            if (posicao == null)
                return NotFound($"Nenhuma posição encontrada para o usuário {usuarioId} e ativo '{codigoAtivo}'.");
            return Ok(posicao.VlPrecoMedio);
        }

        [HttpGet("PosicaoCliente/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Posicao>>> PosicaoCliente(int usuarioId)
        {
            var posicoes = await _context.Posicoes.Where(p => p.FkUsuario == usuarioId).ToListAsync();
            if (!posicoes.Any())
                return NotFound($"Nenhuma posição encontrada para o usuário {usuarioId}.");
            return Ok(posicoes);
        }

        [HttpGet("ValorTotalCorretagem")]
        public async Task<ActionResult<decimal>> ValorTotalCorretagem()
        {
            var total = await _context.Operacoes.SumAsync(o => o.VlCorretagem);
            return Ok(total);
        }

        [HttpGet("Top10ClientesMaiorPosicao")]
        public async Task<ActionResult<IEnumerable<object>>> Top10ClientesMaiorPosicao()
        {
            var top = await _context.Posicoes
                .GroupBy(p => p.FkUsuario)
                .Select(g => new { UsuarioId = g.Key, QuantidadeTotal = g.Sum(p => p.NrQuantidadeTotal) })
                .OrderByDescending(x => x.QuantidadeTotal)
                .Take(10)
                .ToListAsync();
            return Ok(top);
        }

        [HttpGet("Top10ClientesMaisCorretagem")]
        public async Task<ActionResult<IEnumerable<object>>> Top10ClientesMaisCorretagem()
        {
            var top = await _context.Operacoes
                .GroupBy(o => o.FkUsuario)
                .Select(g => new { UsuarioId = g.Key, TotalCorretagem = g.Sum(o => o.VlCorretagem) })
                .OrderByDescending(x => x.TotalCorretagem)
                .Take(10)
                .ToListAsync();
            return Ok(top);
        }
    }
}
