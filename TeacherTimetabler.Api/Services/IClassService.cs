using TeacherTimetabler.Api.DTOs;

namespace TeacherTimetabler.Api.Interfaces;

public interface IClassService
{
    Task<ClassDTO?> GetClassByIdAsync(string userId, int classId);
    Task<ClassDTO?> GetClassByNameAsync(string userId, string name);
    Task<IEnumerable<ClassDTO>> GetClassesAsync(string userId);
    Task<ClassDTO?> CreateClassAsync(string userId, PostClassDTO postClassDTO);
    Task<bool> DeleteClassAsync(string user, int classId);
}
