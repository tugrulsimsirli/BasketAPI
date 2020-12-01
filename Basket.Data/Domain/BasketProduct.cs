using System.ComponentModel.DataAnnotations;

namespace Basket.Data.Domain
{
    public class BasketProduct
    {
        [Key]
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
    }
}