using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;
using TeacherTimetabler.Api.Models.DTOs;
using TeacherTimetabler.Api.Models.Entities;

namespace TeacherTimetabler.Api.Services;

public class ClassService(AppDbContext dbCtx)
{
    private readonly AppDbContext _dbCtx = dbCtx;

    public async Task<ClassDTO?> GetClassByIdAsync(string userId, int entityId)
    {
        ClassEntity? classEntity = await _dbCtx.Classes.FirstOrDefaultAsync(c =>
            c.Id == entityId && c.UserEntityId == userId
        );

        if (classEntity is null)
        {
            return null;
        }
        else
        {
            return new ClassDTO
            {
                Id = classEntity.Id,
                Name = classEntity.Name,
                Subject = classEntity.Subject,
            };
        }
    }

    public async Task<IEnumerable<ClassDTO>> GetClassesAsync(string userId)
    {
        List<ClassDTO> classEntities = await _dbCtx
            .Classes.Where(c => c.UserEntityId == userId)
            .Select(c => new ClassDTO
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
            })
            .ToListAsync();

        return classEntities;
    }

    public async Task<(bool success, ClassDTO? getClassDTO, string? location)> CreateClassAsync(
        string userId,
        PostClassDTO postClassDTO
    )
    {
        // Get user from the database
        AppUserEntity? user =
            await _dbCtx.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new InvalidOperationException("User not found");

        // Create the class entity
        var classEntity = new ClassEntity
        {
            Name = postClassDTO.Name,
            Subject = postClassDTO.Subject,
            UserEntityId = user.Id,
            UserEntity = user,
        };

        await _dbCtx.Classes.AddAsync(classEntity);
        await _dbCtx.SaveChangesAsync();

        // Create the response DTO
        var getClassDTO = new ClassDTO
        {
            Id = classEntity.Id,
            Name = classEntity.Name,
            Subject = classEntity.Subject,
        };

        var location = $"/api/classes/{classEntity.Id}";

        return (true, getClassDTO, location);
    }

    public async Task<(bool isDeleted, ClassDTO? getClassDTO)> DeleteClassAsync(string userId, int id)
    {
        // get ClassEntity from db
        ClassEntity? classEntity = await _dbCtx.Classes.FirstAsync(c => c.Id == id && c.UserEntityId == userId);
        _dbCtx.Classes.Remove(classEntity);
        await _dbCtx.SaveChangesAsync();

        if (classEntity != null)
        {
            return (true, new ClassDTO{
                Id = id,
                Name = classEntity.Name,
                Subject = classEntity.Subject
            });
        } else {
            return (false, null);
        }
    }
}
