namespace bankApI.BusinessLayer.Dto_s.EmployeeDto_s
{
    public class DGetTransferFundHistory
    {
        public string? SenderAccount { get; set; }
        public string? RecieverAccount { get; set; }
        public double Amount { get; set; }
        public DateOnly Date { get; set; }

    }
}
