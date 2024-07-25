using AutoMapper;
using TaskManagement.Data.DTO;
using TaskManagement.Data.Models;

namespace TaskManagement.Common
{
    public class TaskManagementMapper:Profile
    {
        public TaskManagementMapper() 
        {
            CreateMap<TasksDto, Tasks>();
        }
    }
}
