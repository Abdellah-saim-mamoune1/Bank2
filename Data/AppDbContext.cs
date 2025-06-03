

using bankApI.Models.ClientManagement;
using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using bankApI.Models.EmployeeModels;
using Microsoft.EntityFrameworkCore;


namespace bankApI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Person> Persons { get; set; }

        public DbSet<Employee>Employees{ get; set; }

      

        public DbSet<Role> Roles { get; set; }
        public DbSet<Access>Accesses { get; set; }
        public DbSet<Account>Accounts { get; set; }


        public DbSet<EmployeeType> EmployeeType { get; set; }

        public DbSet<EmployeeAccount> EmployeeAccount { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<TransactionsHistory> TransactionsHistory { get; set; }

        public DbSet<TransactionsType> TransactionsTypes { get; set; }


        public DbSet<ClientXNotifications> ClientXNotifications { get; set; }

        public DbSet<EmployeeNotifications> EmployeeNotifications { get; set; }

        public DbSet<NotificationsTypes> NotificationsTypes { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<LoginRegistre> LoginRegistre { get; set; }   

       
        public DbSet<TransferFundHistory> TransferFundHistory { get; set; }

        public DbSet<GetHelp> GetHelp { get; set; }

        public DbSet<TransactionsRegistre> TransactionsRegistres { get; set; }

   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Client)
                .WithOne(c => c.Person)
                .HasForeignKey<Client>(c => c.PersonId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
              .HasOne(p => p.Employee)
              .WithOne(c => c.Person)
              .HasForeignKey<Employee>(c => c.PersonId)
              .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Account>()
            .HasOne(p => p.Person)
            .WithMany(c => c.Accounts)
            .HasForeignKey(c => c.PersonId)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Card>()
               .HasOne(p => p.Account)
               .WithOne(c => c.Card)
               .HasForeignKey<Account>(c => c.CardId)
               .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Employee>()
        .HasOne(p => p.Role)
        .WithMany(c => c.Employees)
        .HasForeignKey(c => c.RoleTypeId)
        .OnDelete(DeleteBehavior.Restrict);

      

            modelBuilder.Entity<Employee>()
            .HasOne(p => p.EmployeeType)
            .WithMany(c => c.Employees)
            .HasForeignKey(c => c.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
              .HasOne(p => p.EmployeeAccount)
              .WithOne(c => c.Person)
              .HasForeignKey<EmployeeAccount>(c=>c.EmployeeId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
           .HasOne(p => p.types)
           .WithMany(c => c.Notifications)
           .HasForeignKey(c => c.TypeId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionsHistory>()
                .HasOne(p => p.TransactionsType)
           .WithMany(c => c.TransactionHistory)
           .HasForeignKey(c => c.TypeId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionsHistory>()
              .HasOne(p => p.Account)
         .WithMany(c => c.AccountTransactionHistory)
         .HasForeignKey(c => c.AccountId)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferFundHistory>()
          .HasOne(p => p.SenderAccountIds)
     .WithMany(c => c.SenderAccount)
     .HasForeignKey(c => c.SenderAccountId)
     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferFundHistory>()
       .HasOne(p => p.RecieverAccountIds)
  .WithMany(c => c.RecieverAccount)
  .HasForeignKey(c => c.RecieverAccountId)
  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GetHelp>()
  .HasOne(p => p.Account)
.WithMany(c => c.GetHelp)
.HasForeignKey(c => c.ClientAccountId)
.OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<TransactionsRegistre>()
        .HasOne(d => d.EmployeeAccount)
        .WithMany(e => e.Deposits)
        .HasForeignKey(d => d.EmployeeAccountId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionsRegistre>()
                .HasOne(d => d.ClientAccount)
                .WithMany(c => c.Deposits)
                .HasForeignKey(d => d.ClientAccountId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Employee>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<TransactionsHistory>()
.Property(l => l.Date)
.HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<GetHelp>()
.Property(l => l.Date)
.HasDefaultValueSql("CURRENT_DATE");

        

            modelBuilder.Entity<TransferFundHistory>()
     .Property(l => l.Date)
     .HasDefaultValueSql("CURRENT_DATE");

          



          
        

            modelBuilder.Entity<Client>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<Account>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("CURRENT_DATE");

           

            modelBuilder.Entity<TransactionsRegistre>()
    .Property(l => l.Date)
    .HasDefaultValueSql("CURRENT_DATE");



            modelBuilder.Entity<TransactionRegistre>()
                .HasOne(l => l.ReceiverClient)
                .WithMany(c => c.ReceivedTransactions)
                .HasForeignKey(l => l.ReceiverClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionRegistre>()
    .Property(l => l.Date)
    .HasDefaultValueSql("CURRENT_DATE");


            modelBuilder.Entity<LoginRegistre>()
.Property(l => l.Date)
.HasDefaultValueSql("CURRENT_DATE");


            modelBuilder.Entity<ClientXNotifications>()
.Property(l => l.Date)
.HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<EmployeeNotifications>()
.Property(l => l.Date)
.HasDefaultValueSql("CURRENT_DATE");



            base.OnModelCreating(modelBuilder);
        }


    }
}
