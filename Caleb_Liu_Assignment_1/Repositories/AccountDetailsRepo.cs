using Caleb_Liu_Assignment_1.Controllers;
using Caleb_Liu_Assignment_1.Data;
using Caleb_Liu_Assignment_1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caleb_Liu_Assignment_1.Repositories
{
    public class AccountDetailsRepo
    {
        ApplicationDbContext db;

        public AccountDetailsRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public IQueryable<AccountDetailsVM> GetAll(string Email)
        {
            var query = from c in db.Client
                        from ca in db.ClinetAccount
                        where c.Email == Email
                        select new AccountDetailsVM()  // Create new object using
                        {                             // our view model class.
                            ClientID = c.ClientID,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Email = c.Email,
                            AccountNum = ca.AccountNum,
                            AccountType = ca.BankAccount.AccountType,
                            Balance = ca.BankAccount.Balance
                        };

            return query;
        }

        public AccountDetailsVM Get(string Email, int accountNum)
        {
            var query = GetAll(Email)
                .Where(ad => ad.Email == Email && ad.AccountNum == accountNum)
                .FirstOrDefault();
            return query;
        }

        public bool Update(AccountDetailsVM adVM)
        {
            BankAccount bankAccount = db.BankAccount
                            .Where(b => b.AccountNum == adVM.AccountNum)
                            .FirstOrDefault();
            bankAccount.AccountType = adVM.AccountType;
            bankAccount.Balance = adVM.Balance;
            db.SaveChanges();
            return true;
        }

        public bool Delete(int accountNum)
        {
            BankAccount bankAccount = db.BankAccount
                                        .Where(b => b.AccountNum == accountNum)
                                        .FirstOrDefault();
            db.Remove(bankAccount);
            db.SaveChanges();
            return true;
        }
    }
}
