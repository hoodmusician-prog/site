csharp
using Microsoft.EntityFrameworkCore;
using AnimeSpring2026.Models;

namespace AnimeSpring2026.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AnimeTitle> AnimeTitles { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Начальные данные
            modelBuilder.Entity<AnimeTitle>().HasData(
                new AnimeTitle
                {
                    Id = 1,
                    Title = "Весеннее пробуждение",
                    OriginalTitle = "Spring Awakening",
                    Description = "История о том, как группа студентов открывает тайны весеннего сезона",
                    Genre = "Романтика",
                    ReleaseDate = new DateTime(2026, 4, 1),
                    Episodes = 12,
                    Rating = 8.5,
                    PosterUrl = "/images/anime1.jpg"
                },
                new AnimeTitle
                {
                    Id = 2,
                    Title = "Сакура рейнджеры",
                    OriginalTitle = "Sakura Rangers",
                    Description = "Экшен-аниме о защитниках весеннего города",
                    Genre = "Экшен",
                    ReleaseDate = new DateTime(2026, 4, 10),
                    Episodes = 24,
                    Rating = 9.0,
                    PosterUrl = "/images/anime2.jpg"
                },
                new AnimeTitle
                {
                    Id = 3,
                    Title = "Смех сквозь цветы",
                    OriginalTitle = "Laughter Through Flowers",
                    Description = "Комедийное аниме о приключениях в цветущем саду",
                    Genre = "Комедия",
                    ReleaseDate = new DateTime(2026, 4, 15),
                    Episodes = 10,
                    Rating = 7.8,
                    PosterUrl = "/images/anime3.jpg"
                }
            );

            modelBuilder.Entity<Character>().HasData(
                new Character { Id = 1, Name = "Юки", Age = 17, Role = "Главный герой", AnimeId = 1 },
                new Character { Id = 2, Name = "Хана", Age = 16, Role = "Героиня", AnimeId = 1 },
                new Character { Id = 3, Name = "Рен", Age = 18, Role = "Лидер рейнджеров", AnimeId = 2 },
                new Character { Id = 4, Name = "Мидори", Age = 17, Role = "Второй рейнджер", AnimeId = 2 }
            );
        }
    }
}
