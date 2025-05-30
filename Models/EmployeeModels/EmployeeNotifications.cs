using bankApI.Models.ClientXEmployeeModels;
using bankApI.Models.EmployeeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientModels
{
    public class EmployeeNotifications
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public bool Isviewed { get; set; }

        public EmployeeAccount Account { get; set; }

        public Notification Notification { get; set; }


    }
}
