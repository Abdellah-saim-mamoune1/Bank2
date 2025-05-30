using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.BusinessLayer.Methods;
using bankApI.BusinessLayer.Services.EmployeeServer.IEmployee;
using bankApI.Data;
using bankApI.Models.ClientManagement;
using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using Microsoft.EntityFrameworkCore;

namespace bankApI.BusinessLayer.Services.EmployeeServer.SEmployee
{
    public class ClientsManagementService:IClientsManagement
    {
        private readonly AppDbContext _db;

        public ClientsManagementService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddNewClientAsync(DPersonClientS cli)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - cli.Person.BirthDate.Year;

            // Adjust if the birthday hasn't happened yet this year
            if (cli.Person.BirthDate > today.AddYears(-age))
            {
                age--;
            }


            if (cli == null|| age < 18 ||cli.Person.Email.Length<5||cli.Person.FirstName.Length<2||
                cli.Person.LastName.Length<2||cli.Person.Address.Length<2||
                cli.Account.Balance<1000||cli.Account.PassWord.Length<7)
                
                return false;

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


            Person person = new Person
            {
                FirstName = cli.Person.FirstName,
                LastName = cli.Person.LastName,
                BirthDate = cli.Person.BirthDate,
                PhoneNumber = cli.Person.PhoneNumber,
                Address = cli.Person.Address,
                Email = cli.Person.Email,
            };
            await _db.Persons.AddAsync(person);
            await _db.SaveChangesAsync();
            Random random = new Random();
            int cvv = random.Next(100, 1000);

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
                PersonId = person.Id,
                AccountAddress = newId,
                PassWord = cli.Account.PassWord,
                Balance = cli.Account.Balance,
                IsFrozen = false,
                CardId = card.Id,

            };
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();




            Client newclient = new Client
            {
                Person = person,
                TypeId = 2,
                AccessId = 1,
                IsActive = false

            };

            await _db.Clients.AddAsync(newclient);
            await _db.SaveChangesAsync();


            await _db.SaveChangesAsync();
            return true;

        }



        public async Task<bool> SendClientAccountMessage(DCNotifications Notification)
        {
            if (Notification == null || Notification.Body == null || Notification.Title == null || Notification.Type <= 0 || Notification.AccountId.Length < 8)
            {

                return false;
            }

            var account = await _db.Accounts.FirstOrDefaultAsync(c => c.AccountAddress == Notification.AccountId);
            if (account == null)
            {
                return false;
            }

            var type = await _db.NotificationsTypes.FirstOrDefaultAsync(c => c.Id == Notification.Type);
            if (type == null)
            {
                return false;
            }

            Notification notification = new Notification { Title = Notification.Title, Body = Notification.Body, TypeId = Notification.Type };
            await _db.Notifications.AddAsync(notification);
            await _db.SaveChangesAsync();

            ClientXNotifications notify = new ClientXNotifications { AccountId = account.Id, NotificationId = notification.Id, Isviewed = false };

            await _db.ClientXNotifications.AddAsync(notify);
            await _db.SaveChangesAsync();
            return true;

        }



        public async Task<bool> Deposit(DDepositWithdraw depositinfos)
        {
            if (depositinfos == null || depositinfos.ClientAccount==null || depositinfos.Amount==null|| depositinfos.Amount<1000
                || depositinfos.Amount > 100000)
                return false;

            try
            {
                var clientaccount = await _db.Accounts.FirstOrDefaultAsync(c => c.AccountAddress == depositinfos.ClientAccount);
                var employeeaccount = await _db.EmployeeAccount.FirstOrDefaultAsync(e => e.Account == depositinfos.EmployeeAccount);

                if (clientaccount == null || employeeaccount == null)
                    return false;


                clientaccount.Balance += depositinfos.Amount;
                var deposit = new TransactionsRegistre
                {
                    ClientAccountId = clientaccount.Id,
                    EmployeeAccountId = employeeaccount.Id,
                    Amount = depositinfos.Amount,
                    type="deposit"

                };
                await _db.TransactionsRegistres.AddAsync(deposit);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }



        public async Task<bool> Withdraw(DDepositWithdraw withdrawinfos)
        {
            if (withdrawinfos == null || withdrawinfos.ClientAccount == null || withdrawinfos.Amount == null || withdrawinfos.Amount < 1000
                || withdrawinfos.Amount > 1000000)
                return false;

            try
            {
                var clientaccount = await _db.Accounts.FirstOrDefaultAsync(c => c.AccountAddress == withdrawinfos.ClientAccount);
                var employeeaccount = await _db.EmployeeAccount.FirstOrDefaultAsync(e => e.Account == withdrawinfos.EmployeeAccount);

                if (clientaccount == null ||clientaccount.Balance<withdrawinfos.Amount|| employeeaccount == null)
                    return false;


                clientaccount.Balance -= withdrawinfos.Amount;
                var deposit = new TransactionsRegistre
                {
                    ClientAccountId = clientaccount.Id,
                    EmployeeAccountId = employeeaccount.Id,
                    Amount = withdrawinfos.Amount,
                    type = "withdraw"

                };
                await _db.TransactionsRegistres.AddAsync(deposit);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }

    }
}
