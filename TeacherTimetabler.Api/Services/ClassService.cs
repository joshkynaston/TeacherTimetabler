using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Interfaces;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Services;

public class ClassService(AppDbContext dbCtx, IMapper mapper) : IClassService
{
    private readonly AppDbContext _dbCtx = dbCtx;
    private readonly IMapper _mapper = mapper;

    public async Task<ClassDTO?> GetClassByIdAsync(string userId, int classId)
    {
        Class? classEntity = await _dbCtx.Classes.FirstOrDefaultAsync(c =>
            c.Id == classId && c.UserId == userId
        );

        if (classEntity is null)
        {
            return null;
        }
        else
        {
            return _mapper.Map<ClassDTO>(classEntity);
        }
    }

    public async Task<ClassDTO?> GetClassByNameAsync(string userId, string name)
    {
        Class? classEntity = await _dbCtx.Classes.FirstOrDefaultAsync(c =>
            c.Name == name && c.UserId == userId
        );

        if (classEntity is null)
        {
            return null;
        }

        return _mapper.Map<ClassDTO>(classEntity);
    }

    public async Task<IEnumerable<ClassDTO>> GetClassesAsync(string userId)
    {
        List<ClassDTO> classEntities = await _dbCtx
            .Classes.Where(c => c.UserId == userId)
            .Select(c => _mapper.Map<ClassDTO>(c))
            .ToListAsync();

        return classEntities;
    }

  public async Task<GetClassDTO?> AddClassAsync(string userId, PostClassDTO postClassDTO)
    {
        var user = await _dbCtx.Users.FindAsync(userId);
        if (user is null)
        {
            return null;
        }

        // Create the class entity
        var classEntity = new Class
        {
            Name = postClassDTO.Name,
            Subject = postClassDTO.Subject,
            UserId = userId,
            User = user,
        };

        await _dbCtx.Classes.AddAsync(classEntity);
        await _dbCtx.SaveChangesAsync();

    var classDTO = _mapper.Map<GetClassDTO>(classEntity);
    return classDTO;
    }

    public async Task<bool> DeleteClassAsync(string userId, int id)
    {
        // get ClassEntity from db
        Class? classEntity = await _dbCtx.Classes.FirstAsync(c => c.Id == id && c.UserId == userId);
        _dbCtx.Classes.Remove(classEntity);
        await _dbCtx.SaveChangesAsync();

        if (classEntity != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
