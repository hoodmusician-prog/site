csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeSpring2026.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя персонажа")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "Возраст")]
        [Range(1, 999)]
        public int? Age { get; set; }

        [Display(Name = "Роль")]
        [StringLength(100)]
        public string Role { get; set; } = "Главный персонаж";

        [Display(Name = "Изображение")]
        public string? ImageUrl { get; set; }

        [Required]
        public int AnimeId { get; set; }

        [ForeignKey("AnimeId")]
        public virtual AnimeTitle? Anime { get; set; }
    }
}
