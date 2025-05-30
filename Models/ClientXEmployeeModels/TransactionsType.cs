using System.ComponentModel.DataAnnotations;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class TransactionsType
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(20)]
        public string Type { get; set; }

        public List<TransactionsHistory>? TransactionHistory { get; set; }
    }
}
