using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using Moq;
using TeacherTimetabler.Api.DTOs;
using TeacherTimetabler.Api.Mappings;
using TeacherTimetabler.Api.Models;
using TeacherTimetabler.Api.Repositories;
using TeacherTimetabler.Api.Services;

namespace TeacherTimetabler.Api.Tests.Services;

public class ClassServiceTests
{
  public class GetClassByIdAsyncTests : ClassServiceTestsBase
  {
    [Theory]
    [InlineData(1, true)]
    [InlineData(2, false)]
    public async Task GetClassAsync_ShouldValidateClassId(int classId, bool isValid)
    {
      // Arrange
      (Teacher testTeacher, Class testClass) = CreatePairedTestUserAndClass(1);

      _mockClassRepository.Setup(r => r.GetAsync(testTeacher.Id, classId)).ReturnsAsync(isValid ? testClass : null);

      // Act
      GetClassDto? result = await _classService.GetClassAsync(testTeacher.Id, classId);

      // Assert
      if (isValid)
      {
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.Map<GetClassDto>(testClass));
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
    protected readonly Mock<IOwnedRepo<Class>> _mockClassRepository;
    protected readonly IMapper _mapper;
    protected readonly IClassService _classService;

    protected ClassServiceTestsBase()
    {
      _fixture = new Fixture().Customize(new AutoMoqCustomization());
      _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
      _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

      // Mock the repository
      _mockClassRepository = _fixture.Freeze<Mock<IOwnedRepo<Class>>>();

      // Create the mapper
      _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper();

      // Create the service with the mocked repository
      _classService = new ClassService(_mockClassRepository.Object, _mapper);
    }

    protected (Teacher, Class) CreatePairedTestUserAndClass(int classId = 1, string className = "testClass")
    {
      Teacher? testTeacher = _fixture.Build<Teacher>().Create();

      Class? testClass = _fixture
        .Build<Class>()
        .With(c => c.Id, classId)
        .With(c => c.TeacherId, testTeacher.Id)
        .With(c => c.Teacher, testTeacher)
        .With(c => c.ClassName, className)
        .Create();

      return (testTeacher, testClass);
    }
  }
}
