using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bankApI.BusinessLayer.Dto_s
{
    public class DPersonClientG
    {
       
        public int Id { get; set; }

       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

       
        public DateOnly BirthDate { get; set; }


        public DAccountGet accountInfo { get; set; } = new();

        public DCardGet cardInfo { get; set; } = new();

        public string PersonalEmail { get; set; }
      
        public string Address { get; set; }
      
        public string PhoneNumber { get; set; }
        public string RoleType { get; set; }

        public List<DAccountGet> BankEmails { get; set; } = new();
       
        public List<DCardGet> Cards { get; set; } = new();

        public bool IsActive { get; set; }

    
       
       
    }
}
