namespace bankApI.BusinessLayer.Dto_s.EmployeeDto_s
{
    public class DGetAllClientsTransactionsHistory
    {
        public string? AccountId { get; set; }
        public string? ClientFullName { get; set; }
        public string? Type { get; set; }
        public double Amount { get; set; }
        public DateOnly TransactionDate { get; set; }
    }
}
