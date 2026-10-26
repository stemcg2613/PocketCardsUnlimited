using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PocketCardsUnlimited.Models;

namespace PocketCardsUnlimited.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Sports Cards
        public DbSet<SportsCard> SportsCards { get; set; }
    }
}