namespace TangoSchool.DataAccess.Entities;

public class Classroom
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Terminated { get; set; }
    
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
