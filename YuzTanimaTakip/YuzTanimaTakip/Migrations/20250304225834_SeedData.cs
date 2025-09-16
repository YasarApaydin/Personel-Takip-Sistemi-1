using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YuzTanimaTakip.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MolaTurleris",
                columns: table => new
                {
                    MolaTurId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MolaTurAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sira = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MolaTurleris", x => x.MolaTurId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KullaniciTurId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetRoles_KullaniciTurId",
                        column: x => x.KullaniciTurId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "CalisanKayitlaris",
                columns: table => new
                {
                    KayitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KullaniciId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GirisSaati = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CikisSaati = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MolaSuresi = table.Column<TimeOnly>(type: "time", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanKayitlaris", x => x.KayitId);
                    table.ForeignKey(
                        name: "FK_CalisanKayitlaris_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GecmisKayitlars",
                columns: table => new
                {
                    GecmisKayitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KullaniciId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GirisZamani = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CikisZamani = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MolaSuresi = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GecmisKayitlars", x => x.GecmisKayitId);
                    table.ForeignKey(
                        name: "FK_GecmisKayitlars_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MesaiSaatleris",
                columns: table => new
                {
                    MesaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KullaniciId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gun = table.Column<DateOnly>(type: "date", nullable: false),
                    BaslangicSaati = table.Column<TimeOnly>(type: "time", nullable: false),
                    BitisSaati = table.Column<TimeOnly>(type: "time", nullable: false),
                    ToplamCalismaSuresi = table.Column<TimeOnly>(type: "time", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesaiSaatleris", x => x.MesaiId);
                    table.ForeignKey(
                        name: "FK_MesaiSaatleris_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Raporlars",
                columns: table => new
                {
                    RaporId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KullaniciId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RaporAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToplamCalismaSuresi = table.Column<TimeOnly>(type: "time", nullable: true),
                    ToplamMolaSuresi = table.Column<TimeOnly>(type: "time", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raporlars", x => x.RaporId);
                    table.ForeignKey(
                        name: "FK_Raporlars_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Molalars",
                columns: table => new
                {
                    MolaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KayitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MolaTurId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MolaBaslangic = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MolaBitis = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Molalars", x => x.MolaId);
                    table.ForeignKey(
                        name: "FK_Molalars_CalisanKayitlaris_KayitId",
                        column: x => x.KayitId,
                        principalTable: "CalisanKayitlaris",
                        principalColumn: "KayitId");
                    table.ForeignKey(
                        name: "FK_Molalars_MolaTurleris_MolaTurId",
                        column: x => x.MolaTurId,
                        principalTable: "MolaTurleris",
                        principalColumn: "MolaTurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3d3241b4-c7bb-4111-ad9c-dab1dc407423"), "b4d4e206-aa3d-4c1e-a963-d73b6c56474d", "User", "USER" },
                    { new Guid("6bc5e969-6c00-4055-b396-cc4baab0c63a"), "f2e5659c-18f4-4b60-871f-8bd791607b58", "SuperAdmin", "SUPERADMIN" },
                    { new Guid("e7df287e-c1c0-48b6-9c06-a125049f63ed"), "85fcc968-73d1-43a3-8b6b-c38d1526c6af", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Ad", "AktifMi", "ConcurrencyStamp", "Email", "EmailConfirmed", "Foto", "KullaniciTurId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OlusturulmaTarihi", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Soyad", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2be8c118-5067-4cfe-be99-a811cdf6c8a5"), 0, "Emirhan", true, "fbcb2133-8ff5-4cd0-baa8-f2ce3712d01a", "apaydinyasar5@gmail.com", false, "profil_resmi.jpg", new Guid("e7df287e-c1c0-48b6-9c06-a125049f63ed"), false, null, "APAYDINYASAR5@GMAIL.COM", "APAYDINYASAR5@GMAIL.COM", new DateTime(2025, 3, 4, 22, 58, 34, 508, DateTimeKind.Utc).AddTicks(2551), "AQAAAAIAAYagAAAAEIuw0PHrFnnpUgPh5O6tUqrNNPGJ5ZYX8IH9tXONjCEFMh90sgaEZWyNiTvB8aJNTQ==", "+905415062981", false, "5092ee31-4a8d-41c2-97ca-3fd8449dd40c", "Tanrıverdı", false, "apaydinyasar5@gmail.com" },
                    { new Guid("c76ac66e-11a9-4728-b8d2-1c188ac591e5"), 0, "Ali", true, "a0d34f1a-bcd6-4c91-ba89-5e435fae36c2", "emirhanyasar@gmail.com", false, "profil_resmi.jpg", new Guid("3d3241b4-c7bb-4111-ad9c-dab1dc407423"), false, null, "EMIRHANYASAR@GMAIL.COM", "EMIRHANYASAR@GMAIL.COM", new DateTime(2025, 3, 4, 22, 58, 34, 465, DateTimeKind.Utc).AddTicks(5699), "AQAAAAIAAYagAAAAEHDHQo4F3iu6T2p3p19UeMcOUY9AYKtFK+asBRKzq3x+8ih+xl4W5qz8UZq+Q2F/LQ==", "+905415062981", false, "b94511ef-70a8-4bb9-9bf7-6f140525bef4", "Altunar", false, "emirhanyasar@gmail.com" },
                    { new Guid("d56bb1d9-0b12-4d99-ab77-2a7d1a27a5d6"), 0, "Yaşar", true, "0b256e08-0cd7-44eb-a53f-b018471da004", "apaydinyasar0@gmail.com", false, "profil_resmi.jpg", new Guid("3d3241b4-c7bb-4111-ad9c-dab1dc407423"), false, null, "APAYDINYASAR0@GMAIL.COM", "APAYDINYASAR0@GMAIL.COM", new DateTime(2025, 3, 4, 22, 58, 34, 549, DateTimeKind.Utc).AddTicks(7649), "AQAAAAIAAYagAAAAECMXBzruej3QEJ98Goy8cV61WaBoe4iKCQ4djaBVS4/qgvWu2aQUlatPcqheNPwenQ==", "+905412345678", false, "e990ab6a-8d5d-4d05-9c30-5d21faed5012", "Apaydın", false, "apaydinyasar0@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("e7df287e-c1c0-48b6-9c06-a125049f63ed"), new Guid("2be8c118-5067-4cfe-be99-a811cdf6c8a5") },
                    { new Guid("6bc5e969-6c00-4055-b396-cc4baab0c63a"), new Guid("c76ac66e-11a9-4728-b8d2-1c188ac591e5") },
                    { new Guid("3d3241b4-c7bb-4111-ad9c-dab1dc407423"), new Guid("d56bb1d9-0b12-4d99-ab77-2a7d1a27a5d6") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUsers_KullaniciTurId",
                table: "AspNetUsers",
                column: "KullaniciTurId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanKayitlaris_KullaniciId",
                table: "CalisanKayitlaris",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_GecmisKayitlars_KullaniciId",
                table: "GecmisKayitlars",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_MesaiSaatleris_KullaniciId",
                table: "MesaiSaatleris",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Molalars_KayitId",
                table: "Molalars",
                column: "KayitId");

            migrationBuilder.CreateIndex(
                name: "IX_Molalars_MolaTurId",
                table: "Molalars",
                column: "MolaTurId");

            migrationBuilder.CreateIndex(
                name: "IX_Raporlars_KullaniciId",
                table: "Raporlars",
                column: "KullaniciId");
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
                name: "GecmisKayitlars");

            migrationBuilder.DropTable(
                name: "MesaiSaatleris");

            migrationBuilder.DropTable(
                name: "Molalars");

            migrationBuilder.DropTable(
                name: "Raporlars");

            migrationBuilder.DropTable(
                name: "CalisanKayitlaris");

            migrationBuilder.DropTable(
                name: "MolaTurleris");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");
        }
    }
}
