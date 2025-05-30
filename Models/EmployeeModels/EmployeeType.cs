using System.ComponentModel.DataAnnotations;

namespace bankApI.Models.EmployeeModels
{
    public class EmployeeType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }

        public List <Employee> Employees { get; set; }=new ();
    }
}
