using AutoMapper;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Models;
using TeacherTimetabler.Api.Repositories;

namespace TeacherTimetabler.Api.Services;

public class ClassService(IOwnedRepo<Class> classRepository, IMapper mapper) : IClassService
{
  public async Task<GetClassDto?> GetClassAsync(string teacherId, int classId)
  {
    Class? classEntity = await classRepository.GetAsync(teacherId, classId);
    return classEntity is not null ? mapper.Map<GetClassDto>(classEntity) : null;
  }

  public async Task<GetClassDto?> AddClassAsync(string teacherId, PostClassDto postClassDto)
  {
    var classEntity = mapper.Map<Class>(postClassDto);
    classEntity.TeacherId = teacherId;

    await classRepository.AddAsync(classEntity);
    await classRepository.SaveChangesAsync();

    return mapper.Map<GetClassDto>(classEntity);
  }

  public async Task<bool> DeleteClassAsync(string teacherId, int classId)
  {
    Class? classEntity = await classRepository.GetAsync(teacherId, classId);

    if (classEntity is null)
      return false;

    classRepository.Delete(classEntity);
    await classRepository.SaveChangesAsync();

    return true;
  }

  public async Task<IEnumerable<GetClassDto>> GetClassesAsync(string teacherId)
  {
    IEnumerable<Class> classEntities = await classRepository.GetAllAsync(teacherId);
    List<GetClassDto> classDtOs = classEntities.Select(mapper.Map<GetClassDto>).ToList();
    return classDtOs;
  }
}
