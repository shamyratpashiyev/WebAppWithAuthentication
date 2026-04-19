using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppWithAuthenticationApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Surname = table.Column<string>(type: "longtext", nullable: false),
                    Position = table.Column<string>(type: "longtext", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Position", "SecurityStamp", "Status", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "72a568297b6e492cb7159c92257d091e", "admin@email.com", false, new DateTime(2026, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "John", null, null, null, null, false, "Software Engineer", "f4d927c3e10a4865a9e3427f1020b941", 0, "Doe", false, "admin" },
                    { 2, 0, "72a568297b6e492cb7159c92257d091e", "m.rossi@email.com", false, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Marco", null, null, null, null, false, "Project Manager", "f4d927c3e10a4865a9e3427f1020b941", 2, "Rossi", false, "m.rossi" },
                    { 3, 0, "72a568297b6e492cb7159c92257d091e", "s.chen@email.com", false, new DateTime(2026, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Sarah", null, null, null, null, false, "UI/UX Designer", "f4d927c3e10a4865a9e3427f1020b941", 0, "Chen", false, "s.chen" },
                    { 4, 0, "72a568297b6e492cb7159c92257d091e", "a.smith@email.com", false, new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Alice", null, null, null, null, false, "DevOps Engineer", "f4d927c3e10a4865a9e3427f1020b941", 1, "Smith", false, "a.smith" },
                    { 5, 0, "72a568297b6e492cb7159c92257d091e", "h.tanaka@email.com", false, new DateTime(2026, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hiroshi", null, null, null, null, false, "Backend Developer", "f4d927c3e10a4865a9e3427f1020b941", 2, "Tanaka", false, "h.tanaka" },
                    { 6, 0, "72a568297b6e492cb7159c92257d091e", "e.dubois@email.com", false, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Elena", null, null, null, null, false, "QA Analyst", "f4d927c3e10a4865a9e3427f1020b941", 0, "Dubois", false, "e.dubois" },
                    { 7, 0, "72a568297b6e492cb7159c92257d091e", "j.kowalski@email.com", false, new DateTime(2026, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Jan", null, null, null, null, false, "Frontend Developer", "f4d927c3e10a4865a9e3427f1020b941", 1, "Kowalski", false, "j.kowalski" },
                    { 8, 0, "72a568297b6e492cb7159c92257d091e", "l.garcia@email.com", false, new DateTime(2026, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Luis", null, null, null, null, false, "Data Scientist", "f4d927c3e10a4865a9e3427f1020b941", 0, "Garcia", false, "l.garcia" },
                    { 9, 0, "72a568297b6e492cb7159c92257d091e", "m.ahmed@email.com", false, new DateTime(2026, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mariam", null, null, null, null, false, "Systems Architect", "f4d927c3e10a4865a9e3427f1020b941", 2, "Ahmed", false, "m.ahmed" },
                    { 10, 0, "72a568297b6e492cb7159c92257d091e", "k.nielsen@email.com", false, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Kasper", null, null, null, null, false, "Business Analyst", "f4d927c3e10a4865a9e3427f1020b941", 2, "Nielsen", false, "k.nielsen" },
                    { 11, 0, "72a568297b6e492cb7159c92257d091e", "f.silva@email.com", false, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Fernanda", null, null, null, null, false, "Cloud Engineer", "f4d927c3e10a4865a9e3427f1020b941", 0, "Silva", false, "f.silva" },
                    { 12, 0, "72a568297b6e492cb7159c92257d091e", "o.peters@email.com", false, new DateTime(2026, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Oliver", null, null, null, null, false, "Security Specialist", "f4d927c3e10a4865a9e3427f1020b941", 2, "Peters", false, "o.peters" },
                    { 13, 0, "72a568297b6e492cb7159c92257d091e", "y.kim@email.com", false, new DateTime(2026, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yuna", null, null, null, null, false, "Mobile Developer", "f4d927c3e10a4865a9e3427f1020b941", 0, "Kim", false, "y.kim" },
                    { 14, 0, "72a568297b6e492cb7159c92257d091e", "r.kumar@email.com", false, new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Rohan", null, null, null, null, false, "Database Administrator", "f4d927c3e10a4865a9e3427f1020b941", 1, "Kumar", false, "r.kumar" },
                    { 15, 0, "72a568297b6e492cb7159c92257d091e", "b.wright@email.com", false, new DateTime(2026, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Beatrice", null, null, null, null, false, "Product Owner", "f4d927c3e10a4865a9e3427f1020b941", 0, "Wright", false, "b.wright" },
                    { 16, 0, "72a568297b6e492cb7159c92257d091e", "t.muller@email.com", false, new DateTime(2026, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Thomas", null, null, null, null, false, "Scrum Master", "f4d927c3e10a4865a9e3427f1020b941", 0, "Müller", false, "t.muller" },
                    { 17, 0, "72a568297b6e492cb7159c92257d091e", "i.popov@email.com", false, new DateTime(2026, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Igor", null, null, null, null, false, "Network Engineer", "f4d927c3e10a4865a9e3427f1020b941", 1, "Popov", false, "i.popov" },
                    { 18, 0, "72a568297b6e492cb7159c92257d091e", "c.santos@email.com", false, new DateTime(2026, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Clara", null, null, null, null, false, "Software Engineer", "f4d927c3e10a4865a9e3427f1020b941", 0, "Santos", false, "c.santos" },
                    { 19, 0, "72a568297b6e492cb7159c92257d091e", "v.nguyen@email.com", false, new DateTime(2026, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Vinh", null, null, null, null, false, "Machine Learning Engineer", "f4d927c3e10a4865a9e3427f1020b941", 2, "Nguyen", false, "v.nguyen" },
                    { 20, 0, "72a568297b6e492cb7159c92257d091e", "g.morris@email.com", false, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Grace", null, null, null, null, false, "Technical Writer", "f4d927c3e10a4865a9e3427f1020b941", 0, "Morris", false, "g.morris" },
                    { 21, 0, "72a568297b6e492cb7159c92257d091e", "d.patel@email.com", false, new DateTime(2026, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Deepak", null, null, null, null, false, "Fullstack Developer", "f4d927c3e10a4865a9e3427f1020b941", 1, "Patel", false, "d.patel" },
                    { 22, 0, "72a568297b6e492cb7159c92257d091e", "z.jones@email.com", false, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Zoe", null, null, null, null, false, "Project Coordinator", "f4d927c3e10a4865a9e3427f1020b941", 2, "Jones", false, "z.jones" },
                    { 23, 0, "72a568297b6e492cb7159c92257d091e", "p.larsen@email.com", false, new DateTime(2026, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Pia", null, null, null, null, false, "Human Resources", "f4d927c3e10a4865a9e3427f1020b941", 0, "Larsen", false, "p.larsen" },
                    { 24, 0, "72a568297b6e492cb7159c92257d091e", "w.clark@email.com", false, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "William", null, null, null, null, false, "IT Manager", "f4d927c3e10a4865a9e3427f1020b941", 1, "Clark", false, "w.clark" },
                    { 25, 0, "72a568297b6e492cb7159c92257d091e", "k.adhikari@email.com", false, new DateTime(2026, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Kiran", null, null, null, null, false, "Embedded Systems Dev", "f4d927c3e10a4865a9e3427f1020b941", 1, "Adhikari", false, "k.adhikari" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
