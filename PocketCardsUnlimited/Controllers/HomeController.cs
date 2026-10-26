using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocketCardsUnlimited.Data;
using PocketCardsUnlimited.Models;
using System.Diagnostics;

namespace PocketCardsUnlimited.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Dashboard
        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                string userId = _userManager.GetUserId(User)!;

                var cards = _context.SportsCards
                    .Where(c => c.UserId == userId);

                ViewBag.TotalCards = await cards.CountAsync();

                ViewBag.TotalValue = await cards
                    .SumAsync(c => c.EstimatedValue);
            }
            else
            {
                ViewBag.TotalCards = 0;
                ViewBag.TotalValue = 0m;
            }

            return View();
        }

        // Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // Error
        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}