csharp
using AnimeSpring2026.Models;

namespace AnimeSpring2026.Controllers
{
    public class HomeViewModel
    {
        public List<AnimeTitle> LatestAnime { get; set; } = new();
        public List<AnimeTitle> TopRated { get; set; } = new();
        public List<AnimeTitle> Upcoming { get; set; } = new();
        public int TotalAnime { get; set; }
        public int TotalCharacters { get; set; }
    }

    public class AboutViewModel
    {
        public int TotalAnime { get; set; }
        public int TotalCharacters { get; set; }
        public int TotalReviews { get; set; }
        public int TotalUsers { get; set; }
    }

    public class AnimeListViewModel
    {
        public List<AnimeTitle> AnimeList { get; set; } = new();
        public List<string> Genres { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public string? SelectedGenre { get; set; }
    }
}
