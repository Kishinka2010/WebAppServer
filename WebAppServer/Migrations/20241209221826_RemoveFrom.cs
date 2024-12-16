using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFrom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Employees_EmployeeId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_Employees_EmployeeId",
                table: "UserFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperience_Employees_EmployeeId",
                table: "WorkExperience");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "WorkExperience",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "UserFiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Educations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Employees_EmployeeId",
                table: "Educations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_Employees_EmployeeId",
                table: "UserFiles",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperience_Employees_EmployeeId",
                table: "WorkExperience",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Employees_EmployeeId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_Employees_EmployeeId",
                table: "UserFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperience_Employees_EmployeeId",
                table: "WorkExperience");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "WorkExperience",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "UserFiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Educations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Employees_EmployeeId",
                table: "Educations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_Employees_EmployeeId",
                table: "UserFiles",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperience_Employees_EmployeeId",
                table: "WorkExperience",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
