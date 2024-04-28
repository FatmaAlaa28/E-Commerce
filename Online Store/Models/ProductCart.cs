using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Online_Store.Models
{
    public class ProductCart
    {
        [Key]
        public int ProductCartId { get; set; }

        public int CartId { get; set; }
        public int ProductId { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
