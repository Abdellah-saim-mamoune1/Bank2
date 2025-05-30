

namespace bankApI.BusinessLayer.Dto_s
{
    public class DAccountGet
    {
       public string AccountId { get; set; }
       public double Balance { get; set; }
        public bool IsFrozen { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
