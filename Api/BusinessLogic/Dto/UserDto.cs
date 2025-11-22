using System.ComponentModel.DataAnnotations;

namespace Api.BusinessLogic.Dto;

public class UserDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    [Required(ErrorMessage = "Mail is required")]
    public string Mail { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must have at least 6 characters.")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "CNP is required.")]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "CNP must contain exactly 13 digits.")]
    public string CNP { get; set; }
}