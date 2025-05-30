using System.ComponentModel.DataAnnotations;

namespace bankApI.BusinessLayer.Dto_s
{
    public class DPerson
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateOnly BirthDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; } 
    }
}
