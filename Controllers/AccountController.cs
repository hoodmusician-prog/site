csharp
using Microsoft.AspNetCore.Mvc;
using AnimeSpring2026.Data;
using AnimeSpring2026.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeSpring2026.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Регистрация (GET)
        public IActionResult Register()
        {
            return View();
        }

        // 2. Регистрация (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,Email,Bio")] UserProfile profile)
        {
            if (ModelState.IsValid)
            {
                if (await _context.UserProfiles.AnyAsync(u => u.UserName == profile.UserName))
                {
                    ModelState.AddModelError("UserName", "Имя пользователя уже занято");
                    return View(profile);
                }

                profile.JoinDate = DateTime.Now;
                profile.IsActive = true;
                _context.UserProfiles.Add(profile);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Регистрация прошла успешно!";
                return RedirectToAction("Index", "Home");
            }
            return View(profile);
        }

        // 3. Профиль пользователя
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(profile.FavoriteAnimeIds))
            {
                var ids = profile.FavoriteAnimeIds.Split(',').Select(int.Parse).ToList();
                ViewBag.FavoriteAnime = await _context.AnimeTitles
                    .Where(a => ids.Contains(a.Id))
                    .ToListAsync();
            }

            return View(profile);
        }

        // 4. Все пользователи
        public async Task<IActionResult> Users()
        {
            var users = await _context.UserProfiles
                .OrderByDescending(u => u.JoinDate)
                .ToListAsync();
            return View(users);
        }

        // 5. Добавить в избранное
        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int userId, int animeId)
        {
            var profile = await _context.UserProfiles.FindAsync(userId);
            if (profile == null) return NotFound();

            var favorites = profile.FavoriteAnimeIds?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList() ?? new List<int>();

            if (favorites.Contains(animeId))
            {
                favorites.Remove(animeId);
            }
            else
            {
                favorites.Add(animeId);
            }

            profile.FavoriteAnimeIds = string.Join(",", favorites);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile", new { id = userId });
        }
    }
}
