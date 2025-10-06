using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entities
{
    public class User
    {
        [Key]
        [Column(TypeName = "UUID")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string Username { get; set; } = default!;

        [Required]
        [Column(TypeName = "VARCHAR(70)")]
        public string Password { get; set; } = default!;

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string FirstName { get; set; } = default!;

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string LastName { get; set; } = default!;

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Email { get; set; } = default!;

        public bool IsBan { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(10)")]
        public string Role { get; set; } = default!;

        [Column(TypeName = "TIMESTAMP WITH TIME ZONE")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "TIMESTAMP WITH TIME ZONE")]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<TaskItem> TaskItems { get; set; } = default!;
    }
}
