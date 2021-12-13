using AutoMapper;
using Tasks.Application.Models;
using Tasks.Domain.Entities;
using Tasks.Domain.Enums;

namespace Tasks.Application.Mappers
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {

            CreateMap<Task, TaskReadDto>();
            CreateMap<Task, TaskPublishedDto>();

            CreateMap<TaskCreateDto, Task>()
                .ForMember(dest => dest.Status, option => option.MapFrom(src => Status.OPEN));
        }
    }
}