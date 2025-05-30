using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using Microsoft.AspNetCore.Mvc;

namespace bankApI.BusinessLayer.Services.SClient.IClient
{
    public interface IClientService
    {
            public Task<IEnumerable<DPersonClientG>> GetAllClientsAsync();
            public Task<DPersonClientG> GetClientByIdAsync(int id);
        public Task<DPersonClientG> GetClientInfo(string clientId);
        


            public Task<bool> UpdateClientByIdAsync( DUpdateClient client);
        public Task<DAccountGet?> AddNewAccountAsync(DBankEmail email);

        public Task<IEnumerable<DGetEmails>> GetAllClientsAccountsAsync();
                                                            
        public Task<bool> FreezeClientAccountAsync(DSetEmailState state);

        public Task<bool> AddGetHelpRequistAsync(DCNotifications state);


    }
    
}
