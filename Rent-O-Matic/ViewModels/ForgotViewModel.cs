using System.ComponentModel.DataAnnotations;

namespace Rent_O_Matic.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}