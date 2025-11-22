using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.DataAccess.Abstractions;
using Api.DataAccess.Enums;

namespace Api.DataAccess.Entities;

public class User : IEntityBase<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Required]
    public string CNP { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    [StringLength(256)]
    public string Mail { get; set; }
    
    [StringLength(100)]
    public string Password { get; set; }

    public Role Role { get; set; } = Role.Default;

    public bool Active { get; set; }
    
    public List<Report> Reports { get; set; } = new();
    
}