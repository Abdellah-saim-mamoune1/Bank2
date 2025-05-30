using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace bankApI.Models.ClientModels
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }

        public int CVV { get; set; }

        public Account Account { get; set; }

    }
}
