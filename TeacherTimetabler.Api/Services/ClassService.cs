using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Services;

public class ClassService(AppDbContext dbCtx, IMapper mapper)
{
    private readonly AppDbContext _dbCtx = dbCtx;
    private readonly IMapper _mapper = mapper;

    public async Task<ClassDTO?> GetClassByIdAsync(string userId, int entityId)
    {
        Class? classEntity = await _dbCtx.Classes.FirstOrDefaultAsync(c =>
            c.Id == entityId && c.TeacherId == userId
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
            c.Name == name && c.TeacherId == userId
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
            .Classes.Where(c => c.TeacherId == userId)
            .Select(c => _mapper.Map<ClassDTO>(c))
            .ToListAsync();

        return classEntities;
    }

    public async Task<(bool success, ClassDTO? getClassDTO, string? location)> CreateClassAsync(
        string userId,
        PostClassDTO postClassDTO
    )
    {
        // Get user from the database
        Teacher? user =
            await _dbCtx.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new InvalidOperationException("User not found");

        // Create the class entity
        var classEntity = new Class
        {
            Name = postClassDTO.Name,
            Subject = postClassDTO.Subject,
            TeacherId = user.Id,
            Teacher = user,
        };

        await _dbCtx.Classes.AddAsync(classEntity);
        await _dbCtx.SaveChangesAsync();

        // Create the response DTO
        var getClassDTO = _mapper.Map<ClassDTO>(classEntity);

        var location = $"/api/classes/{classEntity.Id}";

        return (true, getClassDTO, location);
    }

    public async Task<bool> DeleteClassAsync(
        string userId,
        int id
    )
    {
        // get ClassEntity from db
        Class? classEntity = await _dbCtx.Classes.FirstAsync(c =>
            c.Id == id && c.TeacherId == userId
        );
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
