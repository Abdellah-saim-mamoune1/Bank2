using bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory;
using bankApI.BusinessLayer.Services.ClientServer.IClient;
using bankApI.Data;
using bankApI.Models.ClientXEmployeeModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.IO.IsolatedStorage;

namespace bankApI.BusinessLayer.Services.ClientServer.SClient
{
    public class TransactionsHistoryService: ITransactionsHistory
    {

        private readonly AppDbContext _db;

        public TransactionsHistoryService(AppDbContext db)
        {
            _db = db;
        }
        async public Task<List<DGetTransactionsHistory>> getTransactionsHistoryAsync(string AccountId)
        {

            var account = await _db.Accounts.FirstOrDefaultAsync(A => A.AccountAddress == AccountId);
            if (account==null)
            {
                return null;
            }

            var TH=await _db.Accounts.Include(a=>a.AccountTransactionHistory).ThenInclude(a=>a.TransactionsType).Where(A => A.AccountAddress == AccountId).SelectMany(a=>a.AccountTransactionHistory)
                .Select(w=>
                 new DGetTransactionsHistory
                 {
                     N = w.Id,
                     Amount = w.Amount,
                     Type=w.TransactionsType.Type,
                     Date=w.Date,
            }
             ).ToListAsync();

            return TH;


        }

        async public Task<bool> TransferFundAsync(DSetTransferFund infos)
        {
            if (infos == null || infos.SenderAccountId == null || infos.RecieverAccountId == null||infos.Amount<1000||infos.Amount>1000000)
                return false;
            var senderacc =await _db.Accounts.FirstOrDefaultAsync(a => a.AccountAddress == infos.SenderAccountId);
            var recieveracc=await _db.Accounts.FirstOrDefaultAsync(a => a.AccountAddress == infos.RecieverAccountId);
            if (senderacc == null || recieveracc == null||senderacc?.Balance<infos.Amount)
                return false;
            senderacc.Balance-=infos.Amount;
            recieveracc.Balance += infos.Amount;

            TransferFundHistory t = new TransferFundHistory {
              SenderAccountId =senderacc.Id,
              RecieverAccountId =recieveracc.Id,
              Amount =infos.Amount,
              Description=infos?.Description
    };

            await _db.TransferFundHistory.AddAsync(t);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
