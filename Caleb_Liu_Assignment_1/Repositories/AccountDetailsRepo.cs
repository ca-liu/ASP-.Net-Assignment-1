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
            var clientQuery = (from c in db.Client
                               where c.Email == Email
                               select c).FirstOrDefault();

            var query = from ca in db.ClientAccount
                        where ca.ClientID == clientQuery.ClientID
                        orderby ca.AccountNum descending
                        select new AccountDetailsVM()
                        {
                            FirstName = clientQuery.FirstName,
                            LastName = clientQuery.LastName,
                            Email = clientQuery.Email,
                            AccountNum = ca.AccountNum,
                            AccountType = ca.BankAccount.AccountType,
                            Balance = ca.BankAccount.Balance
                        };

            return query;
        }

        public Client GetClient(string Email)
        {
            var clientIDQuery = (from c in db.Client
                                 where c.Email == Email
                                 select c).FirstOrDefault();
            return clientIDQuery;
        }

        public AccountDetailsVM Create(AccountDetailsVM bankAccount, string Email)
        {
            var clientQuery = (from c in db.Client
                                 where c.Email == Email
                                 select c).FirstOrDefault();

            var newBA = new BankAccount()
            {
                AccountType = bankAccount.AccountType,
                Balance = bankAccount.Balance
            };

            db.BankAccount.Add(newBA);
            db.SaveChanges();

            ClientAccount clientAccount = new ClientAccount()
            {
                ClientID = clientQuery.ClientID,
                AccountNum = newBA.AccountNum
            };

            db.ClientAccount.Add(clientAccount);
            db.SaveChanges();

            var query = new AccountDetailsVM()
            {
                ClientID = clientQuery.ClientID,
                FirstName = clientQuery.FirstName,
                LastName = clientQuery.LastName,
                Email = clientQuery.Email,
                AccountNum = newBA.AccountNum,
                AccountType = newBA.AccountType,
                Balance = newBA.Balance
            };

            return query;
        }

        public AccountDetailsVM Get(int accountNum)
        {

            var query = (from ca in db.ClientAccount
                        where ca.AccountNum == accountNum
                        select new AccountDetailsVM()
                        {
                            ClientID = ca.ClientID,
                            FirstName = ca.Client.FirstName,
                            LastName = ca.Client.LastName,
                            Email = ca.Client.Email,
                            AccountNum = ca.BankAccount.AccountNum,
                            AccountType = ca.BankAccount.AccountType,
                            Balance = ca.BankAccount.Balance
                        }).FirstOrDefault();

            return query;
        }

        public bool Update(AccountDetailsVM adVM)
        {
            BankAccount bankAccount = db.BankAccount
                            .Where(b => b.AccountNum == adVM.AccountNum)
                            .FirstOrDefault();
            bankAccount.Balance = adVM.Balance;

            Client client = db.Client
                .Where(c => c.ClientID == adVM.ClientID)
                .FirstOrDefault();
            client.FirstName = adVM.FirstName;
            client.LastName = adVM.LastName;

            db.SaveChanges();
            return true;
        }

        public bool Delete(int AccountNum)
        {
            //Remove form bridgeTable firt
            var baBT = (from ca in db.ClientAccount
                        where ca.AccountNum == AccountNum
                        select ca).FirstOrDefault();

            db.ClientAccount.Remove(baBT);
            db.SaveChanges();

            var ba = (from b in db.BankAccount
                      where b.AccountNum == AccountNum
                      select b).FirstOrDefault();

            db.BankAccount.Remove(ba);
            db.SaveChanges();
            return true;
        }

        public bool CreateNewAccount(BankAccount ba, Client cl)
        {
            var client = (from c in db.Client
                          where c.ClientID == cl.ClientID
                          select c).FirstOrDefault();

            var bAccount = (from b in db.BankAccount
                            where b.AccountNum == ba.AccountNum
                            select b).FirstOrDefault();

            ClientAccount ca = new ClientAccount()
            {
                AccountNum = bAccount.AccountNum,
                ClientID = client.ClientID
            };

            db.ClientAccount.Add(ca);
            db.SaveChanges();
            return true;
        }
    }
}
