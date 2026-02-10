using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Json;

namespace TesteItau
{
    [ApiController]
    [Route("api/[controller]")]
    public class CotacoesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        public CotacoesController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cotacao>>> Get() => await _context.Cotacoes.ToListAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<Cotacao>> Get(int id)
        {
            var item = await _context.Cotacoes.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }
        [HttpPost]
        public async Task<ActionResult<Cotacao>> Post(Cotacao obj)
        {
            _context.Cotacoes.Add(obj);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = obj.PkCotacao }, obj);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Cotacao obj)
        {
            if (id != obj.PkCotacao) return BadRequest();
            _context.Entry(obj).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cotacoes.Any(e => e.PkCotacao == id)) return NotFound();
                else throw;
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Cotacoes.FindAsync(id);
            if (item == null) return NotFound();
            _context.Cotacoes.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("ObterCotacoes")]
        public async Task<IActionResult> ObterCotacoes([FromQuery] string? tickers = null)
        {
            var client = _httpClientFactory.CreateClient();
            string url = "https://b3api.vercel.app/api/Assets/";
            if (!string.IsNullOrEmpty(tickers))
                url += $"?tickers={tickers}";
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            var cotacoesB3 = await response.Content.ReadFromJsonAsync<List<B3CotacaoDto>>();
            if (cotacoesB3 == null)
                return BadRequest("Não foi possível obter as cotações da B3.");

            // Atualiza a base de dados tb_cotacao
            foreach (var cotacao in cotacoesB3)
            {
                var ativo = await _context.Ativos.FirstOrDefaultAsync(a => a.DsCodigo == cotacao.ticker);
                if (ativo == null) continue;
                var novaCotacao = new Cotacao
                {
                    FkAtivo = ativo.PkAtivo,
                    VlPrecoUnitario = cotacao.price ?? 0,
                    DtCotacao = cotacao.tradetime ?? DateTime.Now
                };
                _context.Cotacoes.Add(novaCotacao);
            }
            await _context.SaveChangesAsync();
            return Ok(cotacoesB3);
        }

        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<Cotacao>>> ObterTodos()
        {
            return await _context.Cotacoes.ToListAsync();
        }
    }

    public class B3CotacaoDto
    {
        public string ticker { get; set; } = string.Empty;
        public decimal? price { get; set; }
        public decimal? priceopen { get; set; }
        public decimal? high { get; set; }
        public decimal? low { get; set; }
        public long? volume { get; set; }
        public long? marketcap { get; set; }
        public DateTime? tradetime { get; set; }
        public long? volumeavg { get; set; }
        public decimal? pe { get; set; }
        public decimal? eps { get; set; }
        public decimal? high52 { get; set; }
        public decimal? low52 { get; set; }
        public decimal? change { get; set; }
        public decimal? changepct { get; set; }
        public decimal? closeyest { get; set; }
        public long? shares { get; set; }
    }
}
