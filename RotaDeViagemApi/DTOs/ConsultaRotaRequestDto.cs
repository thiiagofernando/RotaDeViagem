using System.ComponentModel.DataAnnotations;

namespace RotaDeViagemApi.DTOs
{
    public class ConsultaRotaRequestDto
    {
        [Required(ErrorMessage = "A origem é obrigatória")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "A origem deve ter exatamente 3 caracteres")]
        public string Origem { get; set; }

        [Required(ErrorMessage = "O destino é obrigatório")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "O destino deve ter exatamente 3 caracteres")]
        public string Destino { get; set; }
    }
}
