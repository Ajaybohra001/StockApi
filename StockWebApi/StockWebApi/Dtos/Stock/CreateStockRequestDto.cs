using System.ComponentModel.DataAnnotations;

namespace StockWebApi.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        public string? Symbol { get; set; }
        [Required]
        public string? CompanyName { get; set; }
        [Required]
        [Range(0.001,100)]
        public decimal LastDiv { get; set; }
        [Required]
        public string? Industry { get; set; }
        [Range(1,100000)]

        public long MarketCap { get; set; }
    }
}
