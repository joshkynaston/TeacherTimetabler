using TeacherTimetabler.Api.DTOs;

namespace TeacherTimetabler.Api.Services;

public interface IClassService
{
  Task<GetClassDto?> GetClassAsync(string userId, int classId);
  Task<IEnumerable<GetClassDto>> GetClassesAsync(string userId);
  Task<GetClassDto?> AddClassAsync(string userId, PostClassDto postClassDto);
  Task<bool> DeleteClassAsync(string user, int classId);
}
