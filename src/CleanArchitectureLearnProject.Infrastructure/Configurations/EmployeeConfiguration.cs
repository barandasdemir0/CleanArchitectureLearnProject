using CleanArchitectureLearnProject.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureLearnProject.Infrastructure.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.OwnsOne(p => p.PersonelInformation, builder =>
        {
            builder.Property(i => i.TCNo).HasColumnName("TCNO");
            builder.Property(i => i.Phone1).HasColumnName("Phone1");
            builder.Property(i => i.Phone2).HasColumnName("Phone2");
            builder.Property(i => i.Email).HasColumnName("Email");
        });
        builder.OwnsOne(p => p.Address, builder =>
        {
            builder.Property(a => a.Country).HasColumnName("Country");
            builder.Property(a => a.City).HasColumnName("City");
            builder.Property(a => a.Town).HasColumnName("Town");
            builder.Property(a => a.FullAddress).HasColumnName("FullAddress");
        });
        builder.Property(p => p.Salary).HasColumnType("money");
    }
}
