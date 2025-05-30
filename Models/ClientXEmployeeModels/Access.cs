using System.ComponentModel.DataAnnotations;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class Access
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

    }
}
