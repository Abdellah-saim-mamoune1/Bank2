using bankApI.Models.ClientModels;
using bankApI.Models.EmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class Person
    {

        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }

        
        public DateOnly BirthDate { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Range(1,22,ErrorMessage ="Phone number size cannot be above 22 number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public Employee ?Employee { get; set; }
      
        public  Client? Client { get; set; }
        public List<Account> Accounts { get; set; }

        public EmployeeAccount? EmployeeAccount { get; set; }
    }
}
