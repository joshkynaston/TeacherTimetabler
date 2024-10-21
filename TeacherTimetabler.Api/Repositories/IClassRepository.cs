using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Repositories;

public interface IClassRepository : ITeacherEntityRepository<Class>
{
  Task<Class?> GetByNameAsync(string userId, string className);
}
