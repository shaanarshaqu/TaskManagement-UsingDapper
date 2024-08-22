using AutoMapper;
using TaskManagement.Common;
using TaskManagement.Data;
using TaskManagement.Data.DTO;
using TaskManagement.Data.Models;
using TaskManagement.Manager.Interface;

namespace TaskManagement.Manager
{
    public class TaskManager : ITaskManager
    {
        private readonly DataProvider dataProvider;
        private readonly IMapper mapper;
        public TaskManager(DataProvider dataProvider, IMapper mapper)
        {
            this.dataProvider = dataProvider;
            this.mapper = mapper;
        }



        public async Task<IEnumerable<Tasks>> GetAllTask(string id)
        {
            try
            {
                IEnumerable<Tasks> AllTasks = await dataProvider.GetAllByCondition<Tasks>(Constants.Tables.Tasks.ToString(),new Tasks { user_id=id});
                return AllTasks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Tasks> GetTaskById(string id)
        {
            try
            {
                Tasks TaskById = await dataProvider.GetByIdAsync<Tasks>(Constants.Tables.Tasks.ToString(), id);
                return TaskById;
            }
            catch (Exception ex)
            {
                throw new Exception("Something Went Wrong");
            }
        }

        public async Task<bool> AddNewTask(TasksDto tasksDto)
        {
            try
            {
                Tasks convertedtask = mapper.Map<Tasks>(tasksDto);
                convertedtask.Id = Guid.NewGuid().ToString();
                convertedtask.CreatedAt = DateTime.Now;
                convertedtask.UpdatedAt = DateTime.Now;
                int rowEffected = await dataProvider.IncertAsync(Constants.Tables.Tasks.ToString(), convertedtask);
                return rowEffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Task Is Not Created");
            }
        }

        public async Task<bool> UpdateTask(Tasks task)
        {
            try
            {
                int rowEffected = await dataProvider.UpdateByIdAsync<Tasks>(Constants.Tables.Tasks.ToString(), task);
                return rowEffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Task Is Not Updated");
            }
        }


        public async Task<bool> DeleteTask(string id)
        {
            try
            {
                int rowEffected = await dataProvider.DeleteByIdAsync(Constants.Tables.Tasks.ToString(), id);
                return rowEffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Task Is Not Deleted");
            }
        }
    }
}
