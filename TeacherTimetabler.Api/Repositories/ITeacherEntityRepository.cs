using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Repositories;

public interface ITeacherEntityRepository<T>
  where T : class, IOwnedByTeacher
{
  Task<T?> GetAsync(string userId, int entityId);
  Task<IEnumerable<T>> GetAllAsync(string userId);
  Task AddAsync(T entity);
  void Update(T entity);
  void Delete(T entity);
  Task SaveChangesAsync();
}
