using Demo.DAL.Models.DepartmentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configurations
{
    public class DepartmentConfigurations : BaseEntityConfigurations<Department>,IEntityTypeConfiguration<Department>
    {
        public new void  Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(d => d.Name).HasColumnType("varchar(20)").IsRequired();
            builder.Property(d => d.Code).HasColumnType("varchar(20)").IsRequired();
            builder.Property(d => d.Description).HasColumnType("varchar(50)");
            builder.Property(d => d.IsDeleted).HasDefaultValue(false);

            base.Configure(builder);

        }
    }
}
