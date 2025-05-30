using bankApI.BusinessLayer.Services.ClientServer.IClient;
using bankApI.Models.ClientManagement;
using bankApI.Models.ClientXEmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientModels
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AccountAddress { get; set; }

        [Required]

        public string PassWord { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        [Required]

        [ForeignKey("Card")]
        public int CardId { get; set; }
        [Required]
        public double Balance { get; set; }


        [Required]
        public bool IsFrozen { get; set; }

        [Required]
        public DateOnly CreatedAt { get; set; }
        public Person Person { get; set; }
        public Card Card { get; set; }
        public List<TransactionsHistory> AccountTransactionHistory { get; set; }
        public List <TransferFundHistory>? SenderAccount { get; set; }
        public List<TransferFundHistory>? RecieverAccount { get; set; }
        public List<ClientXNotifications>? clientXNotifications { get; set; }
        public List<TransactionsRegistre> Deposits { get; set; } = new();
        public List<GetHelp>? GetHelp { get; set; }


    }

}
