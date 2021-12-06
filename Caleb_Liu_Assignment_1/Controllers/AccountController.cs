using Caleb_Liu_Assignment_1.Data;
using Caleb_Liu_Assignment_1.Repositories;
using Caleb_Liu_Assignment_1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
        public IActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            AccountDetailsRepo adRepo = new AccountDetailsRepo(context);
            var query = adRepo.GetAll();
            return View(query);

        }

        public IActionResult Details(int clientID, int accountNum)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            AccountDetailsRepo adRepo = new AccountDetailsRepo(context);
            var query = adRepo.Get(clientID, accountNum);
            return View(query);
        }

        [HttpGet]
        public ActionResult Edit(int clientID, int accountNum)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            AccountDetailsRepo adRepo = new AccountDetailsRepo(context);
            var query = adRepo.Get(clientID, accountNum);
            return View(query);
        }

        // This method is called when the user clicks the submit
        // button from the edit page.
        [HttpPost]
        public ActionResult Edit(AccountDetailsVM adVM)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            AccountDetailsRepo adRepo = new AccountDetailsRepo(context);
            adRepo.Update(adVM);

            return RedirectToAction("Index", "Account");
        }

        public IActionResult Delete(int accountNum)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            AccountDetailsRepo adRepo = new AccountDetailsRepo(context);
            var query = adRepo.Delete(accountNum);
            return View(query);
        }
    }
}
