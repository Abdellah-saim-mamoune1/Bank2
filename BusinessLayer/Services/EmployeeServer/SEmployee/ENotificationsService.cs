using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Services.EmployeeServer.IEmployee;
using bankApI.Data;
using Microsoft.EntityFrameworkCore;

namespace bankApI.BusinessLayer.Services.EmployeeServer.SEmployee
{
    public class ENotificationsService: IENotifications
    {

        private readonly AppDbContext _db;

        public ENotificationsService(AppDbContext db)
        {
            _db = db;
        }



        public async Task<List<DCNotificationsGet>> GetNotifications(string AccountId)
        {
            var account = await _db.EmployeeAccount.FirstOrDefaultAsync(c => c.Account == AccountId);
            if (account == null)
                return null;

            var Notification = await _db.EmployeeAccount.Include(c => c.EmployeeNotifications).Where(c => c.Account == AccountId).SelectMany(a => a.EmployeeNotifications)
    .Select(cxn => new DCNotificationsGet
    {
        Id = cxn.Notification.Id,
        Title = cxn.Notification.Title,
        Notification = cxn.Notification.Body,
        Type = cxn.Notification.types.Name,
        Date = cxn.Date,
        Isviewed = cxn.Isviewed

    })
    .ToListAsync();


            return Notification;

        }


        public async Task<bool> ESetIsviewed(DSetIsNotificationV info)
        {
            if (info.id <= 0 ||info==null)
                return false;

            var ntf = await _db.EmployeeNotifications.FirstOrDefaultAsync(c => c.NotificationId == info.id&&c.Account.Account== info.account);
            if (ntf == null)
                return false;

            ntf.Isviewed = true;
            await _db.SaveChangesAsync();

            return true;


        }
    }
}
