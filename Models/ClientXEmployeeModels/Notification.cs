using bankApI.Models.ClientModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankApI.Models.ClientXEmployeeModels
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [ForeignKey("NotificationsTypes")]
        public int TypeId { get; set; }
        

        public List<ClientXNotifications> clientXNotifications { get; set; }

        public NotificationsTypes types { get; set; }

    }
}
