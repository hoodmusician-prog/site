csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeSpring2026.Data;
using AnimeSpring2026.Models;

namespace AnimeSpring2026.Controllers
{
    public class AnimeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Список всех аниме с фильтрацией
        public async Task<IActionResult> Index(string genre, int page = 1)
        {
            int pageSize = 9;
            var query = _context.AnimeTitles.AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(a => a.Genre == genre);
                ViewBag.SelectedGenre = genre;
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(a => a.ReleaseDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var genres = await _context.AnimeTitles
                .Select(a => a.Genre)
                .Distinct()
                .ToListAsync();

            var viewModel = new AnimeListViewModel
            {
                AnimeList = items,
                Genres = genres,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                TotalItems = totalItems,
                SelectedGenre = genre
            };

            return View(viewModel);
        }

        // 2. Детальная страница аниме
        public async Task<IActionResult> Details(int id)
        {
            var anime = await _context.AnimeTitles
                .Include(a => a.Characters)
                .Include(a => a.Reviews.Where(r => r.IsApproved))
                .FirstOrDefaultAsync(a => a.Id == id);

            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // 3. Добавить отзыв (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(int animeId, [Bind("UserName,Rating,Comment")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.AnimeId = animeId;
                review.CreatedAt = DateTime.Now;
                review.IsApproved = false;
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Отзыв добавлен и ожидает модерации!";
                return RedirectToAction(nameof(Details), new { id = animeId });
            }
            return RedirectToAction(nameof(Details), new { id = animeId });
        }

        // 4. Топ-10 аниме
        public async Task<IActionResult> TopRated()
        {
            var topAnime = await _context.AnimeTitles
                .OrderByDescending(a => a.Rating)
                .Take(10)
                .ToListAsync();
            return View(topAnime);
        }
    }
}
