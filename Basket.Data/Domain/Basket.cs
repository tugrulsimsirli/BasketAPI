using System.ComponentModel.DataAnnotations.Schema;

namespace Basket.Data.Domain
{
    public class Basket
    {
        public int BasketId { get; set; }
        [ForeignKey("BasketId")]
        public virtual BasketProduct BasketProducts { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}