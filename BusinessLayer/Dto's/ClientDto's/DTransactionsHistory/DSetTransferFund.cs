namespace bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory
{
    public class DSetTransferFund
    {
        public string SenderAccountId { get; set; }
        public string RecieverAccountId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

    }
}
