using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.EmployeeModels
{
    public class LoginRegistre
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
