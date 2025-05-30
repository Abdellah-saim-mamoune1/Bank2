using System.ComponentModel.DataAnnotations;

namespace bankApI.BusinessLayer.Dto_s
{
    public class DUpdateClient
    {
        public string AccountId { get; set; }
        public string FirstName { get; set; }
      
        public string LastName { get; set; }

        
        public DateOnly BirthDate { get; set; }
       
        public string Email { get; set; }
      
        public string Address { get; set; }
      
        public string PhoneNumber { get; set; }
        
       
    }
}
