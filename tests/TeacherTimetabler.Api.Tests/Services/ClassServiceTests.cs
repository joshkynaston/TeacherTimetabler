using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;
using TeacherTimetabler.Api.Models;
using TeacherTimetabler.Api.Services;
using TeacherTimetabler.Api.Mappings;
using TeacherTimetabler.Api.DTOs;

namespace TeacherTimetabler.Api.Tests.Services;

public class ClassServiceTests
{
    private readonly ClassService _classService;
    private readonly AppDbContext _context;
    private readonly Teacher _mock_user;

    public ClassServiceTests()
    {
        // Use InMemoryDatabase to simulate database
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);

        _context.Database.EnsureDeleted();

        _context.Users.Add(new Teacher { FirstName = "John", LastName = "Smith" });

        _context.SaveChanges();

        _mock_user = _context.Users.First(e => e.FirstName == "John");

        _context.Classes.Add(
            new Class
            {
                Id = 1,
                Name = "Math",
                Subject = "Mathematics",
                TeacherId = _mock_user.Id,
                Teacher = _mock_user,
            }
        );

        _context.SaveChanges();

        IMapper mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile(new MappingProfile())
        ).CreateMapper();

        // Set up service with the context
        _classService = new ClassService(_context, mapper);
    }

    [Fact]
    public async Task GetClassByIdAsync_ReturnsClassDTO_WhenClassExists()
    {
        ClassDTO? result = await _classService.GetClassByIdAsync(_mock_user.Id, 1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Math", result.Name);
        Assert.Equal("Mathematics", result.Subject);
    }

    [Fact]
    public async Task GetClassByIdAsync_ReturnsNull_WhenClassDoesNotExist()
    {
        ClassDTO? result = await _classService.GetClassByIdAsync(_mock_user.Id, 2);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateClassAsync_ReturnsClassDTO_WhenClassIsCreated()
    {
        var postClassDTO = new PostClassDTO { Name = "Science", Subject = "Physics" };

        (bool success, ClassDTO? result, _) = await _classService.CreateClassAsync(
            _mock_user.Id,
            postClassDTO
        );

        Assert.True(success);
        Assert.NotNull(result);
        Assert.Equal("Science", result.Name);
        Assert.IsType<ClassDTO>(result);
    }
}
