using System.ComponentModel.DataAnnotations;

namespace bankApI.BusinessLayer.Dto_s.ClientDto_s.DNotifications
{


    public class DAddNotification
    {

        [Required, MaxLength(30)]
        public string Title { get; set; }
        [Required, MaxLength(500)]
        public string Body { get; set; }
        [Required]
        public int TypeId { get; set; }

       
    }

    

}