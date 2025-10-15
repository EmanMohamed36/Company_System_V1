using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DAL.Data.Migrations
{
    public partial class CreateDepartmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ✅ Safely drop primary key only if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.key_constraints 
                           WHERE [name] = 'PK_TEntity' AND [parent_object_id] = OBJECT_ID('TEntity'))
                ALTER TABLE [TEntity] DROP CONSTRAINT [PK_TEntity];
            ");

            // ✅ Rename the table if it still exists
            migrationBuilder.Sql(@"
                IF OBJECT_ID('TEntity', 'U') IS NOT NULL
                EXEC sp_rename 'TEntity', 'Employees';
            ");

            // ✅ Add primary key to the new table name (safe)
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.key_constraints 
                               WHERE [name] = 'PK_Employees' AND [parent_object_id] = OBJECT_ID('Employees'))
                ALTER TABLE [Employees] ADD CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]);
            ");

            // ✅ Create Departments table
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10, 10"),
                    Name = table.Column<string>(type: "varchar(20)", nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, computedColumnSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Departments");

            // ✅ Safely drop Employees PK if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.key_constraints 
                           WHERE [name] = 'PK_Employees' AND [parent_object_id] = OBJECT_ID('Employees'))
                ALTER TABLE [Employees] DROP CONSTRAINT [PK_Employees];
            ");

            // ✅ Rename back to TEntity only if Employees exists
            migrationBuilder.Sql(@"
                IF OBJECT_ID('Employees', 'U') IS NOT NULL
                EXEC sp_rename 'Employees', 'TEntity';
            ");

            // ✅ Recreate PK for TEntity
            migrationBuilder.Sql(@"
                IF OBJECT_ID('TEntity', 'U') IS NOT NULL 
                   AND NOT EXISTS (SELECT 1 FROM sys.key_constraints 
                                   WHERE [name] = 'PK_TEntity' AND [parent_object_id] = OBJECT_ID('TEntity'))
                ALTER TABLE [TEntity] ADD CONSTRAINT [PK_TEntity] PRIMARY KEY ([Id]);
            ");
        }
    }
}
