using bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory;

namespace bankApI.BusinessLayer.Services.ClientServer.IClient
{
    public interface ITransactionsHistory
    {
        public Task<List<DGetTransactionsHistory>> getTransactionsHistoryAsync(string AccountId);
        public Task<bool> TransferFundAsync(DSetTransferFund infos);
    }
}
