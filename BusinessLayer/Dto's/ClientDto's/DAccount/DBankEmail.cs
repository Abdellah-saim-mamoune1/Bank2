using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bankApI.BusinessLayer.Dto_s
{
    public class DBankEmail
    {
        public string CurrentAccount { get; set; }
   
        public string PassWord { get; set; }

        public double InitialBalance { get; set; }
      
    }
}
