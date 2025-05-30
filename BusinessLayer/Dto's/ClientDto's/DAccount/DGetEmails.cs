using System.ComponentModel.DataAnnotations;

namespace bankApI.BusinessLayer.Dto_s
{
    public class DGetEmails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

       
        public string Email { get; set; }
        public double Balance { get; set; }

        public bool IsFrozen { get; set; }
        public int PersonId { get; set; }
    }
}
