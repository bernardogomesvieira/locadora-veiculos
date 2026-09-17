using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Data;
using LocadoraVeiculos.Models;

namespace LocadoraVeiculos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasVeiculoController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CategoriasVeiculoController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaVeiculo>>> GetCategorias()
        {
            return await _context.CategoriasVeiculo.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaVeiculo>> GetCategoria(int id)
        {
            var categoria = await _context.CategoriasVeiculo.FindAsync(id);
            if (categoria == null) return NotFound();
            return categoria;
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaVeiculo>> PostCategoria(CategoriaVeiculo categoria)
        {
            _context.CategoriasVeiculo.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, CategoriaVeiculo categoria)
        {
            if (id != categoria.Id) return BadRequest();
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.CategoriasVeiculo.FindAsync(id);
            if (categoria == null) return NotFound();
            _context.CategoriasVeiculo.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
