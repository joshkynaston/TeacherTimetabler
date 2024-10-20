using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Repositories;

public class ClassRepository(AppDbContext dbCtx) : IClassRepository
{
    private readonly AppDbContext _dbCtx = dbCtx;

    public async Task<Class?> GetClassByIdAsync(string userId, int classId)
    {
        return await _dbCtx.Classes.FirstOrDefaultAsync(c => c.Id == classId && c.UserId == userId);
    }

    public async Task<Class?> GetClassByNameAsync(string userId, string className)
    {
        return await _dbCtx.Classes.FirstOrDefaultAsync(c =>
            c.Name == className && c.UserId == userId
        );
    }

    public async Task<IEnumerable<Class>> GetClassesAsync(string userId)
    {
        return await _dbCtx.Classes.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task AddClassAsync(Class classEntity)
    {
        await _dbCtx.Classes.AddAsync(classEntity);
    }

    public void RemoveClass(Class classEntity)
    {
        _dbCtx.Classes.Remove(classEntity);
    }
}
