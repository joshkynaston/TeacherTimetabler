using AutoMapper;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    // GetDTOs
    CreateMap<Class, GetClassDTO>();
    CreateMap<Timeslot, GetTimeslotDTO>();
    CreateMap<Timetable, GetTimetableDTO>();
    CreateMap<ItemInstance, GetItemInstanceDTO>();

    CreateMap<Class, PostClassDto>();
  }
}
