using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using Microsoft.AspNetCore.Mvc;

namespace bankApI.BusinessLayer.Services.ClientServer.IClient
{
    public interface IClientNotifications
    {
     
        public Task<bool> SetIsviewed(int Notificationid);
        public Task<List<DCNotificationsGet>> GetNotifications(string AccountId);


    }
}
