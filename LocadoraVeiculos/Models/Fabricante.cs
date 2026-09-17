using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models
{
    public class Fabricante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O país de origem é obrigatório.")]
        public string PaisOrigem { get; set; } = string.Empty;

        public ICollection<Veiculo>? Veiculos { get; set; }
    }
}