using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRoleGroups",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoleGroups", x => new { x.RoleId, x.GroupId });
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserGroups",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGroups", x => new { x.UserId, x.GroupId });
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RMenuType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RControllerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RMenuGroupId = table.Column<int>(type: "int", nullable: true),
                    RMenuGroupOrder = table.Column<int>(type: "int", nullable: true),
                    RMenuIndex = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "COMPANYMASTER",
                columns: table => new
                {
                    CMPYID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMPYNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CMPYADDR1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYADDR2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYADDR3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYCITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYSTATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYPINCODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYCONTACTPERSON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYPHONE1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYPHONE2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYEMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYGSTNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMPYPANNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DISPSTATUS = table.Column<short>(type: "smallint", nullable: false),
                    CUSRID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRCSDATE = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANYMASTER", x => x.CMPYID);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERLOCATIONDETAIL",
                columns: table => new
                {
                    CATEAID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEID = table.Column<int>(type: "int", nullable: false),
                    CATEAADDR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LOCTID = table.Column<int>(type: "int", nullable: false),
                    STATEID = table.Column<int>(type: "int", nullable: false),
                    CATEA_GST_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEA_PINCODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEAADDR1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEAADDR2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEA_CITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEA_COUNTRY = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERLOCATIONDETAIL", x => x.CATEAID);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERMASTER",
                columns: table => new
                {
                    CATEID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATETID = table.Column<int>(type: "int", nullable: false),
                    CATENO = table.Column<int>(type: "int", nullable: false),
                    CATECODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATENAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEDNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATE_GST_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATE_PAN_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATE_TAN_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CURNID = table.Column<int>(type: "int", nullable: true),
                    CATEBANKNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEBANKBRNCHNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEBANKADDR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEBANK_ACTYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEBANK_ACNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEBANK_IFCS_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEBANK_IBAN_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CATEBANK_SWIFT_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CUSRID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LMUSRID = table.Column<int>(type: "int", nullable: true),
                    DISPSTATUS = table.Column<short>(type: "smallint", nullable: true),
                    PRCSDATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CATE_TALLY_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ACHEADID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERMASTER", x => x.CATEID);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LOCATIONMASTER",
                columns: table => new
                {
                    LOCTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOCTNAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    STATEID = table.Column<int>(type: "int", nullable: false),
                    DISPSTATUS = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOCATIONMASTER", x => x.LOCTID);
                });

            migrationBuilder.CreateTable(
                name: "STATEMASTER",
                columns: table => new
                {
                    STATEID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STATENAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DISPSTATUS = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATEMASTER", x => x.STATEID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRoleGroups");

            migrationBuilder.DropTable(
                name: "ApplicationUserGroups");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "COMPANYMASTER");

            migrationBuilder.DropTable(
                name: "CUSTOMERLOCATIONDETAIL");

            migrationBuilder.DropTable(
                name: "CUSTOMERMASTER");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "LOCATIONMASTER");

            migrationBuilder.DropTable(
                name: "STATEMASTER");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
