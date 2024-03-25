using AuditService.DataAccess.Enums;

namespace AuditService.DataAccess.Entities;

public class Group
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public DanceProficiencyLevel Level { get; set; }
    
    public int MaxStudentCapacity { get; set; }
    
    public Guid TeacherId { get; set; }
    
    public Teacher Teacher { get; set; } = null!;
    
    public ICollection<StudentGroup> JoinedStudentGroups { get; set; } = new List<StudentGroup>();
    
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
