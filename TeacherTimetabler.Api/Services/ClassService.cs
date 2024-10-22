using AutoMapper;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Models;
using TeacherTimetabler.Api.Repositories;

namespace TeacherTimetabler.Api.Services;

public class ClassService(IOwnedRepo<Class> classRepository, IMapper mapper) : IClassService
{
  public async Task<GetClassDTO?> GetClassAsync(string teacherId, int classId)
  {
    Class? classEntity = await classRepository.GetAsync(teacherId, classId);
    return classEntity is not null ? mapper.Map<GetClassDTO>(classEntity) : null;
  }

  public async Task<GetClassDTO?> AddClassAsync(string teacherId, PostClassDto postClassDto)
  {
    var classEntity = mapper.Map<Class>(postClassDto);
    classEntity.TeacherId = teacherId;

    await classRepository.AddAsync(classEntity);
    await classRepository.SaveChangesAsync();

    return mapper.Map<GetClassDTO>(classEntity);
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

  public async Task<IEnumerable<GetClassDTO>> GetClassesAsync(string teacherId)
  {
    IEnumerable<Class> classEntities = await classRepository.GetAllAsync(teacherId);
    List<GetClassDTO> classDtOs = classEntities.Select(mapper.Map<GetClassDTO>).ToList();
    return classDtOs;
  }

  public async Task TestFunc<T>(string teacherId, int entityId) { }
}
