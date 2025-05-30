using System.ComponentModel.DataAnnotations;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class NotificationsTypes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List< Notification > Notifications { get; set; }
    }
}
