using TeacherTimetabler.Api.DTOs;

namespace TeacherTimetabler.Api.Services;

public interface IClassService
{
  Task<GetClassDTO?> GetClassAsync(string userId, int classId);
  Task<IEnumerable<GetClassDTO>> GetClassesAsync(string userId);
  Task<GetClassDTO?> AddClassAsync(string userId, PostClassDTO postClassDTO);
  Task<bool> DeleteClassAsync(string user, int classId);
}
