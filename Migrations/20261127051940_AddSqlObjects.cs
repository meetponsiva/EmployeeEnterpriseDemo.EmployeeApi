using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeApi.Migrations
{
    public partial class AddSqlObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Scalar Function
            migrationBuilder.Sql(@"
                CREATE OR ALTER FUNCTION [dbo].[fn_FormatEmployeeName]
                (
                    @Name NVARCHAR(100),
                    @DepartmentId INT
                )
                RETURNS NVARCHAR(150)
                AS
                BEGIN
                    RETURN @Name + ' (Dept #' + CAST(@DepartmentId AS NVARCHAR(10)) + ')';
                END
            ");

            // 2. View
            migrationBuilder.Sql(@"
                CREATE OR ALTER VIEW [dbo].[vw_EmployeeDirectory]
                AS
                SELECT
                    e.[Id],
                    e.[Name],
                    e.[DepartmentId],
                    ISNULL(d.[DepartmentName], 'Unassigned') AS [DepartmentName]
                FROM [dbo].[EmployeeDetails] e
                LEFT JOIN [dbo].[DepartmentDetails] d ON e.[DepartmentId] = d.[Id];
            ");

            // 3. Stored Procedure
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllEmployees]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT [Id], [Name], [DepartmentId]
                    FROM [dbo].[vw_EmployeeDirectory]
                    ORDER BY [Id] DESC;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[sp_GetAllEmployees];");
            migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vw_EmployeeDirectory];");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS [dbo].[fn_FormatEmployeeName];");
        }
    }
}
