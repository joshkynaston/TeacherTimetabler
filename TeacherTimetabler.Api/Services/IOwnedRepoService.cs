namespace TeacherTimetabler.Api.Services;

public interface IOwnedRepoService<TDTO>
{
  Task<TDTO?> GetAsync(TDTO DTO);
}
