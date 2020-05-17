using Microsoft.EntityFrameworkCore.Migrations;

namespace api_dijeta_data.Migrations
{
    public partial class prva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "korisnici",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_korisnici", x => x.KorisnikId);
                });

            migrationBuilder.CreateTable(
                name: "namirnice",
                columns: table => new
                {
                    NamirnicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    JM = table.Column<string>(nullable: true),
                    Kalorije = table.Column<float>(nullable: false),
                    Ugljikohidrati = table.Column<float>(nullable: false),
                    Masti = table.Column<float>(nullable: false),
                    Proteini = table.Column<float>(nullable: false),
                    Kolicina = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_namirnice", x => x.NamirnicaId);
                });

            migrationBuilder.CreateTable(
                name: "dijete",
                columns: table => new
                {
                    DijetaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    BrojDana = table.Column<int>(nullable: false),
                    Kalorije = table.Column<float>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dijete", x => x.DijetaId);
                    table.ForeignKey(
                        name: "FK_dijete_korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dani",
                columns: table => new
                {
                    DanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojKalorija = table.Column<float>(nullable: false),
                    DijetaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dani", x => x.DanId);
                    table.ForeignKey(
                        name: "FK_dani_dijete_DijetaId",
                        column: x => x.DijetaId,
                        principalTable: "dijete",
                        principalColumn: "DijetaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "obroci",
                columns: table => new
                {
                    ObrokId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivObroka = table.Column<string>(nullable: true),
                    DanId = table.Column<int>(nullable: false),
                    BrojKalorija = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_obroci", x => x.ObrokId);
                    table.ForeignKey(
                        name: "FK_obroci_dani_DanId",
                        column: x => x.DanId,
                        principalTable: "dani",
                        principalColumn: "DanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "namirniceObroka",
                columns: table => new
                {
                    NamirniceObrokaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kolicina = table.Column<int>(nullable: false),
                    Kalorije = table.Column<float>(nullable: false),
                    ObrokId = table.Column<int>(nullable: false),
                    NamirnicaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_namirniceObroka", x => x.NamirniceObrokaId);
                    table.ForeignKey(
                        name: "FK_namirniceObroka_namirnice_NamirnicaId",
                        column: x => x.NamirnicaId,
                        principalTable: "namirnice",
                        principalColumn: "NamirnicaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_namirniceObroka_obroci_ObrokId",
                        column: x => x.ObrokId,
                        principalTable: "obroci",
                        principalColumn: "ObrokId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dani_DijetaId",
                table: "dani",
                column: "DijetaId");

            migrationBuilder.CreateIndex(
                name: "IX_dijete_KorisnikId",
                table: "dijete",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_namirniceObroka_NamirnicaId",
                table: "namirniceObroka",
                column: "NamirnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_namirniceObroka_ObrokId",
                table: "namirniceObroka",
                column: "ObrokId");

            migrationBuilder.CreateIndex(
                name: "IX_obroci_DanId",
                table: "obroci",
                column: "DanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "namirniceObroka");

            migrationBuilder.DropTable(
                name: "namirnice");

            migrationBuilder.DropTable(
                name: "obroci");

            migrationBuilder.DropTable(
                name: "dani");

            migrationBuilder.DropTable(
                name: "dijete");

            migrationBuilder.DropTable(
                name: "korisnici");
        }
    }
}
