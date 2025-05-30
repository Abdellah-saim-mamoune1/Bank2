using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;

namespace bankApI.BusinessLayer.Services.EmployeeServer.IEmployee
{
    public interface IENotifications
    {
        public Task<bool> ESetIsviewed(DSetIsNotificationV info);
        public Task<List<DCNotificationsGet>> GetNotifications(string AccountId);
    }
}
