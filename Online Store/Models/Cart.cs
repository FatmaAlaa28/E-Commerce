using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Online_Store.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Quantity should contain numbers only")]
        public int Quantity { get; set; }

        public int UserId { get; set; }
        //public int ProductCartId { get; set; }

        public ICollection<ProductCart> ProductCards { get; set; }

        public virtual User Users { get; set; }
    }
}
