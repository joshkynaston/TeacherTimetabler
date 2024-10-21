using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TeacherTimetabler.Api.Data;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Mappings;
using TeacherTimetabler.Api.Models;
using TeacherTimetabler.Api.Services;

namespace TeacherTimetabler.Api.Tests.Services;

public class ClassServiceTests
{
  public class GetClassByIdAsyncTests : ClassServiceTestsBase
  {
    [Theory]
    [InlineData(1, true)]
    [InlineData(2, false)]
    public async Task GetClassForUserIdByAsync_ShouldValidateClassId(int classId, bool isValid)
    {
      // Arrange
      var (testUser, testClass) = CreatePairedTestUserAndClass(classId: 1);

      _context.Users.Add(testUser);
      _context.Classes.Add(testClass);
      _context.SaveChanges();

      // Act
      var result = await _classService.GetClassByIdAsync(testUser.Id, classId);
      var testClassDTO = _mapper.Map<GetClassDTO>(result);

      // Assert
      if (isValid)
      {
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(testClassDTO);
      }
      else
      {
        result.Should().BeNull();
      }
    }
  }

  public abstract class ClassServiceTestsBase
  {
    protected readonly IFixture _fixture;
    protected readonly AppDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly ClassService _classService;

    protected ClassServiceTestsBase()
    {
      _fixture = new Fixture().Customize(new AutoMoqCustomization());
      _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
      _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

      _context = new(new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options);
      _context.Database.EnsureDeleted();

      _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper();

      _classService = new(_context, _mapper);
    }

    protected (Teacher, Class) CreatePairedTestUserAndClass(int classId = 1)
    {
      var testUser = _fixture.Build<Teacher>().Create();

      var testClass = _fixture
        .Build<Class>()
        .With(c => c.Id, classId)
        .With(c => c.TeacherId, testUser.Id)
        .With(c => c.Teacher, testUser)
        .Create();

      return (testUser, testClass);
    }
  }
}
