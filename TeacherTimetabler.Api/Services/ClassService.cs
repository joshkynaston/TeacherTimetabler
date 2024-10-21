using AutoMapper;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Models;
using TeacherTimetabler.Api.Repositories;

namespace TeacherTimetabler.Api.Services;

public class ClassService(IClassRepository classRepository, IMapper mapper) : IClassService
{
  private readonly IClassRepository _classRepository = classRepository;
  private readonly IMapper _mapper = mapper;

  public async Task<GetClassDTO?> GetClassByIdAsync(string userId, int classId)
  {
    Class? classEntity = await _classRepository.GetAsync(userId, classId);
    return classEntity is not null ? _mapper.Map<GetClassDTO>(classEntity) : null;
  }

  public async Task<GetClassDTO?> AddClassAsync(string userId, PostClassDTO postClassDTO)
  {
    var classEntity = _mapper.Map<Class>(postClassDTO);
    classEntity.TeacherId = userId;

    await _classRepository.AddAsync(classEntity);
    await _classRepository.SaveChangesAsync();

    var classDTO = _mapper.Map<GetClassDTO>(classEntity);
    return classDTO;
  }

  public Task<bool> DeleteClassAsync(string user, int classId)
  {
    throw new NotImplementedException();
  }

  public Task<GetClassDTO?> GetClassByNameAsync(string userId, string name)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<GetClassDTO>> GetClassesAsync(string userId)
  {
    throw new NotImplementedException();
  }
}
