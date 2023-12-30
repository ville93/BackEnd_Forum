using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyChat.Migrations
{
    /// <inheritdoc />
    public partial class classes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discussions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discussions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discussions_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscussionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Discussions_DiscussionId",
                        column: x => x.DiscussionId,
                        principalTable: "Discussions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Business" },
                    { 2, "Celebrity" },
                    { 3, "Gaming" },
                    { 4, "General" },
                    { 5, "Politics" },
                    { 6, "Football Fever" }
                });

            migrationBuilder.InsertData(
                table: "Discussions",
                columns: new[] { "Id", "ChannelId", "CreatedAt", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 12, 30, 10, 14, 12, 717, DateTimeKind.Local).AddTicks(9132), "Entrepreneurship Tips" },
                    { 2, 1, new DateTime(2023, 12, 30, 9, 14, 12, 717, DateTimeKind.Local).AddTicks(9189), "Marketing Strategies" },
                    { 3, 2, new DateTime(2023, 12, 30, 10, 14, 12, 717, DateTimeKind.Local).AddTicks(9192), "Recent Celebrity News" },
                    { 4, 2, new DateTime(2023, 12, 30, 9, 14, 12, 717, DateTimeKind.Local).AddTicks(9194), "Favorite Celebrity Moments" },
                    { 5, 3, new DateTime(2023, 12, 30, 10, 14, 12, 717, DateTimeKind.Local).AddTicks(9197), "Favorite Video Games" },
                    { 6, 3, new DateTime(2023, 12, 30, 9, 14, 12, 717, DateTimeKind.Local).AddTicks(9199), "Upcoming Game Releases" },
                    { 7, 4, new DateTime(2023, 12, 30, 10, 14, 12, 717, DateTimeKind.Local).AddTicks(9202), "Favorite Books" },
                    { 8, 4, new DateTime(2023, 12, 30, 9, 14, 12, 717, DateTimeKind.Local).AddTicks(9205), "What is happening?" },
                    { 9, 5, new DateTime(2023, 12, 30, 10, 14, 12, 717, DateTimeKind.Local).AddTicks(9207), "Election Season" },
                    { 10, 5, new DateTime(2023, 12, 30, 9, 14, 12, 717, DateTimeKind.Local).AddTicks(9209), "Foreign Relations" },
                    { 11, 6, new DateTime(2023, 12, 30, 10, 14, 12, 717, DateTimeKind.Local).AddTicks(9212), "Who's excited for the upcoming football season?" },
                    { 12, 6, new DateTime(2023, 12, 30, 9, 14, 12, 717, DateTimeKind.Local).AddTicks(9215), "Basketball Talk" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "DiscussionId", "Timestamp" },
                values: new object[,]
                {
                    { 1, "What are some valuable tips for starting a successful business?", 1, new DateTime(2023, 12, 30, 10, 44, 12, 717, DateTimeKind.Local).AddTicks(9260) },
                    { 2, "In my experience, focusing on customer needs is crucial!", 1, new DateTime(2023, 12, 30, 10, 54, 12, 717, DateTimeKind.Local).AddTicks(9266) },
                    { 3, "Let's discuss effective marketing strategies.", 2, new DateTime(2023, 12, 30, 10, 34, 12, 717, DateTimeKind.Local).AddTicks(9269) },
                    { 4, "I've found social media marketing to be quite impactful.", 2, new DateTime(2023, 12, 30, 10, 59, 12, 717, DateTimeKind.Local).AddTicks(9272) },
                    { 5, "What's the latest celebrity gossip or news you've heard?", 3, new DateTime(2023, 12, 30, 10, 44, 12, 717, DateTimeKind.Local).AddTicks(9274) },
                    { 6, "I heard there's a new blockbuster movie in the making!", 3, new DateTime(2023, 12, 30, 10, 54, 12, 717, DateTimeKind.Local).AddTicks(9279) },
                    { 7, "What are some of your all-time favorite celebrity moments?", 4, new DateTime(2023, 12, 30, 10, 34, 12, 717, DateTimeKind.Local).AddTicks(9281) },
                    { 8, "I loved that awards ceremony last year. Memorable performances!", 4, new DateTime(2023, 12, 30, 10, 59, 12, 717, DateTimeKind.Local).AddTicks(9284) },
                    { 9, "What are your all-time favorite video games?", 5, new DateTime(2023, 12, 30, 10, 44, 12, 717, DateTimeKind.Local).AddTicks(9286) },
                    { 10, "I can't get enough of The Witcher 3. What about you?", 5, new DateTime(2023, 12, 30, 10, 54, 12, 717, DateTimeKind.Local).AddTicks(9290) },
                    { 11, "Any exciting upcoming game releases you're looking forward to?", 6, new DateTime(2023, 12, 30, 10, 34, 12, 717, DateTimeKind.Local).AddTicks(9292) },
                    { 12, "I can't wait for the next installment of the Assassin's Creed series!", 6, new DateTime(2023, 12, 30, 10, 59, 12, 717, DateTimeKind.Local).AddTicks(9295) },
                    { 13, "Share your favorite books and why you love them.", 7, new DateTime(2023, 12, 30, 10, 44, 12, 717, DateTimeKind.Local).AddTicks(9297) },
                    { 14, "I recently read 'The Great Gatsby' – such a classic!", 7, new DateTime(2023, 12, 30, 10, 54, 12, 717, DateTimeKind.Local).AddTicks(9299) },
                    { 15, "Discussing important things.", 8, new DateTime(2023, 12, 30, 10, 34, 12, 717, DateTimeKind.Local).AddTicks(9302) },
                    { 16, "Any thoughts?", 8, new DateTime(2023, 12, 30, 10, 59, 12, 717, DateTimeKind.Local).AddTicks(9304) },
                    { 17, "With the election season approaching, what are your predictions?", 9, new DateTime(2023, 12, 30, 10, 44, 12, 717, DateTimeKind.Local).AddTicks(9307) },
                    { 18, "I believe the debates will play a crucial role this time.", 9, new DateTime(2023, 12, 30, 10, 54, 12, 717, DateTimeKind.Local).AddTicks(9311) },
                    { 19, "Let's discuss the current state of foreign relations.", 10, new DateTime(2023, 12, 30, 10, 34, 12, 717, DateTimeKind.Local).AddTicks(9313) },
                    { 20, "The recent summit had some interesting outcomes.", 10, new DateTime(2023, 12, 30, 10, 59, 12, 717, DateTimeKind.Local).AddTicks(9316) },
                    { 21, "I can't wait to see my favorite team in action!", 10, new DateTime(2023, 12, 30, 10, 44, 12, 717, DateTimeKind.Local).AddTicks(9318) },
                    { 22, "Predictions for the championship winner?", 11, new DateTime(2023, 12, 30, 10, 54, 12, 717, DateTimeKind.Local).AddTicks(9321) },
                    { 23, "Let's discuss the latest basketball games!", 12, new DateTime(2023, 12, 30, 10, 34, 12, 717, DateTimeKind.Local).AddTicks(9323) },
                    { 24, "Who's your favorite basketball team?", 12, new DateTime(2023, 12, 30, 10, 59, 12, 717, DateTimeKind.Local).AddTicks(9326) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_ChannelId",
                table: "Discussions",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DiscussionId",
                table: "Messages",
                column: "DiscussionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Discussions");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "AspNetUsers");
        }
    }
}
