using System.ComponentModel.DataAnnotations;

namespace Climate_Watch.Models
{
    public class Users {
        [Key] public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Telegram Id")]
        public string TelegramId { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Register date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime RegisterDate { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsSuperAdmin { get; set; }

    }
}
