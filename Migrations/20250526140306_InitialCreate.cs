using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeHierarchyApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "ManagerId", "Name", "Title", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9771), null, "John Smith", "CEO", new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9772) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsActive", "LastLoginAt", "LastName", "PasswordHash", "Role", "Username" },
                values: new object[] { 1, new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9854), "test@example.com", "Test", true, null, "Test", "1234", "Admin", "testuser" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "ManagerId", "Name", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9775), 1, "Sarah Johnson", "CTO", new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9775) },
                    { 3, new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9776), 1, "Mike Wilson", "VP Sales", new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9776) },
                    { 4, new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9777), 2, "Lisa Brown", "Engineering Manager", new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9778) },
                    { 7, new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9782), 3, "David Miller", "Sales Manager", new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9782) },
                    { 5, new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9779), 4, "Tom Davis", "Senior Developer", new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9779) },
                    { 6, new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9780), 4, "Anna Garcia", "Developer", new DateTime(2025, 5, 26, 14, 3, 6, 139, DateTimeKind.Utc).AddTicks(9780) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
