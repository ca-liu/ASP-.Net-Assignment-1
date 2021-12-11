using Caleb_Liu_Assignment_1.Data;
using Caleb_Liu_Assignment_1.Repositories;
using Caleb_Liu_Assignment_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
            string firstName = adRepo.GetClient(User.Identity.Name).FirstName;
            HttpContext.Session.SetString("NAME_KEY", firstName);
            return View(query);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("AccountType,Balance")] AccountDetailsVM bankAccount)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            var newAccount = adRepo.Create(bankAccount, User.Identity.Name);
            return RedirectToAction("Details","Account", newAccount);
        }

        public IActionResult Details(int accountNum)
        {
            var requester = Request.Headers["Referer"].ToString();
            if (requester.Length > 31)
            {
                ViewBag.Message = "Action was successful";
            }
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

        [HttpPost]
        public ActionResult Edit(AccountDetailsVM adVM)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            adRepo.Update(adVM);
            return RedirectToAction("Details", "Account", adVM);
        }

        [HttpGet]
        public IActionResult Delete(int accountNum, int clientID)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            var query = adRepo.Get(accountNum);
            return View(query);
        }

        [HttpPost]
        public IActionResult Delete(int accountNum)
        {
            AccountDetailsRepo adRepo = new AccountDetailsRepo(_context);
            var query = adRepo.Delete(accountNum);
            return RedirectToAction("Index", "Account");
        }
    }
}
