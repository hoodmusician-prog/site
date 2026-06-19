csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeSpring2026.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Оценка")]
        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Комментарий")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsApproved { get; set; } = false;

        [Required]
        public int AnimeId { get; set; }

        [ForeignKey("AnimeId")]
        public virtual AnimeTitle? Anime { get; set; }
    }
}
