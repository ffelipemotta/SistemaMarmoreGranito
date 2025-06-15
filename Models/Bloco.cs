using System.ComponentModel.DataAnnotations;

namespace SistemaMamoreGranito.Models
{
    public class Bloco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do material é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string? NomeMaterial { get; set; }

        [Required(ErrorMessage = "O tipo do material é obrigatório")]
        public string? TipoMaterial { get; set; } // Mármore ou Granito

        [Required(ErrorMessage = "A altura é obrigatória")]
        [Range(0.1, 1000, ErrorMessage = "A altura deve estar entre 0.1 e 1000")]
        public decimal Altura { get; set; }

        [Required(ErrorMessage = "A largura é obrigatória")]
        [Range(0.1, 1000, ErrorMessage = "A largura deve estar entre 0.1 e 1000")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "O comprimento é obrigatório")]
        [Range(0.1, 1000, ErrorMessage = "O comprimento deve estar entre 0.1 e 1000")]
        public decimal Comprimento { get; set; }

        [Required(ErrorMessage = "O peso é obrigatório")]
        [Range(0.1, 100000, ErrorMessage = "O peso deve estar entre 0.1 e 100000")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "A pedreira de origem é obrigatória")]
        public string? PedreiraOrigem { get; set; }

        [Required(ErrorMessage = "O número da nota fiscal é obrigatório")]
        public string? NumeroNotaFiscal { get; set; }

        [Required(ErrorMessage = "O valor de compra é obrigatório")]
        [Range(0.01, 1000000, ErrorMessage = "O valor deve estar entre 0.01 e 1000000")]
        public decimal ValorCompra { get; set; }

        public string? Observacoes { get; set; }
        public DateTime DataEntrada { get; set; } = DateTime.Now;
        public bool Disponivel { get; set; } = true;
    }
} 