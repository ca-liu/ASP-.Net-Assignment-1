using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Caleb_Liu_Assignment_1.ViewModels
{
    public class AccountDetailsVM
    {
        [DisplayName("Client ID")]
        public int ClientID { get; set; }
        [DisplayName("Account Number")] // Give nice label name for CRUD.
        public int AccountNum { get; set; }
        [DisplayName("Account Type")] // Give nice label name for CRUD.
        [Required(ErrorMessage = "Account type required.")]
        public string AccountType { get; set; }
        [DisplayName("First Name")]  // Give nice label name for CRUD.
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only letters are allowed")]
        [Required(ErrorMessage = "First name required.")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]   // Give nice label name for CRUD.
        [Required(ErrorMessage = "Last name required.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only letters are allowed")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email required.")]
        public string Email { get; set; }

        [Range(0.001, int.MaxValue, ErrorMessage = "Please enter a number greater than 0")]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
    }
}
