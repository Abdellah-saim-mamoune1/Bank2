using bankApI.Models.ClientManagement;
using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.EmployeeModels
{
    public class EmployeeAccount
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Person")]
        public  int EmployeeId { get; set; }
        [Required]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }

        public bool IsFrozen { get; set; }

        public Person Person { get; set; }
        public List<TransactionsRegistre> Deposits { get; set; } = new();
        public List<EmployeeNotifications>? EmployeeNotifications { get; set; }
    }
}
