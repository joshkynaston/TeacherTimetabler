using TeacherTimetabler.Api.DTOs;

namespace TeacherTimetabler.Api.Services;

public interface IClassService
{
  Task<GetClassDTO?> GetClassByIdAsync(string userId, int classId);
  Task<GetClassDTO?> GetClassByNameAsync(string userId, string name);
  Task<IEnumerable<GetClassDTO>> GetClassesAsync(string userId);
  Task<GetClassDTO?> AddClassAsync(string userId, PostClassDTO postClassDTO);
  Task<bool> DeleteClassAsync(string user, int classId);
}
