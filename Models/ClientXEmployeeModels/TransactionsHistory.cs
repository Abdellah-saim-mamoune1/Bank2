using bankApI.Models.ClientModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class TransactionsHistory
    {
        [Key]
       public  int Id { get; set; }

        [Required, ForeignKey("Account")]
        public int  AccountId { get; set; }
        [Required,ForeignKey("TransactionsType")]
       public int TypeId { get; set; }

        public double Amount { get; set; }

        public DateOnly Date { get; set; }
       
        public TransactionsType TransactionsType { get; set; }

        public Account Account { get; set; }

    }
}
