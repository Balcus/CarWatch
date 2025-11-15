using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.DataAccess.Abstractions;
using Api.DataAccess.Enums;

namespace Api.DataAccess.Entities;

public class Report : IEntityBase<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
    public decimal Latitude { get; set; }
    
    [Required]
    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
    public decimal Longitude { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(500)] public string ImageUrl { get; set; } = "";
    
    [Required]
    public Status Status { get; set; } = Status.Pending;

    [Required]
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}