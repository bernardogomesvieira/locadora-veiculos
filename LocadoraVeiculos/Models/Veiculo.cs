using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Modelo { get; set; } = string.Empty;

        [Range(1900, 2100, ErrorMessage = "Ano de fabricação inválido.")]
        public int AnoFabricacao { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Quilometragem não pode ser negativa.")]
        public double Quilometragem { get; set; }

        public int FabricanteId { get; set; }
        public Fabricante? Fabricante { get; set; }

        public int CategoriaVeiculoId { get; set; }
        public CategoriaVeiculo? CategoriaVeiculo { get; set; }

        public ICollection<Aluguel>? Alugueis { get; set; }
    }
}