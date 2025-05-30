using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;

namespace bankApI.BusinessLayer.Services.EmployeeServer.IEmployee
{
    public interface IClientsManagement
    {
        public Task<bool> AddNewClientAsync(DPersonClientS client);
        public Task<bool> SendClientAccountMessage(DCNotifications Notification);
        public Task<bool> Deposit(DDepositWithdraw depositinfos);
        public Task<bool> Withdraw(DDepositWithdraw withdrawinfos);
    }
}
