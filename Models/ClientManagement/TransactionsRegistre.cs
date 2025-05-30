using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bankApI.Models.ClientModels;
using bankApI.Models.EmployeeModels;

namespace bankApI.Models.ClientManagement
{
    public class TransactionsRegistre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeAccountId { get; set; }

        [Required]
        public int ClientAccountId { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public string type { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [ForeignKey(nameof(ClientAccountId))]
        public Account ClientAccount { get; set; }

        [ForeignKey(nameof(EmployeeAccountId))]
        public EmployeeAccount EmployeeAccount { get; set; }
    }
}
