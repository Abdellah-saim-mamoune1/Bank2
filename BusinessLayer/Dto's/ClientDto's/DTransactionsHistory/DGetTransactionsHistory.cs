namespace bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory
{
    public class DGetTransactionsHistory
    {
        public int N {  get; set; }
        public string  Type { get; set; }

        public double Amount { get; set; }

        public DateOnly Date { get; set; }
    }
}
