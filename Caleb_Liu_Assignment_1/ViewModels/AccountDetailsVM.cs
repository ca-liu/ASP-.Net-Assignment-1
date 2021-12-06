using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Caleb_Liu_Assignment_1.ViewModels
{
    public class AccountDetailsVM
    {
        public int ClientID { get; set; }
        [DisplayName("Account Number")] // Give nice label name for CRUD.
        public int AccountNum { get; set; }
        [DisplayName("Account Type")] // Give nice label name for CRUD.
        public string AccountType { get; set; }
        [DisplayName("First Name")]  // Give nice label name for CRUD.
        public string FirstName { get; set; }
        [DisplayName("Last Name")]   // Give nice label name for CRUD.
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }

    }
}
