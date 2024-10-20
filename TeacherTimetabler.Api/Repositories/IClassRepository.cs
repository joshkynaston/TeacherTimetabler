using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Repositories;

public interface IClassRepository
{
    Task<Class?> GetClassByIdAsync(string userId, int classId);
    Task<Class?> GetClassByNameAsync(string userId, string name);
    Task<IEnumerable<Class>> GetClassesAsync(string userId);
    Task AddClassAsync(Class classEntity);
    void RemoveClass(Class classEntity);
}
