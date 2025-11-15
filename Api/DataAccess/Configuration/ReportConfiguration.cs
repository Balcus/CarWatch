using Api.DataAccess.Entities;
using Api.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataAccess.Configuration;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports");
        
        builder.HasData(
            new Report
            {
                Id = 1,
                Latitude = 44.4268m,
                Longitude = 26.1025m,
                Description = "Pothole on main street causing traffic issues",
                ImageUrl = "https://example.com/images/report1.jpg",
                Status = Status.Pending,
                UserId = 1
            },
            new Report
            {
                Id = 2,
                Latitude = 44.4325m,
                Longitude = 26.1039m,
                Description = "Broken street light near the park",
                ImageUrl = "https://example.com/images/report2.jpg",
                Status = Status.Pending,
                UserId = 1
            },
            new Report
            {
                Id = 3,
                Latitude = 44.4410m,
                Longitude = 26.0960m,
                Description = "Graffiti on public building wall",
                ImageUrl = "https://example.com/images/report3.jpg",
                Status = Status.Pending,
                UserId = 1
            }
        );
    }
}