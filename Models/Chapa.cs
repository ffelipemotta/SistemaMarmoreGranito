using System.ComponentModel.DataAnnotations;

namespace SistemaMamoreGranito.Models
{
    public class Chapa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do material é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        [Display(Name = "Nome do Material")]
        public string? NomeMaterial { get; set; }

        [Required(ErrorMessage = "O tipo do material é obrigatório")]
        [Display(Name = "Tipo de Material")]
        public string? TipoMaterial { get; set; } // Mármore ou Granito

        [Required(ErrorMessage = "A espessura é obrigatória")]
        [Range(0.1, 100, ErrorMessage = "A espessura deve estar entre 0.1 e 100")]
        [Display(Name = "Espessura (cm)")]
        public decimal Espessura { get; set; }

        [Required(ErrorMessage = "A altura é obrigatória")]
        [Range(0.1, 1000, ErrorMessage = "A altura deve estar entre 0.1 e 1000")]
        [Display(Name = "Altura (cm)")]
        public decimal Altura { get; set; }

        [Required(ErrorMessage = "A largura é obrigatória")]
        [Range(0.1, 1000, ErrorMessage = "A largura deve estar entre 0.1 e 1000")]
        [Display(Name = "Largura (cm)")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "O peso é obrigatório")]
        [Range(0.1, 100000, ErrorMessage = "O peso deve estar entre 0.1 e 100000")]
        [Display(Name = "Peso (kg)")]
        public decimal Peso { get; set; }

        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }

        [Required(ErrorMessage = "A data de entrada é obrigatória")]
        [Display(Name = "Data de Entrada")]
        [DataType(DataType.Date)]
        public DateTime DataEntrada { get; set; } = DateTime.Now;

        [Display(Name = "Disponível")]
        public bool Disponivel { get; set; } = true;

        [Display(Name = "Bloco de Origem")]
        public int? BlocoId { get; set; }
        public Bloco? BlocoOrigem { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        [Display(Name = "Valor (R$)")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
    }
} 