using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Caleb_Liu_Assignment_1.Data
{
    public class Client
    {
        [Key]
        [Display(Name = "Client ID")]
        [Required]
        public int ClientID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        // Navigation properties.
        // Child.        
        public virtual ICollection<ClinetAccount> ClinetAccount { get; set; }
    }

    public class BankAccount
    {
        [Key]
        [Display(Name = "Account Number")]
        [Required]
        public int AccountNum { get; set; }

        [Display(Name = "Account Type")]
        [Required]
        public string AccountType { get; set; }
        public decimal Balance { get; set; }

        // Navigation properties.
        // Child.        
        public virtual ICollection<ClinetAccount> ClinetAccount { get; set; }

    }

    public class ClinetAccount
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
        public DbSet<ClinetAccount> ClinetAccount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Define composite primary keys.
            modelBuilder.Entity<ClinetAccount>()
                .HasKey(ca => new { ca.ClientID, ca.AccountNum});

            // Define foreign keys here. Do not use foreign key annotations.
            modelBuilder.Entity<ClinetAccount>()
                .HasOne(c => c.Client)
                .WithMany(ca => ca.ClinetAccount)
                .HasForeignKey(fk => new { fk.ClientID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<ClinetAccount>()
                .HasOne(b => b.BankAccount)
                .WithMany(ca => ca.ClinetAccount)
                .HasForeignKey(fk => new { fk.AccountNum})
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}
