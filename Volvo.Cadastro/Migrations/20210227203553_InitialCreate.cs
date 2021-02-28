using Microsoft.EntityFrameworkCore.Migrations;

namespace Volvo.Cadastro.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbModelo",
                columns: table => new
                {
                    idModelo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("idModelo", x => x.idModelo);
                });

            migrationBuilder.CreateTable(
                name: "tbCaminhao",
                columns: table => new
                {
                    idCaminhao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeloIdModelo = table.Column<int>(type: "int", nullable: false),
                    anoFabricacao = table.Column<int>(type: "int", nullable: false),
                    anoModelo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("idCaminhao", x => x.idCaminhao);
                    table.ForeignKey(
                        name: "FK_tbCaminhao_tbModelo_ModeloIdModelo",
                        column: x => x.ModeloIdModelo,
                        principalTable: "tbModelo",
                        principalColumn: "idModelo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tbModelo",
                columns: new[] { "idModelo", "descricao" },
                values: new object[] { 1, "FH" });

            migrationBuilder.InsertData(
                table: "tbModelo",
                columns: new[] { "idModelo", "descricao" },
                values: new object[] { 2, "FM" });

            migrationBuilder.CreateIndex(
                name: "IX_tbCaminhao_ModeloIdModelo",
                table: "tbCaminhao",
                column: "ModeloIdModelo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbCaminhao");

            migrationBuilder.DropTable(
                name: "tbModelo");
        }
    }
}
