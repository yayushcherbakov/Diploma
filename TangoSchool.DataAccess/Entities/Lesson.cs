using TangoSchool.DataAccess.Enums;

namespace TangoSchool.DataAccess.Entities;

public class Lesson
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public LessonType LessonType { get; set; }

    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset FinishTime { get; set; }

    public Guid ClassroomId { get; set; }

    public Classroom Classroom { get; set; } = null!;

    public Guid TeacherId { get; set; }

    public Teacher Teacher { get; set; } = null!;

    public Guid? StudentId { get; set; }

    public Student? Student { get; set; } = null!;

    public Guid? GroupId { get; set; }

    public Group? Group { get; set; } = null!;

    public bool Terminated { get; set; }

    public ICollection<Subscription> UsedSubscriptions { get; set; } = new List<Subscription>();
}
