using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaMamoreGranito.Models
{
    public class ProcessoSerragem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BlocoId { get; set; }

        [ForeignKey("BlocoId")]
        public Bloco? Bloco { get; set; }

        [Required(ErrorMessage = "A espessura das chapas é obrigatória")]
        [Range(0.1, 100, ErrorMessage = "A espessura deve estar entre 0.1 e 100 cm")]
        [Display(Name = "Espessura da Chapa (cm)")]
        public decimal EspessuraChapa { get; set; }

        [Required(ErrorMessage = "A quantidade de chapas é obrigatória")]
        [Range(1, 1000, ErrorMessage = "A quantidade deve estar entre 1 e 1000")]
        [Display(Name = "Quantidade de Chapas")]
        public int QuantidadeChapas { get; set; }

        [Required(ErrorMessage = "A data do processo é obrigatória")]
        [Display(Name = "Data do Processo")]
        public DateTime DataProcesso { get; set; } = DateTime.Now;

        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }

        [NotMapped]
        public decimal VolumeTotalChapas => Bloco != null ? EspessuraChapa * Bloco.Largura * Bloco.Comprimento * QuantidadeChapas : 0;

        [NotMapped]
        public decimal PesoTotalChapas => Bloco != null ? (VolumeTotalChapas / 1000000) * 2700 : 0; // Densidade média do mármore/granito (2.7 g/cm³)

        [NotMapped]
        public int QuantidadeMaximaChapas => Bloco != null ? (int)(Bloco.Altura / EspessuraChapa) : 0;

        [NotMapped]
        public decimal VolumeBloco => Bloco != null ? Bloco.Altura * Bloco.Largura * Bloco.Comprimento : 0;
    }
} 