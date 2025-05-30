using bankApI.BusinessLayer.Dto_s;
using bankApI.Data;
using Microsoft.EntityFrameworkCore;
using bankApI.BusinessLayer.Services.SClient.IClient;
using bankApI.Models.ClientModels;
using bankApI.BusinessLayer.Methods;
using bankApI.Models.ClientXEmployeeModels;


namespace bankApI.BusinessLayer.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _db;

        public ClientService(AppDbContext db)
        {
            _db = db;
        }


     
        public async Task<IEnumerable<DPersonClientG>> GetAllClientsAsync()
        {
            var clients = await _db.Clients
    .Include(c => c.Person)
        .ThenInclude(p => p.Accounts)
       .ThenInclude(p => p.Card)
       .Include(c => c.Role)
       .Include(c => c.Access)

       .Select(c => new DPersonClientG
       {
           Id = c.PersonId,
           FirstName = c.Person.FirstName,
           LastName = c.Person.LastName,
           PersonalEmail = c.Person.Email,
           Address = c.Person.Address,
           BirthDate = c.Person.BirthDate,
           PhoneNumber = c.Person.PhoneNumber,
           RoleType = c.Role.Type,
           BankEmails = c.Person.Accounts.Select(a => new DAccountGet
           {
               AccountId = a.AccountAddress,
               Balance = a.Balance,
               IsFrozen = a.IsFrozen
           }).ToList(),

         Cards= c.Person.Accounts.Select(a => new DCardGet
          {
            CardNumber=a.Card.CardNumber,
            ExpirationDate=a.Card.ExpirationDate
         }).ToList(),

         
           IsActive = c.IsActive


       })
       .ToListAsync();


            return clients;
        }

        public async Task<DPersonClientG> GetClientByIdAsync(int id)
        {
            var client = await _db.Clients
          .Include(c => c.Person)
            
          .Include(c => c.Role)
          .Include(c => c.Access)
     
          .Where(c => c.PersonId == id)
          .Select(c => new DPersonClientG
          {

              Id = c.PersonId,
              FirstName = c.Person.FirstName,
              LastName = c.Person.LastName,
              PersonalEmail = c.Person.Email,
              Address = c.Person.Address,
              BirthDate = c.Person.BirthDate,
              PhoneNumber = c.Person.PhoneNumber,
              RoleType = c.Role.Type,
              BankEmails = c.Person.Accounts.Select(a => new DAccountGet
              {
                  AccountId = a.AccountAddress,
                  Balance = a.Balance,
                  IsFrozen = a.IsFrozen
              }).ToList(),
              
             
              IsActive = c.IsActive


          }).FirstOrDefaultAsync();

            return client;

        }

      
        public async Task<IEnumerable<DGetEmails>> GetAllClientsAccountsAsync()
        {
            var Emails = await _db.Accounts.Include(c => c.Person).Select(c=>new DGetEmails
            {
                Id = c.Id,
                FirstName = c.Person.FirstName,
                LastName=c.Person.LastName,
                Email=c.AccountAddress,
                Balance=c.Balance,
                IsFrozen=c.IsFrozen,
                PersonId=c.PersonId
            }).ToListAsync();

            return Emails;
            
        }
        public async Task<bool> UpdateClientByIdAsync(DUpdateClient client)
        { 
            if (client == null)
            {
                return false;
            }

          

            var Client = await _db.Accounts.Include(c=>c.Person).FirstOrDefaultAsync(c => c.AccountAddress == client.AccountId);

            if (Client == null)
            {
                return false;
            }


            if (client.BirthDate != default)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var age = today.Year - client.BirthDate.Year;

                // Adjust if the birthday hasn't happened yet this year
                if (client.BirthDate > today.AddYears(-age))
                {
                    age--;
                }

                if (age < 18)
                {
                    return false;
                }
            }


            Client.Person.FirstName = client.FirstName ?? Client.Person.FirstName;
            Client.Person.LastName = client.LastName ?? Client.Person.LastName;
            Client.Person.PhoneNumber = client.PhoneNumber ?? Client.Person.PhoneNumber;
            Client.Person.BirthDate = client.BirthDate;
            Client.Person.Address = client.Address;
            Client.Person.Email = client.Email ?? Client.Person.Email;
                await _db.SaveChangesAsync();
                return true;
            

           

        }




        public async Task<DAccountGet?> AddNewAccountAsync(DBankEmail email)
        {
            if (email == null  || email.InitialBalance < 1000 || email.InitialBalance>1000000
                || string.IsNullOrWhiteSpace(email.PassWord) || email.PassWord.Length < 7)
                return null;

            var person = await _db.Accounts.Include(C => C.Person).Where(p => p.AccountAddress == email.CurrentAccount).FirstOrDefaultAsync();
            if (person == null)
                return null;
            var t = await _db.Persons.Include(c => c.Accounts).Where(r => r.Id == person.Person.Id).FirstOrDefaultAsync();
            Console.WriteLine(t?.Accounts.Count);
            if ( person.Person.Accounts == null || t?.Accounts.Count > 6)
                return null;


   
            Random random = new Random();
            int cvv = random.Next(100, 1000);

            string newId;
            string newcardid;
            do
            {
                newId = GenerateKeys.GenerateId(10);
            }
            while (await _db.Accounts.AnyAsync(c => c.AccountAddress == newId));

            do
            {
                newcardid = GenerateKeys.GenerateNumberId(16);
            }
            while (await _db.Cards.AnyAsync(c => c.CardNumber == newcardid));



            Card card = new Card
            {
                CardNumber = newcardid,
                CVV = cvv,
                ExpirationDate = DateTime.UtcNow.AddYears(1).ToString("MM/yy")

            };

            await _db.Cards.AddAsync(card);
            await _db.SaveChangesAsync();

            Account account = new Account
            {
                PersonId = person.Person.Id,
                AccountAddress = newId,           
                PassWord = email.PassWord,
                Balance = email.InitialBalance,
                IsFrozen = false,
                CardId = card.Id,

            };

        
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
            DateOnly date=new DateOnly();
            DAccountGet info = new DAccountGet
            {
                AccountId = account.AccountAddress,
                Balance = account.Balance,
                IsFrozen = false,
                CreatedAt = date
            };

            Notification notif = new Notification
            {
                Body = "The new account is " + account.AccountAddress + " with the password: " + account.PassWord,
                Title = "New account",
                TypeId = 4

            };

            await _db.Notifications.AddAsync(notif);
            await _db.SaveChangesAsync();
            ClientXNotifications notification = new ClientXNotifications
            {
                AccountId = person.Id,
                NotificationId = notif.Id,
                Isviewed = false


            };

            await _db.ClientXNotifications.AddAsync(notification);
            await _db.SaveChangesAsync();




            return info;
        }


      
        public async Task<bool> FreezeClientAccountAsync(DSetEmailState state)
        {

            if (state.state != "Activate" && state.state != "DisActivate"||state.AccountId.Length<=4)
                return false;

            var c = await _db.Accounts.FirstOrDefaultAsync(d=>d.AccountAddress== state.AccountId);
            if (c == null)
                return false;

            if(state.state == "DisActivate")
                c.IsFrozen = true;
            else
                c.IsFrozen = false;


            await _db.SaveChangesAsync(); 
           
            return true;
        }


        async public Task<bool> AddGetHelpRequistAsync(DCNotifications state)
        {
            if (state == null || state.Body== null || state.Title.Length > 30 || state.Title.Length <= 0 || state.Title == null
                || state.Body.Length <= 0 || state.Body.Length > 400)
                return false;

            var account = await _db.Accounts.FirstOrDefaultAsync(a => a.AccountAddress == state.AccountId);
            if (account == null)
                return false;

            Notification notification = new Notification
            {
                Body="From "+account.AccountAddress+": "+state.Body,
                Title=state.Title,
                TypeId=4,
            };

            await _db.Notifications.AddAsync(notification);
            await _db.SaveChangesAsync();

            var EAccounts = await _db.EmployeeAccount.ToListAsync();

            if (EAccounts != null)
            {
                List<EmployeeNotifications> Enotifications = new List<EmployeeNotifications>();

                foreach (var n in EAccounts)
                {

                    EmployeeNotifications notify = new EmployeeNotifications
                    {
                        AccountId = n.Id,
                        NotificationId = notification.Id,
                        Isviewed = false
                    };

                    Enotifications.Add(notify);

                }

                await _db.EmployeeNotifications.AddRangeAsync(Enotifications);
                await _db.SaveChangesAsync();
            }


            return true;
        }


        async public Task<DPersonClientG?> GetClientInfo(string clientId) { 
        var client = await _db.Accounts.Include(C => C.Person).ThenInclude(C => C.Client).ThenInclude(C => C.Role).Where(C => C.AccountAddress == clientId).Select(c => new DPersonClientG
        {

            Id = c.PersonId,
            FirstName = c.Person.FirstName,
            LastName = c.Person.LastName,
            accountInfo = c.Person.Accounts.Where(a => a.AccountAddress == clientId)
              .Select(a => new DAccountGet
              {
                  AccountId = a.AccountAddress,
                  Balance = a.Balance,
                  IsFrozen = a.IsFrozen,
                  CreatedAt = a.CreatedAt


              }).FirstOrDefault(),
            cardInfo = c.Person.Accounts.Where(a => a.AccountAddress == clientId).Select(c => new DCardGet
            {
                CardNumber = c.Card.CardNumber.ToString().Substring(12, 4),
                ExpirationDate = c.Card.ExpirationDate,

            }).FirstOrDefault(),
            PersonalEmail = c.Person.Email,

            Address = c.Person.Address,
            BirthDate = c.Person.BirthDate,
            PhoneNumber = c.Person.PhoneNumber,
            RoleType = c.Person.Client.Role.Type,
            BankEmails = c.Person.Accounts.Select(a => new DAccountGet
            {
                AccountId = a.AccountAddress,
                Balance = a.Balance,
                IsFrozen = a.IsFrozen,
                CreatedAt = a.CreatedAt


            }).ToList(),

            Cards = c.Person.Accounts.Select(c => new DCardGet
            {
                CardNumber = c.Card.CardNumber.ToString().Substring(12, 4),
                ExpirationDate = c.Card.ExpirationDate,

            }).ToList(),
            IsActive = c.Person.Client.IsActive


        }).FirstOrDefaultAsync();
          

            if (client == null)
                return null;

            return client;
    }

}
}
