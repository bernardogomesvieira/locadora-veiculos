using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Data;
using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public VeiculosController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos()
        {
            return await _context.Veiculos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null) return NotFound();
            return veiculo;
        }

        [HttpPost]
        public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVeiculo), new { id = veiculo.Id }, veiculo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculo(int id, Veiculo veiculo)
        {
            if (id != veiculo.Id) return BadRequest();
            _context.Entry(veiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null) return NotFound();
            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("por-fabricante/{fabricanteId}")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculosPorFabricante(int fabricanteId)
        {
            var veiculos = await _context.Veiculos
                .Include(v => v.Fabricante)
                .Include(v => v.CategoriaVeiculo)
                .Where(v => v.FabricanteId == fabricanteId)
                .ToListAsync();

            return veiculos;
        }

        [HttpGet("disponiveis")]
public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculosDisponiveis()
{
    var veiculos = await _context.Veiculos
        .GroupJoin(
            _context.Alugueis.Where(a => a.DataDevolucao == null),
            v => v.Id,
            a => a.VeiculoId,
            (v, alugueis) => new { Veiculo = v, Alugueis = alugueis }
        )
        .Where(x => !x.Alugueis.Any())
        .Select(x => x.Veiculo)
        .ToListAsync();

    return veiculos;
          }
          [HttpGet("relatorio-completo")]
public async Task<ActionResult<IEnumerable<object>>> GetRelatorioCompleto()
{
    var relatorio = await _context.Veiculos
        .Join(_context.Fabricantes,
            v => v.FabricanteId,
            f => f.Id,
            (v, f) => new { v, f })
        .Join(_context.CategoriasVeiculo,
            vf => vf.v.CategoriaVeiculoId,
            c => c.Id,
            (vf, c) => new
            {
                VeiculoId = vf.v.Id,
                Modelo = vf.v.Modelo,
                AnoFabricacao = vf.v.AnoFabricacao,
                Quilometragem = vf.v.Quilometragem,
                Fabricante = vf.f.Nome,
                Categoria = c.Nome,
                ValorDiariaBase = c.ValorDiariaBase
            })
        .ToListAsync();

    return relatorio;
        }
    }
    
}