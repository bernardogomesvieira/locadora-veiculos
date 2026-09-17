using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models
{
    public class CategoriaVeiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "O valor da diária não pode ser negativo.")]
        public decimal ValorDiariaBase { get; set; }

        public ICollection<Veiculo>? Veiculos { get; set; }
    }
}