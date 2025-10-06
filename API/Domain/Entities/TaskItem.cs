using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entities
{
    public class TaskItem
    {
        [Key]
        [Column(TypeName = "UUID")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Title { get; set; } = default!;

        [Column(TypeName = "TEXT")]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(20)")]
        public string Status { get; set; } = default!;

        [Required]
        [Column(TypeName = "VARCHAR(10)")]
        public string Priority { get; set; } = default!;

        [Column(TypeName = "TIMESTAMP WITH TIME ZONE")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "TIMESTAMP WITH TIME ZONE")]
        public DateTime DueDate { get; set; }

        [Required]
        [Column(TypeName = "UUID")]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = default!;

        [Column(TypeName = "TIMESTAMP WITH TIME ZONE")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "TIMESTAMP WITH TIME ZONE")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
