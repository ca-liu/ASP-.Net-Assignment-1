using Caleb_Liu_Assignment_1.Data;
using Caleb_Liu_Assignment_1.Repositories;
using Caleb_Liu_Assignment_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;

namespace Caleb_Liu_Assignment_1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;


        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Account()
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            var query = adRepo.GetAll(UserPrincipal.Current.EmailAddress);
            return View(query);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("AccountType,Balance")] BankAccount bankAccount)
        {
            if(ModelState.IsValid)
            {
                _context.BankAccount.Add(bankAccount);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(bankAccount);
        }

/*        public IActionResult Details(int clientID, int accountNum)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            var query = adRepo.Get(clientID, accountNum);
            return View(query);
        }

        [HttpGet]
        public ActionResult Edit(int clientID, int accountNum)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            var query = adRepo.Get(clientID, accountNum);
            return View(query);
        }

        // This method is called when the user clicks the submit
        // button from the edit page.
        [HttpPost]
        public ActionResult Edit(AccountDetailsVM adVM)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            adRepo.Update(adVM);

            return RedirectToAction("Index", "Account");
        }*/

        public IActionResult Delete(int accountNum)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            var query = adRepo.Delete(accountNum);
            return View(query);
        }
    }
}
