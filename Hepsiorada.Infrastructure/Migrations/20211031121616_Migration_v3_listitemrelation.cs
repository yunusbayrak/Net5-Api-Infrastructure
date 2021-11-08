using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hepsiorada.Infrastructure.Migrations
{
    public partial class Migration_v3_listitemrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_UserLists_UserListId",
                table: "ListItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserListId",
                table: "ListItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_UserLists_UserListId",
                table: "ListItems",
                column: "UserListId",
                principalTable: "UserLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_UserLists_UserListId",
                table: "ListItems");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Categories");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserListId",
                table: "ListItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_UserLists_UserListId",
                table: "ListItems",
                column: "UserListId",
                principalTable: "UserLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
