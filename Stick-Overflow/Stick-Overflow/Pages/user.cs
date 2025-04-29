using System.ComponentModel.DataAnnotations;

namespace Stick_Overflow.Pages
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Il nickname deve essere compreso tra i 3 e 20 caratteri")]
        public string name { get; set; }

        [StringLength(255, MinimumLength = 8, ErrorMessage = "La password deve essere compresa tra gli 8 e i 255 caratteri")]
        public string password { get; set; }

        [StringLength(50, MinimumLength = 6, ErrorMessage = "L'email deve essere compresa tra i 50 e i 6 caratteri")]
        public string email { get; set; }

        public bool abilitato { get; set; }
    }
}
