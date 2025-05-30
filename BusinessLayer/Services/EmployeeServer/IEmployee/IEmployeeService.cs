using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.Models;
using Microsoft.AspNetCore.Mvc;

namespace bankApI.BusinessLayer.Services.EmployeeServer.IEmployee
{
    public interface IEmployeeService
    {
        public Task<DGetEmployee> AddNewEmployeeAsync(DEmployeePerson employee);
        public Task<DGetCardsInfo> GetCardsInfoAsync();
        public Task<DGetResentTransactions> GetAllTransactionsAsync();
        public Task<DGetEmployeeInfos?> GetEmployeeInfo(string EmployeeId);
        public Task<IEnumerable<DGetEmployeeInfos?>> GetAllEmployeesInfos();
        public Task<bool> UpdateEmployeeInfos(DUpdateEmployee Employeeinfo);
        public Task<bool> FreezeUnfreezeEmployeeAccountAsync(DSetEmailState state);
        public Task<bool> SendEmployeeMessage(DENotifications Notification);
    }
}
