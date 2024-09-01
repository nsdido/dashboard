using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Climate_Watch.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }

       
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsFinaly { get; set; }





        [ForeignKey("UserId")]
        public Users Users { get; set; }

        public List<OrderDetail> OrderDatail { get; set; }
    }
}
