using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models
{
    public class Aluguel
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int VeiculoId { get; set; }
        public Veiculo? Veiculo { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime DataInicio { get; set; }

        public DateTime? DataDevolucao { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Quilometragem inicial não pode ser negativa.")]
        public double QuilometragemInicial { get; set; }

        public double? QuilometragemFinal { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Valor da diária não pode ser negativo.")]
        public decimal ValorDiaria { get; set; }

        public decimal? ValorTotal { get; set; }
    }
}