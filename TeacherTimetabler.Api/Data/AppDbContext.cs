using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<Teacher>(options)
{
    public DbSet<Class> Classes { get; set; }
    public DbSet<Timetable> Timetables {get; set;}
    public DbSet<Timeslot> Timeslots {get; set;}
    public DbSet<RecurringItem> RecurringItems {get; set;}
    public DbSet <WeekInstance> WeekInstances {get; set;}
    public DbSet<ItemInstance> ItemInstances {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}