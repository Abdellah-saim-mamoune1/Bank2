using System.ComponentModel.DataAnnotations;

namespace bankApI.BusinessLayer.Dto_s
{
    public class DClientWithSAccountG
    {

        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateOnly BirthDate { get; set; }
        [Required]
        public string PersonalEmail { get; set; }
        [Required, MaxLength(100)]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string RoleType { get; set; }

        [Required]
        public DAccountGet BankEmail { get; set; } = new();

        [Required]
        public DCardGet Card { get; set; } = new();

        public bool IsActive { get; set; }




    }
}
