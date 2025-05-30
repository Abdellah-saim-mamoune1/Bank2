using bankApI.Models.ClientModels;
using bankApI.Models.EmployeeModels;
using System.ComponentModel.DataAnnotations;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class Role
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        public List<Client> ? Clients { get; set; } = new();

        public List<Employee>? Employees { get; set; } = new();
    }
}
