using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BankWEB.Models;

namespace BankWEB.DataContext
{
    public class AuthBankContext : IdentityDbContext<User>
    {
        public AuthBankContext(DbContextOptions options) : base(options) 
        { 
            Database.EnsureCreated();
        }
    }
}
