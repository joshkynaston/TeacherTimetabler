using AutoMapper;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.DTOs;

public class EntityDTOFactory(IMapper mapper) : IEntityDTOFactory
{
  private readonly IMapper _mapper = mapper;

  public object? CreateDto(OwnedEntity entity)
  {
    return entity switch
    {
      Class classEntity => _mapper.Map<GetClassDTO>(classEntity),
      Timeslot timeslotEntity => _mapper.Map<GetTimeslotDTO>(timeslotEntity),
      _ => null,
    };
  }
}
