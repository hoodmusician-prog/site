csharp
using System.ComponentModel.DataAnnotations;

namespace AnimeSpring2026.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "О себе")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string? Bio { get; set; }

        [Display(Name = "Аватар")]
        public string? AvatarUrl { get; set; }

        [Display(Name = "Любимые аниме (ID через запятую)")]
        public string? FavoriteAnimeIds { get; set; }

        public DateTime JoinDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
