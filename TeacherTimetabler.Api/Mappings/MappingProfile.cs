using AutoMapper;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Class, GetClassDto>();
    CreateMap<Class, PostClassDto>();
  }
}
