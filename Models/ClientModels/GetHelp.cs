using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientModels
{
    public class GetHelp
    {
        [Key]
        public int Id { get; set; }

        [Required,ForeignKey("Account")]
        public int ClientAccountId { get; set; }
        [Required,MaxLength(31)]
        public string Subject { get; set; }
        [Required, MaxLength(401)]
        public string Message { get; set; }

        public DateOnly Date { get; set; }
        public Account Account { get; set; }
    }
}
