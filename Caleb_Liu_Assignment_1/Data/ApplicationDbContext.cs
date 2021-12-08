using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caleb_Liu_Assignment_1.Data
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Client ID")]
        [Required]
        public int ClientID { get; set; }

        [Display(Name = "First Name")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only letters are allowed")]
        [Required(ErrorMessage = "First name required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only letters are allowed")]
        [Required(ErrorMessage = "Last name required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email required.")]
        [EmailAddress]
        public string Email { get; set; }

        // Navigation properties.
        // Child.        
        public virtual ICollection<ClientAccount> ClientAccount { get; set; }
    }

    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Account Number")]
        [Required]
        public int AccountNum { get; set; }

        [Display(Name = "Account Type")]
        [Required(ErrorMessage = "Account type required.")]
        public string AccountType { get; set; }
        [Range(0.001, int.MaxValue, ErrorMessage = "Please enter a number greater than 0")]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        // Navigation properties.
        // Child.        
        public virtual ICollection<ClientAccount> ClientAccount { get; set; }

    }

    public class ClientAccount
    {
        [Key]
        [Display(Name = "Client ID")]
        [Required]
        public int ClientID { get; set; }

        [Key]
        [Display(Name = "Account Number")]
        [Required]
        public int AccountNum { get; set; }

        // Navigation properties.
        // Parents.
        public virtual BankAccount BankAccount{ get; set; }
        public virtual Client Client { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Define entity collections.
        public DbSet<Client> Client { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<ClientAccount> ClientAccount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Define composite primary keys.
            modelBuilder.Entity<ClientAccount>()
                .HasKey(ca => new { ca.ClientID, ca.AccountNum});

            // Define foreign keys here. Do not use foreign key annotations.
            modelBuilder.Entity<ClientAccount>()
                .HasOne(c => c.Client)
                .WithMany(ca => ca.ClientAccount)
                .HasForeignKey(fk => new { fk.ClientID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<ClientAccount>()
                .HasOne(b => b.BankAccount)
                .WithMany(ca => ca.ClientAccount)
                .HasForeignKey(fk => new { fk.AccountNum})
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}
