namespace TeacherTimetabler.Api.DTOs;

public record GetClassDTO( //
  int Id,
  string Name,
  string? Subject,
  int TeacherId
);

public record GetTimeslotDTO( //
  int Id,
  string Name,
  TimeSpan StartTime,
  TimeSpan EndTime,
  int TimetableId,
  int TeacherId
);

public record GetTimetableDTO( //
  int Id,
  string AcademicYear,
  bool IsBiWeekly,
  int TeacherId
);

public record GetItemInstanceDTO( //
  int Id,
  string ActivityType,
  string? Title,
  string? Description,
  int WeekInstanceId,
  int ClassId,
  int TeacherId
);
