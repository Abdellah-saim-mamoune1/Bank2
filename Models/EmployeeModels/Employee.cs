using bankApI.Models.ClientManagement;
using bankApI.Models.ClientXEmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace bankApI.Models.EmployeeModels
{
    public class Employee
    {
       
        [Key, ForeignKey("Person")]
        public int PersonId { get; set; }
     

        [Required]
        public double salary { get; set; }

        [ForeignKey("EmployeeType")]

        public int TypeId { get; set; }


        [ForeignKey("Role")]
        public int RoleTypeId { get; set; }

        [ForeignKey("Access")]
        public int AccessId { get; set; }


       

        public bool IsActive { get; set; }

       
      
        public DateOnly CreatedAt { get; set; }

        public Person Person { get; set; }

       
        public Access Access { get; set; }

     

       
        public Role Role { get; set; }

      
        public EmployeeType EmployeeType { get; set; }


    }
}
