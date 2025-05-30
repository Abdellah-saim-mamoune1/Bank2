using bankApI.Models.ClientXEmployeeModels;
using bankApI.Models.EmployeeModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientModels
{
    public class Client
    {
        [Key, ForeignKey("Person")]
        public int PersonId { get; set; }

      

        [ForeignKey("Role")]
        public int TypeId { get; set; }


        [ForeignKey("Access")]

        public int AccessId { get; set; }

    
        public DateOnly CreatedAt { get; set; }


        public bool IsActive { get; set; }



        public Person Person { get; set; }

        public Access Access { get; set; }


        public Role Role { get; set; }
        
        public List<TransactionRegistre> SentTransactions { get; set; } = new();
        public List<TransactionRegistre> ReceivedTransactions { get; set; } = new();
      

    }
}
