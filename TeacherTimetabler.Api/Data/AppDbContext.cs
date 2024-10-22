using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Models;

namespace TeacherTimetabler.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<Teacher>(options)
{
  public DbSet<Class> Classes { get; init; }
  public DbSet<Timetable> Timetables { get; init; }
  public DbSet<Timeslot> Timeslots { get; init; }
  public DbSet<RecurringItem> RecurringItems { get; init; }
  public DbSet<WeekInstance> WeekInstances { get; init; }
  public DbSet<ItemInstance> ItemInstances { get; init; }
}
