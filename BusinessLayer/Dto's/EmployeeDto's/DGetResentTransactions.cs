namespace bankApI.BusinessLayer.Dto_s.EmployeeDto_s
{
    public class DGetResentTransactions
    {
        public List<DGetAllClientsTransactionsHistory>? TransactionsHistory { get; set; } = new();
        public List<DGetTransferFundHistory>? TransferFundHistory { get; set; } = new();
    }
}
