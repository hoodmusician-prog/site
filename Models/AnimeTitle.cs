csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeSpring2026.Models
{
    public class AnimeTitle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [Display(Name = "Название")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Оригинальное название")]
        [StringLength(200)]
        public string? OriginalTitle { get; set; }

        [Required]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Жанр")]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Дата выхода")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Количество эпизодов")]
        [Range(1, 1000)]
        public int Episodes { get; set; } = 12;

        [Display(Name = "Рейтинг")]
        [Range(0, 10)]
        public double Rating { get; set; }

        [Display(Name = "Постер")]
        public string? PosterUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
