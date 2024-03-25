using TangoSchool.DataAccess.Enums;

namespace TangoSchool.DataAccess.Entities;

public class Student
{
    public Guid Id { get; set; }
    
    public Guid ApplicationUserId { get; set; }
    
    public ApplicationUser ApplicationUser { get; set; } = null!;
    
    public DanceProficiencyLevel Level { get; set; }
    
    public ICollection<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();
    
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    
    public ICollection<LessonRequest> LessonRequests { get; set; } = new List<LessonRequest>();
}
