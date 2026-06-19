csharp
using Microsoft.AspNetCore.Mvc;
using AnimeSpring2026.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeSpring2026.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Главная страница
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                LatestAnime = await _context.AnimeTitles
                    .OrderByDescending(a => a.ReleaseDate)
                    .Take(6)
                    .ToListAsync(),
                TopRated = await _context.AnimeTitles
                    .OrderByDescending(a => a.Rating)
                    .Take(4)
                    .ToListAsync(),
                Upcoming = await _context.AnimeTitles
                    .Where(a => a.ReleaseDate.Year == 2026)
                    .OrderBy(a => a.ReleaseDate)
                    .Take(5)
                    .ToListAsync(),
                TotalAnime = await _context.AnimeTitles.CountAsync(),
                TotalCharacters = await _context.Characters.CountAsync()
            };

            return View(viewModel);
        }

        // 2. О проекте
        public async Task<IActionResult> About()
        {
            var stats = new AboutViewModel
            {
                TotalAnime = await _context.AnimeTitles.CountAsync(),
                TotalCharacters = await _context.Characters.CountAsync(),
                TotalReviews = await _context.Reviews.CountAsync(),
                TotalUsers = await _context.UserProfiles.CountAsync()
            };
            return View(stats);
        }

        // 3. Поиск
        public async Task<IActionResult> Search(string query)
        {
            var results = new List<AnimeTitle>();
            if (!string.IsNullOrEmpty(query))
            {
                results = await _context.AnimeTitles
                    .Where(a => a.Title.Contains(query) || 
                               (a.OriginalTitle != null && a.OriginalTitle.Contains(query)))
                    .ToListAsync();
            }
            ViewBag.Query = query;
            return View(results);
        }
    }
}
