using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Migrations
{
    /// <inheritdoc />
    public partial class ConnectParentUserIdToToDoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // connect parenUserId on ToDo to id on User M:1
            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Users_ParentUserId",
                table: "ToDos",
                column: "ParentUserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Users_ParentUserId",
                table: "ToDos");
        }
    }
}
