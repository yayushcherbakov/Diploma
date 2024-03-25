using System.ComponentModel.DataAnnotations;

namespace AuditService.ApplicationServices.Models.Identities;

public class RegisterUserPayload
{
    [Required] [Display(Name = "Email")] 
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = null!;

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль")]
    public string PasswordConfirm { get; set; } = null!;

    [Required] [Display(Name = "Имя")] 
    public string FirstName { get; set; } = null!;

    [Required] [Display(Name = "Фамилия")] 
    public string LastName { get; set; } = null!;

    [Display(Name = "Отчество")] public string? MiddleName { get; set; }
    
    [Required] 
    [Display(Name = "Номер телефона")]
    public string? PhoneNumber { get; set; }
    
    [Display(Name = "Фото")]
    public string? Photo { get; set; }
    
    [Display(Name = "Описание")]
    public string? Description { get; set; }
}
