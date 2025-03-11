namespace RotaDeViagemApi.DTOs
{
    public class ConsultaRotaResponseDto
    {
        public List<string> Trajeto { get; set; }
        public decimal CustoTotal { get; set; }
        public string RotaFormatada => $"{string.Join(" - ", Trajeto)} ao custo de ${CustoTotal}";
    }
}
