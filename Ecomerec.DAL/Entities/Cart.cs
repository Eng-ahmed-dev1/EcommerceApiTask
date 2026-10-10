using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Ecomerec.DAL
{
    public class Cart
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<CartItem> cartItems = new HashSet<CartItem>();

    }
}