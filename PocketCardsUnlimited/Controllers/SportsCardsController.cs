using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocketCardsUnlimited.Data;
using PocketCardsUnlimited.Models;

namespace PocketCardsUnlimited.Controllers
{
    [Authorize]
    public class SportsCardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SportsCardsController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // My Collection
        public async Task<IActionResult> Index(string? searchString)
        {
            string userId = _userManager.GetUserId(User)!;

            var cards = _context.SportsCards
                .Where(c => c.UserId == userId);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                string search = searchString.Trim().ToLower();

                if (search.Contains('*'))
                {
                    search = search.Replace("*", "");
                }

                cards = cards.Where(c =>
                    c.PlayerName.ToLower().Contains(search) ||
                    c.Sport.ToLower().Contains(search) ||
                    (c.Team != null &&
                     c.Team.ToLower().Contains(search)) ||
                    (c.Manufacturer != null &&
                     c.Manufacturer.ToLower().Contains(search)) ||
                    c.Year.ToString().Contains(search));
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await cards
                .OrderBy(c => c.PlayerName)
                .ToListAsync());
        }

        // Card Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = _userManager.GetUserId(User)!;

            var card = await _context.SportsCards
                .FirstOrDefaultAsync(c =>
                    c.Id == id && c.UserId == userId);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // Add Card
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PlayerName,Sport,Team,Manufacturer,Year,CardNumber,Condition,EstimatedValue")]
            SportsCard card)
        {
            ModelState.Remove(nameof(SportsCard.UserId));

            if (ModelState.IsValid)
            {
                card.UserId = _userManager.GetUserId(User)!;

                _context.SportsCards.Add(card);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] =
                    "Sports card added successfully!";

                return RedirectToAction(nameof(Index));
            }

            return View(card);
        }

        // Edit Card
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = _userManager.GetUserId(User)!;

            var card = await _context.SportsCards
                .FirstOrDefaultAsync(c =>
                    c.Id == id && c.UserId == userId);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("PlayerName,Sport,Team,Manufacturer,Year,CardNumber,Condition,EstimatedValue")]
            SportsCard updatedCard)
        {
            string userId = _userManager.GetUserId(User)!;

            var card = await _context.SportsCards
                .FirstOrDefaultAsync(c =>
                    c.Id == id && c.UserId == userId);

            if (card == null)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(SportsCard.UserId));

            if (ModelState.IsValid)
            {
                card.PlayerName = updatedCard.PlayerName;
                card.Sport = updatedCard.Sport;
                card.Team = updatedCard.Team;
                card.Manufacturer = updatedCard.Manufacturer;
                card.Year = updatedCard.Year;
                card.CardNumber = updatedCard.CardNumber;
                card.Condition = updatedCard.Condition;
                card.EstimatedValue = updatedCard.EstimatedValue;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] =
                    "Sports card updated successfully!";

                return RedirectToAction(nameof(Index));
            }

            return View(updatedCard);
        }

        // Delete Card
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = _userManager.GetUserId(User)!;

            var card = await _context.SportsCards
                .FirstOrDefaultAsync(c =>
                    c.Id == id && c.UserId == userId);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = _userManager.GetUserId(User)!;

            var card = await _context.SportsCards
                .FirstOrDefaultAsync(c =>
                    c.Id == id && c.UserId == userId);

            if (card == null)
            {
                return NotFound();
            }

            _context.SportsCards.Remove(card);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] =
                "Sports card deleted successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}