using System.ComponentModel.DataAnnotations;

namespace AuditService.ApplicationServices.Models.Identities;

public class ChangePasswordRequest
{
    [Required]
    public string CurrentPassword { get; set; } = null!;

    [Required]
    public string NewPassword { get; set; } = null!;
}