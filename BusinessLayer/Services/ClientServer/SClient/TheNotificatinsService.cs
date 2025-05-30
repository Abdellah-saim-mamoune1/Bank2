
using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Services.ClientServer.IClient;
using bankApI.Data;
using Microsoft.EntityFrameworkCore;

namespace bankApI.BusinessLayer.Services.ClientServer.SClient
{
    public class TheNotificatinsService:IClientNotifications
    {
        private readonly AppDbContext _db;

        public TheNotificatinsService(AppDbContext db)
        {
            _db = db;
        }
      


        public async Task<List<DCNotificationsGet>> GetNotifications(string AccountId)
        {
           var account = await _db.Accounts.FirstOrDefaultAsync(c => c.AccountAddress == AccountId);
            if (account == null)
                return null;

            var Notification = await _db.Accounts.Include(c=>c.clientXNotifications).Where(c => c.AccountAddress == AccountId).SelectMany(a => a.clientXNotifications)
    .Select(cxn => new DCNotificationsGet
    {
        Id=cxn.Notification.Id,
        Title=cxn.Notification.Title,
        Notification=cxn.Notification.Body,
        Type=cxn.Notification.types.Name,
        Date =cxn.Date,
        Isviewed=cxn.Isviewed
      
    })
    .ToListAsync();

         
            return Notification;

        }


        public async Task<bool> SetIsviewed(int Notificationid)
        {
            if (Notificationid <= 0)
                return false;

            var ntf = await _db.ClientXNotifications.FirstOrDefaultAsync(c => c.Id == Notificationid);
            if (ntf == null)
                return false;

            ntf.Isviewed = true;
            await _db.SaveChangesAsync();

            return true;


        }
    }
}
