using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.BusinessLayer.Services.EmployeeServer.IEmployee;
using bankApI.Data;
using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using bankApI.Models.EmployeeModels;
using Microsoft.EntityFrameworkCore;


namespace bankApI.BusinessLayer.Services.EmployeeServer.SEmployee
{
    public class EmployeeService:IEmployeeService
    {
        private readonly AppDbContext _db;

        public EmployeeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<DGetEmployee?> AddNewEmployeeAsync(DEmployeePerson employee)
        {
           if (employee == null || employee.Person == null ||
                employee.Employee == null || employee.EmployeeAccount == null)
                return null;
            Console.WriteLine("yes");

            var person = employee.Person;

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DateOnly birthDate = person.BirthDate;
            TimeSpan ageSpan = today.ToDateTime(TimeOnly.MinValue) - birthDate.ToDateTime(TimeOnly.MinValue);
            int age = (int)(ageSpan.Days / 365.25);

            if (string.IsNullOrWhiteSpace(person.FirstName) ||
                string.IsNullOrWhiteSpace(person.LastName) ||
                string.IsNullOrWhiteSpace(person.Address) ||
                age < 18 || age > 120 ||
                string.IsNullOrWhiteSpace(person.PhoneNumber) ||
                person.PhoneNumber.Length  !=9) 
            {
                return null;
            }
            Console.WriteLine("yes");

            var empData = employee.Employee;
            var role = await _db.EmployeeType.FirstOrDefaultAsync(p=>p.Id==empData.TypeId);
         
            if (role==null||
                empData.TypeId <= 0 ||
                empData.salary <= 0||empData.salary>1000000)
            {
                return null;
            }
            Console.WriteLine("yes");

            var acc = employee.EmployeeAccount;
            if (string.IsNullOrWhiteSpace(acc.Account) ||
                string.IsNullOrWhiteSpace(acc.Password))
            {
                return null;
            }

            Console.WriteLine("yes");
            var newPerson = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                BirthDate = person.BirthDate,
                Address = person.Address,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber
            };

            await _db.Persons.AddAsync(newPerson);
            await _db.SaveChangesAsync();


            Console.WriteLine("yes");
            var emp = new Employee
            {
                PersonId= newPerson.Id,
                AccessId = 1,
                RoleTypeId = 2,
                TypeId = empData.TypeId,
                IsActive = false,
                salary = empData.salary
            };

            await _db.Employees.AddAsync(emp);
            await _db.SaveChangesAsync();

          
            var empAcc = new EmployeeAccount
            {
                EmployeeId = newPerson.Id,
                Account = acc.Account,
                Password = acc.Password,
                IsFrozen = false
            };

            await _db.EmployeeAccount.AddAsync(empAcc);
            await _db.SaveChangesAsync();



            DGetEmployee Employee = new DGetEmployee
            {
                Id = newPerson.Id,
                FirstName = newPerson.FirstName,
                LastName = newPerson.LastName,
                PersonalEmail = newPerson.Email,
                BirthDate = newPerson.BirthDate,
                Address = newPerson.Address,
                PhoneNumber = newPerson.PhoneNumber,
                RoleType = "Employee",
                Type = role.Type,
                IsActive = emp.IsActive,
                Salary = emp.salary
            };

            return Employee;
        }
        





        public async Task<DGetCardsInfo> GetCardsInfoAsync()
        {
           int staffnum= await _db.Employees.CountAsync();
           int cli=await _db.Clients.CountAsync();
          double totalWithdrawls=0;
          double totalDeposits = 0;
            DateOnly today= DateOnly.FromDateTime(DateTime.Today);
            DateOnly currentweek= today.AddDays(-17);

            await _db.TransactionsHistory.Include(a => a.TransactionsType)
                .ForEachAsync(a => {
                    if (a.TransactionsType.Type == "Withdraw")
                        totalWithdrawls++;

                    else if (a.TransactionsType.Type == "Deposit")
                        totalDeposits++;

                });

            double totalTransfers = await _db.TransferFundHistory.CountAsync();
            short newClients = 0;
            await _db.Clients.ForEachAsync(a => {
                if (a.CreatedAt >= currentweek)
                    newClients++;
            });
            DGetCardsInfo infos = new DGetCardsInfo
            {
                totalStaff = staffnum,
                totalDeposits = totalDeposits,
                totalWithdrawls = totalWithdrawls,
                newClients = newClients,
                totalTransfers = totalTransfers,
                totalClients = cli,


            };


            return infos;

        }

        public async Task<DGetResentTransactions> GetAllTransactionsAsync()
        {
            var transactions = await _db.TransactionsHistory
                .Include(t => t.Account)
                    .ThenInclude(a => a.Person)
                .Include(t => t.TransactionsType)
                .OrderByDescending(t => t.Date)
                .Select(t => new DGetAllClientsTransactionsHistory
                {
                    AccountId = t.Account.AccountAddress,
                    ClientFullName = t.Account.Person.FirstName + " " + t.Account.Person.LastName,
                    TransactionDate = t.Date,
                    Amount = t.Amount,
                    Type = t.TransactionsType.Type,
                })
                .ToListAsync();

            var transferfunds = await _db.TransferFundHistory
                .Include(tf => tf.SenderAccountIds)
                .Include(tf => tf.RecieverAccountIds)
                .OrderByDescending(t => t.Date) 
                .Select(tf => new DGetTransferFundHistory
                {
                    SenderAccount = tf.SenderAccountIds.AccountAddress,
                    RecieverAccount = tf.RecieverAccountIds.AccountAddress,
                    Amount = tf.Amount,
                    Date = tf.Date
                })
                .ToListAsync();

         

            return new DGetResentTransactions
            {
                TransactionsHistory = transactions,
                TransferFundHistory = transferfunds
            };
        }


        async public Task<DGetEmployeeInfos?> GetEmployeeInfo(string EmployeeId)
        {
            var employee = await _db.EmployeeAccount.Include(C => C.Person).ThenInclude(C => C.Employee).ThenInclude(C => C.EmployeeType).
                Where(C => C.Account == EmployeeId).Select(c => new DGetEmployeeInfos
            {


                FirstName = c.Person.FirstName,
                LastName = c.Person.LastName,
                accountInfo =
                   new DEAccount
                   {
                       AccountId = c.Person.EmployeeAccount.Account,
                       Salary = c.Person.Employee.salary,
                       IsFrozen = c.Person.EmployeeAccount.IsFrozen,
                       CreatedAt = c.Person.Employee.CreatedAt

                   }
          ,
              
                PersonalEmail = c.Person.Email,

                Address = c.Person.Address,
                BirthDate = c.Person.BirthDate,
                PhoneNumber = c.Person.PhoneNumber,
                Type=c.Person.Employee.EmployeeType.Type,
                RoleType = c.Person.Employee.Role.Type,
               
                IsActive = c.Person.Employee.IsActive


            }).FirstOrDefaultAsync();

            if (employee == null)
                return null;

            return employee;
        }
        public async Task<IEnumerable<DGetEmployeeInfos?>> GetAllEmployeesInfos()
        {
            var employee = await _db.EmployeeAccount.Include(C => C.Person).ThenInclude(C => C.Employee).ThenInclude(C => C.EmployeeType)
          .Select(c => new DGetEmployeeInfos
             {


                 FirstName = c.Person.FirstName,
                 LastName = c.Person.LastName,
                 accountInfo =
                new DEAccount
                {
                    AccountId = c.Person.EmployeeAccount.Account,
                    Salary = c.Person.Employee.salary,
                    IsFrozen = c.Person.EmployeeAccount.IsFrozen,
                    CreatedAt = c.Person.Employee.CreatedAt

                }
       ,

                 PersonalEmail = c.Person.Email,

                 Address = c.Person.Address,
                 BirthDate = c.Person.BirthDate,
                 PhoneNumber = c.Person.PhoneNumber,
                 Type = c.Person.Employee.EmployeeType.Type,
                 RoleType = c.Person.Employee.Role.Type,

                 IsActive = c.Person.Employee.IsActive


             }).ToListAsync();

            if (employee == null)
                return null;

            return employee;

        }



        public async Task<bool> UpdateEmployeeInfos(DUpdateEmployee EmployeeInfos)
        {
            if (EmployeeInfos == null)
            {
                return false;
            }



            var Employee = await _db.EmployeeAccount.Include(c => c.Person).ThenInclude(c=>c.Employee).FirstOrDefaultAsync(c => c.Account == EmployeeInfos.AccountId);
            var Type = await _db.EmployeeType.FirstOrDefaultAsync(c => c.Type == EmployeeInfos.type);
            if (Employee == null||Type==null)
            {
                return false;
            }


            if (EmployeeInfos.BirthDate != default)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var age = today.Year - EmployeeInfos.BirthDate.Year;

                // Adjust if the birthday hasn't happened yet this year
                if (EmployeeInfos.BirthDate > today.AddYears(-age))
                {
                    age--;
                }

                if (age < 18)
                {
                    return false;
                }
            }


            Employee.Person.FirstName = EmployeeInfos.FirstName ?? Employee.Person.FirstName;
            Employee.Person.LastName = EmployeeInfos.LastName ?? Employee.Person.LastName;
            Employee.Person.PhoneNumber = EmployeeInfos.PhoneNumber ?? Employee.Person.PhoneNumber;
            Employee.Person.BirthDate = EmployeeInfos.BirthDate;
            Employee.Person.Address = EmployeeInfos.Address;
            Employee.Person.Email = EmployeeInfos.Email ?? Employee.Person.Email;
            Employee.Person.Employee.TypeId=Type.Id;
            await _db.SaveChangesAsync();
            return true;


        }


        public async Task<bool> FreezeUnfreezeEmployeeAccountAsync(DSetEmailState state)
        {
            

            if (state.state != "Activate" && state.state != "DisActivate" || state.AccountId.Length <= 4)
                return false;

            var c = await _db.EmployeeAccount.FirstOrDefaultAsync(d => d.Account == state.AccountId);
            if (c == null||c.Person?.Employee?.EmployeeType.Type=="Admin")
                return false;

            if (state.state == "DisActivate")
                c.IsFrozen = true;
            else
                c.IsFrozen = false;


            await _db.SaveChangesAsync();

            return true;
        
    }

        public async Task<bool> SendEmployeeMessage(DENotifications Notification)
        {
            if (Notification == null || Notification.Body == null || Notification.Title == null
                ||Notification.senderAccount==null||Notification.Type <= 0 || Notification.AccountId.Length < 8)
            {

                return false;
            }

            var account = await _db.EmployeeAccount.FirstOrDefaultAsync(c => c.Account == Notification.AccountId);
            if (account == null)
            {
                return false;
            }

            var type = await _db.NotificationsTypes.FirstOrDefaultAsync(c => c.Id == Notification.Type);
            if (type == null)
            {
                return false;
            }

            Notification notification = new Notification { Title = Notification.Title, 
                Body ="from "+Notification.senderAccount+": "+
                Notification.Body, TypeId = Notification.Type };
            await _db.Notifications.AddAsync(notification);
            await _db.SaveChangesAsync();

            EmployeeNotifications notify = new EmployeeNotifications { AccountId = account.Id, NotificationId = notification.Id, Isviewed = false };

            await _db.EmployeeNotifications.AddAsync(notify);
            await _db.SaveChangesAsync();
            return true;

        }


    }
}
