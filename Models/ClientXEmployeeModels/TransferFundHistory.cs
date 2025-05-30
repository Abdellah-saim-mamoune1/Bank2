using bankApI.Models.ClientModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class TransferFundHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SenderAccountId { get; set; }

        [Required]
        public int RecieverAccountId { get; set; }
        [Required]
        public double Amount { get; set; }
        public string? Description { get; set; }
        public DateOnly Date { get; set; }

        [ForeignKey(nameof(SenderAccountId))]
        public Account SenderAccountIds { get; set; }

        [ForeignKey(nameof(RecieverAccountId))]
        public Account RecieverAccountIds { get; set; }
    }
}
