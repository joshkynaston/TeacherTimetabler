using AutoMapper;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Class, ClassDTO>();
    }
}
