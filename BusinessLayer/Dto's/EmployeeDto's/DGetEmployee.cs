namespace bankApI.BusinessLayer.Dto_s
{
    public class DGetEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string PersonalEmail { get; set; }

        public DateOnly BirthDate { get; set; }
        public string Address { get; set; }

        public string PhoneNumber { get; set; }


        public string Type { get; set; }

        public string RoleType { get; set; }
      
        public bool IsActive { get; set; }
        public double Salary { get; set; }
       

    }
}
