
namespace bankApI.BusinessLayer.Dto_s.EmployeeDto_s

{
    public class DGetEmployeeInfos
    {


        public string FirstName { get; set; }

        public string LastName { get; set; }


        public DateOnly BirthDate { get; set; }

        public string PersonalEmail { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Type { get; set; }
        public string RoleType { get; set; }

        public DEAccount accountInfo { get; set; } = new();


        public bool IsActive { get; set; }






    }
}
