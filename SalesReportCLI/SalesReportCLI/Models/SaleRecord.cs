namespace SalesReportCLI.Models
{
    public class SaleRecord
    {
        // Ajuste de acordo com as colunas do CSV
        public DateTime Data { get; set; }
        public string Produto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        // Propriedade calculada
        public decimal TotalPrice => Quantidade * PrecoUnitario;
    }
}
