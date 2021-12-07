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
        public IActionResult Index()
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            var query = adRepo.GetAll(User.Identity.Name);
            return View(query);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("AccountType,Balance")] BankAccount bankAccount)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            adRepo.Create(bankAccount, User.Identity.Name);
            return RedirectToAction("Index", "Account");
        }

        public IActionResult Details(int accountNum)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            var query = adRepo.Get(accountNum);
            return View(query);
        }

        [HttpGet]
        public ActionResult Edit(int accountNum)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            var query = adRepo.Get(accountNum);
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
        }
    }
}
