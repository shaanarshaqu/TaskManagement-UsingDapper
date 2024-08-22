using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.Models
{
    public class Users
    {
        [Required]
        public string? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
