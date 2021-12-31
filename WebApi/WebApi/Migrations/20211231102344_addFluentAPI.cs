using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class addFluentAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HangHoa_Loai_MaLoai",
                table: "HangHoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HangHoa",
                table: "HangHoa");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "HangHoa");

            migrationBuilder.RenameColumn(
                name: "MaLoai",
                table: "Loai",
                newName: "IDLoai");

            migrationBuilder.RenameColumn(
                name: "MaLoai",
                table: "HangHoa",
                newName: "IDLoai");

            migrationBuilder.RenameIndex(
                name: "IX_HangHoa_MaLoai",
                table: "HangHoa",
                newName: "IX_HangHoa_IDLoai");

            migrationBuilder.AddColumn<int>(
                name: "IDHangHoa",
                table: "HangHoa",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HangHoa",
                table: "HangHoa",
                column: "IDHangHoa");

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    IDDonHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiGiao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongTien = table.Column<double>(type: "float", nullable: false),
                    SoDienThoai = table.Column<double>(type: "float", nullable: false),
                    TinhTrangDonHang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.IDDonHang);
                });

            migrationBuilder.CreateTable(
                name: "DonHangChiTiet",
                columns: table => new
                {
                    IDDonHang = table.Column<int>(type: "int", nullable: false),
                    IDHangHoa = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    GiamGia = table.Column<byte>(type: "tinyint", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangChiTiet", x => new { x.IDHangHoa, x.IDDonHang });
                    table.ForeignKey(
                        name: "FK_DonHangChiTiet_DonHang_IDDonHang",
                        column: x => x.IDDonHang,
                        principalTable: "DonHang",
                        principalColumn: "IDDonHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiet_HangHoa_IDHangHoa",
                        column: x => x.IDHangHoa,
                        principalTable: "HangHoa",
                        principalColumn: "IDHangHoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiet_IDDonHang",
                table: "DonHangChiTiet",
                column: "IDDonHang");

            migrationBuilder.AddForeignKey(
                name: "FK_HangHoa_Loai_IDLoai",
                table: "HangHoa",
                column: "IDLoai",
                principalTable: "Loai",
                principalColumn: "IDLoai",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HangHoa_Loai_IDLoai",
                table: "HangHoa");

            migrationBuilder.DropTable(
                name: "DonHangChiTiet");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HangHoa",
                table: "HangHoa");

            migrationBuilder.DropColumn(
                name: "IDHangHoa",
                table: "HangHoa");

            migrationBuilder.RenameColumn(
                name: "IDLoai",
                table: "Loai",
                newName: "MaLoai");

            migrationBuilder.RenameColumn(
                name: "IDLoai",
                table: "HangHoa",
                newName: "MaLoai");

            migrationBuilder.RenameIndex(
                name: "IX_HangHoa_IDLoai",
                table: "HangHoa",
                newName: "IX_HangHoa_MaLoai");

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "HangHoa",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_HangHoa",
                table: "HangHoa",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_HangHoa_Loai_MaLoai",
                table: "HangHoa",
                column: "MaLoai",
                principalTable: "Loai",
                principalColumn: "MaLoai",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
