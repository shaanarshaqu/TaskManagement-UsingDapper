using TaskManagement.Data.DTO;
using TaskManagement.Data.Models;

namespace TaskManagement.Manager.Interface
{
    public interface ITaskManager
    {
        Task<IEnumerable<Tasks>> GetAllTask();
        Task<Tasks> GetTaskById(string id);
        Task<bool> AddNewTask(TasksDto tasksDto);
        Task<bool> UpdateTask(Tasks task);
        Task<bool> DeleteTask(string id);
    }
}
