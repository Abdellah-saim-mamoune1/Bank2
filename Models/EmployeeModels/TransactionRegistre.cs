using bankApI.Models.ClientModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.EmployeeModels
{
    public class TransactionRegistre
    {
        public int Id { get; set; }
        public double Amount { get; set; }

        public DateOnly Date { get; set; }
        [ForeignKey("SenderClient")]
        public int SenderClientId { get; set; }
        public Client SenderClient { get; set; }

        // Receiver Client Relationship
        [ForeignKey("ReceiverClient")]
        public int ReceiverClientId { get; set; }
        public Client ReceiverClient { get; set; }
    }
}
