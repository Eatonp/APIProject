using AutoMapper;

namespace TodoApi.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoAPI.Data.Models.TodoItem, TodoApi.Models.TodoItem>();
        }
    }
}
