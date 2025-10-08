using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared;
using Demo.DAL.Models.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Demo.DAL.Data.Configurations
{
    public class EmployeeConfigurations : BaseEntityConfigurations<Employee>, IEntityTypeConfiguration<Employee>
    {
        public  new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Address).HasColumnType("varchar(150)");
            builder.Property(e => e.Salary).HasColumnType("decimal(10,2)");
            builder.Property(e => e.Gender).HasConversion(
                (empGender) => empGender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );
            builder.Property(e => e.EmployeeType).HasConversion(
                (empType) => empType.ToString(),
                (type) => (EmployeeType) Enum.Parse(typeof(EmployeeType) , type)
                );

            base.Configure(builder);
            /// validation of Age 
            // validation of address
        }
    }
}
