using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mc.core.data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Initials = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    Code = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Code = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    Initials = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CountryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    EmissionDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Complement = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalContact",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ContactType = table.Column<int>(nullable: false),
                    Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    PersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalContact_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Initials = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    Code = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    StateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PublicPlace = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Number = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: true),
                    Complement = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    District = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    CityId = table.Column<Guid>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonId",
                table: "Address",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_PersonId",
                table: "Document",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonId",
                table: "Person",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalContact_PersonId",
                table: "PersonalContact",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "PersonalContact");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
