using AutoMapper;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Models;
using TeacherTimetabler.Api.Repositories;

namespace TeacherTimetabler.Api.Services;

public class ClassService(IOwnedRepo<Class> classRepository, IMapper mapper) : IClassService
{
  private readonly IOwnedRepo<Class> _classRepository = classRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<GetClassDTO?> GetClassAsync(string teacherId, int classId)
  {
    Class? classEntity = await _classRepository.GetAsync(teacherId, classId);
    return classEntity is not null ? _mapper.Map<GetClassDTO>(classEntity) : null;
  }

  public async Task<GetClassDTO?> AddClassAsync(string teacherId, PostClassDTO postClassDTO)
  {
    var classEntity = _mapper.Map<Class>(postClassDTO);
    classEntity.TeacherId = teacherId;

    await _classRepository.AddAsync(classEntity);
    await _classRepository.SaveChangesAsync();

    return _mapper.Map<GetClassDTO>(classEntity);
  }

  public async Task<bool> DeleteClassAsync(string teacherId, int classId)
  {
    Class? classEntity = await _classRepository.GetAsync(teacherId, classId);

    if (classEntity is null)
    {
      return false;
    }

    _classRepository.Delete(classEntity);
    await _classRepository.SaveChangesAsync();

    return true;
  }

  public async Task<IEnumerable<GetClassDTO>> GetClassesAsync(string teacherId)
  {
    IEnumerable<Class> classEntities = await _classRepository.GetAllAsync(teacherId);
    List<GetClassDTO> classDTOs = classEntities.Select(_mapper.Map<GetClassDTO>).ToList();
    return classDTOs;
  }
}
