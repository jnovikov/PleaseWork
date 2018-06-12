using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 150, nullable: true),
                    Password = table.Column<string>(maxLength: 150, nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "Deadlines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Finish = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deadlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deadlines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    WorkTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadlineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Deadlines_DeadlineId",
                        column: x => x.DeadlineId,
                        principalTable: "Deadlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deadlines_UserId",
                table: "Deadlines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_DeadlineId",
                table: "Tasks",
                column: "DeadlineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Deadlines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}