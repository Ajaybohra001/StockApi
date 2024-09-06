using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWebApi.Model
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName ="decimal(18,2)")]
        public decimal LastDiv {  get; set; }
        public string? Industry {  get; set; }

        public long MarketCap {  get; set; }

        public virtual ICollection<Comment>? Comment { get; set; }

    }
}
