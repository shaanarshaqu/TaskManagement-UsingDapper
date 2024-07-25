using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.DTO
{
    public class TasksDto
    {
        [Required(ErrorMessage="You Should Add Title For The Task..")]
        [MaxLength(35, ErrorMessage="The Length Of The Title Between 1 - 35")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
