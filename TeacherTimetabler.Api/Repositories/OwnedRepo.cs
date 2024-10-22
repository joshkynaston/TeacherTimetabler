using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Repositories;

public class OwnedRepo<T> : IOwnedRepo<T>
  where T : OwnedEntity
{
  private readonly AppDbContext _context;
  private readonly DbSet<T> _dbSet;

  public OwnedRepo(AppDbContext context)
  {
    _context = context;
    _dbSet = _context.Set<T>();
  }

  public async Task AddAsync(T entity)
  {
    await _dbSet.AddAsync(entity);
  }

  public void Delete(T entity)
  {
    _dbSet.Remove(entity);
  }

  public async Task<IEnumerable<T>> GetAllAsync(string teacherId)
  {
    return await _dbSet.Where(c => c.TeacherId == teacherId).ToListAsync();
  }

  public async Task<T?> GetAsync(string teacherId, int entityId)
  {
    return await _dbSet.FirstOrDefaultAsync(c => c.TeacherId == teacherId && c.Id == entityId);
  }

  public void Update(T entity)
  {
    _dbSet.Update(entity);
  }

  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }
}
