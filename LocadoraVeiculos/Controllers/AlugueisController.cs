using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Data;
using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlugueisController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AlugueisController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueis()
        {
            return await _context.Alugueis.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluguel>> GetAluguel(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null) return NotFound();
            return aluguel;
        }

        [HttpPost]
        public async Task<ActionResult<Aluguel>> PostAluguel(Aluguel aluguel)
        {
            _context.Alugueis.Add(aluguel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAluguel), new { id = aluguel.Id }, aluguel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluguel(int id, Aluguel aluguel)
        {
            if (id != aluguel.Id) return BadRequest();
            _context.Entry(aluguel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluguel(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null) return NotFound();
            _context.Alugueis.Remove(aluguel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("por-cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueisPorCliente(int clienteId)
        {
            var alugueis = await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .Where(a => a.ClienteId == clienteId)
                .ToListAsync();

            return alugueis;
        }

        [HttpGet("por-periodo")]
public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueisPorPeriodo(DateTime dataInicio, DateTime dataFim)
{
    var alugueis = await _context.Alugueis
        .Include(a => a.Cliente)
        .Include(a => a.Veiculo)
        .Where(a => a.DataInicio >= dataInicio && a.DataInicio <= dataFim)
        .ToListAsync();

    return alugueis;
        }
    }
}