using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Media.Migrations
{
    /// <inheritdoc />
    public partial class updateTableUser_and_createnewOtherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundProfile",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "addressID",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name_with_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "followers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userFollowerID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_followers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_followers_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "followings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pageFollowerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userFollowerID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_followings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_followings_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "type_friends",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_friends", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "friends",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type_FriendsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friends", x => x.ID);
                    table.ForeignKey(
                        name: "FK_friends_type_friends_Type_FriendsID",
                        column: x => x.Type_FriendsID,
                        principalTable: "type_friends",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_addressID",
                table: "users",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_followers_userID",
                table: "followers",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_followings_userID",
                table: "followings",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_friends_Type_FriendsID",
                table: "friends",
                column: "Type_FriendsID");

            migrationBuilder.AddForeignKey(
                name: "FK_users_addresses_addressID",
                table: "users",
                column: "addressID",
                principalTable: "addresses",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_addresses_addressID",
                table: "users");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "followers");

            migrationBuilder.DropTable(
                name: "followings");

            migrationBuilder.DropTable(
                name: "friends");

            migrationBuilder.DropTable(
                name: "type_friends");

            migrationBuilder.DropIndex(
                name: "IX_users_addressID",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "users");

            migrationBuilder.DropColumn(
                name: "BackgroundProfile",
                table: "users");

            migrationBuilder.DropColumn(
                name: "addressID",
                table: "users");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "users");
        }
    }
}
