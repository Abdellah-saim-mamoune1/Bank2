using bankApI.Models;
using System.ComponentModel.DataAnnotations;

namespace bankApI.BusinessLayer.Dto_s
{
    public class DAcceptRequist
    {
        [Required]
        public int requistId { get; set; }
        [Required]
        public string Choice { get; set; }
    }
}
