using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Poll.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TasksId = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tasks", x => x.TasksId);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    VoteId = table.Column<Guid>(nullable: false),
                    employeeid = table.Column<Guid>(nullable: false),
                    taskid = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(unicode: false, maxLength: 4000, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "(now() at time zone 'utc')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_votes", x => x.VoteId);
                    table.ForeignKey(
                        name: "FK_Vote_Employee",
                        column: x => x.employeeid,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vote_Tasks",
                        column: x => x.taskid,
                        principalTable: "Tasks",
                        principalColumn: "TasksId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeId",
                table: "Employee",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TasksId",
                table: "Tasks",
                column: "TasksId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_votes_employeeid",
                table: "Vote",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_VoteId",
                table: "Vote",
                column: "VoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vote_taskid",
                table: "Vote",
                column: "taskid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
