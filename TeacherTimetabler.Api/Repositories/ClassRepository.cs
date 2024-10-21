using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Repositories;

public class ClassRepository(AppDbContext context) : OwnedRepository<Class>(context), IClassRepository
{
  public async Task<Class?> GetByNameAsync(string userId, string className)
  {
    return await _dbSet.FirstOrDefaultAsync(c => c.TeacherId == userId && c.Name == className);
  }
}
