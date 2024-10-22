using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Repositories;

public interface IOwnedRepo<T>
  where T : OwnedEntity
{
  Task<T?> GetAsync(string teacherId, int entityId);
  Task<IEnumerable<T>> GetAllAsync(string userId);
  Task AddAsync(T entity);
  void Update(T entity);
  void Delete(T entity);
  Task SaveChangesAsync();
}
