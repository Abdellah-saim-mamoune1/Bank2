using System.ComponentModel.DataAnnotations;

namespace bankApI.BusinessLayer.Dto_s
{
    public class DCNotifications
    {
       
        public string Title { get; set; }
       
        public string Body { get; set; }
      
        public int Type { get; set; }

        public string AccountId { get; set; }
    }
}
