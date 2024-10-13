using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Models.Entities;

namespace TeacherTimetabler.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<AppUserEntity>(options)
{
    public DbSet<ClassEntity> Classes { get; set; }
}
