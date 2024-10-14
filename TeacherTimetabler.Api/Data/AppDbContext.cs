using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<UserEntity>(options)
{
    public DbSet<ClassEntity> Classes { get; set; }
}
