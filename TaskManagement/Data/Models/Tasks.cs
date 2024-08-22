using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.Models
{
    public class Tasks
    {
        [Required]
        public string? Id { get; set; }
        public DateTime? CreatedAt {  get; set; }    
        public DateTime? UpdatedAt { get; set; }
        [Required]
        [MaxLength(35, ErrorMessage = "The Length Of The Title Between 1 - 35")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? user_id { get; set; }
    }
}
