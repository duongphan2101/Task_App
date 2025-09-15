using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIServerApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    MaChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenChucVu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVu", x => x.MaChucVu);
                });

            migrationBuilder.CreateTable(
                name: "DonVi",
                columns: table => new
                {
                    MaDonVi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenDonVi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonVi", x => x.MaDonVi);
                });

            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    MaPhongBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenPhongBan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan", x => x.MaPhongBan);
                });

            migrationBuilder.CreateTable(
                name: "TepTin",
                columns: table => new
                {
                    MaTep = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTep = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TenTepGoc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DuongDan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TepTin", x => x.MaTep);
                });

            migrationBuilder.CreateTable(
                name: "MaCongViecSequence",
                columns: table => new
                {
                    MaDonVi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaPhongBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    STT = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaCongViecSequence", x => new { x.MaDonVi, x.MaPhongBan });
                    table.ForeignKey(
                        name: "FK_MaCongViecSequence_DonVi_MaDonVi",
                        column: x => x.MaDonVi,
                        principalTable: "DonVi",
                        principalColumn: "MaDonVi");
                    table.ForeignKey(
                        name: "FK_MaCongViecSequence_PhongBan_MaPhongBan",
                        column: x => x.MaPhongBan,
                        principalTable: "PhongBan",
                        principalColumn: "MaPhongBan");
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaPhongBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaDonVi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LaLanhDao = table.Column<bool>(type: "bit", nullable: true),
                    IsAdmin = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.MaNguoiDung);
                    table.ForeignKey(
                        name: "FK_NguoiDung_ChucVu_MaChucVu",
                        column: x => x.MaChucVu,
                        principalTable: "ChucVu",
                        principalColumn: "MaChucVu");
                    table.ForeignKey(
                        name: "FK_NguoiDung_DonVi_MaDonVi",
                        column: x => x.MaDonVi,
                        principalTable: "DonVi",
                        principalColumn: "MaDonVi");
                    table.ForeignKey(
                        name: "FK_NguoiDung_PhongBan_MaPhongBan",
                        column: x => x.MaPhongBan,
                        principalTable: "PhongBan",
                        principalColumn: "MaPhongBan");
                });

            migrationBuilder.CreateTable(
                name: "CongViec",
                columns: table => new
                {
                    MaCongViec = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiGiao = table.Column<int>(type: "int", nullable: true),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LapLai = table.Column<bool>(type: "bit", nullable: true),
                    TanSuat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongViec", x => x.MaCongViec);
                    table.ForeignKey(
                        name: "FK_CongViec_NguoiDung_NguoiGiao",
                        column: x => x.NguoiGiao,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietCongViec",
                columns: table => new
                {
                    MaChiTietCV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCongViec = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayNhanCongViec = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayKetThucCongViec = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHoanThanh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoNgayHoanThanh = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    TienDo = table.Column<int>(type: "int", nullable: false),
                    MucDoUuTien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietCongViec", x => x.MaChiTietCV);
                    table.ForeignKey(
                        name: "FK_ChiTietCongViec_CongViec_MaCongViec",
                        column: x => x.MaCongViec,
                        principalTable: "CongViec",
                        principalColumn: "MaCongViec");
                });

            migrationBuilder.CreateTable(
                name: "NguoiLienQuanCongViec",
                columns: table => new
                {
                    MaCongViec = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiLienQuanCongViec", x => new { x.MaCongViec, x.MaNguoiDung });
                    table.ForeignKey(
                        name: "FK_NguoiLienQuanCongViec_CongViec_MaCongViec",
                        column: x => x.MaCongViec,
                        principalTable: "CongViec",
                        principalColumn: "MaCongViec");
                    table.ForeignKey(
                        name: "FK_NguoiLienQuanCongViec_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung");
                });

            migrationBuilder.CreateTable(
                name: "PhanHoiCongViec",
                columns: table => new
                {
                    MaPhanHoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCongViec = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Loai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanHoiCongViec", x => x.MaPhanHoi);
                    table.ForeignKey(
                        name: "FK_PhanHoiCongViec_CongViec_MaCongViec",
                        column: x => x.MaCongViec,
                        principalTable: "CongViec",
                        principalColumn: "MaCongViec");
                    table.ForeignKey(
                        name: "FK_PhanHoiCongViec_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung");
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    MaEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NguoiGui = table.Column<int>(type: "int", nullable: false),
                    MaChiTietCV = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayGui = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    MessageId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InReplyTo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    References = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiTietCongViecMaChiTietCV = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.MaEmail);
                    table.ForeignKey(
                        name: "FK_Email_ChiTietCongViec_ChiTietCongViecMaChiTietCV",
                        column: x => x.ChiTietCongViecMaChiTietCV,
                        principalTable: "ChiTietCongViec",
                        principalColumn: "MaChiTietCV");
                    table.ForeignKey(
                        name: "FK_Email_ChiTietCongViec_MaChiTietCV",
                        column: x => x.MaChiTietCV,
                        principalTable: "ChiTietCongViec",
                        principalColumn: "MaChiTietCV");
                    table.ForeignKey(
                        name: "FK_Email_NguoiDung_NguoiGui",
                        column: x => x.NguoiGui,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung");
                });

            migrationBuilder.CreateTable(
                name: "ThongBaoNguoiDung",
                columns: table => new
                {
                    MaThongBao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaChiTietCV = table.Column<int>(type: "int", nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    NgayThongBao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoNguoiDung", x => x.MaThongBao);
                    table.ForeignKey(
                        name: "FK_ThongBaoNguoiDung_ChiTietCongViec_MaChiTietCV",
                        column: x => x.MaChiTietCV,
                        principalTable: "ChiTietCongViec",
                        principalColumn: "MaChiTietCV");
                    table.ForeignKey(
                        name: "FK_ThongBaoNguoiDung_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung");
                });

            migrationBuilder.CreateTable(
                name: "NguoiNhanEmail",
                columns: table => new
                {
                    MaEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiNhanEmail", x => new { x.MaEmail, x.MaNguoiDung });
                    table.ForeignKey(
                        name: "FK_NguoiNhanEmail_Email_MaEmail",
                        column: x => x.MaEmail,
                        principalTable: "Email",
                        principalColumn: "MaEmail");
                    table.ForeignKey(
                        name: "FK_NguoiNhanEmail_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung");
                });

            migrationBuilder.CreateTable(
                name: "TepDinhKemEmail",
                columns: table => new
                {
                    MaEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaTep = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TepDinhKemEmail", x => new { x.MaEmail, x.MaTep });
                    table.ForeignKey(
                        name: "FK_TepDinhKemEmail_Email_MaEmail",
                        column: x => x.MaEmail,
                        principalTable: "Email",
                        principalColumn: "MaEmail");
                    table.ForeignKey(
                        name: "FK_TepDinhKemEmail_TepTin_MaTep",
                        column: x => x.MaTep,
                        principalTable: "TepTin",
                        principalColumn: "MaTep");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietCongViec_MaCongViec",
                table: "ChiTietCongViec",
                column: "MaCongViec");

            migrationBuilder.CreateIndex(
                name: "IX_CongViec_NguoiGiao",
                table: "CongViec",
                column: "NguoiGiao");

            migrationBuilder.CreateIndex(
                name: "IX_Email_ChiTietCongViecMaChiTietCV",
                table: "Email",
                column: "ChiTietCongViecMaChiTietCV");

            migrationBuilder.CreateIndex(
                name: "IX_Email_MaChiTietCV",
                table: "Email",
                column: "MaChiTietCV");

            migrationBuilder.CreateIndex(
                name: "IX_Email_NguoiGui",
                table: "Email",
                column: "NguoiGui");

            migrationBuilder.CreateIndex(
                name: "IX_MaCongViecSequence_MaPhongBan",
                table: "MaCongViecSequence",
                column: "MaPhongBan");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MaChucVu",
                table: "NguoiDung",
                column: "MaChucVu");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MaDonVi",
                table: "NguoiDung",
                column: "MaDonVi");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MaPhongBan",
                table: "NguoiDung",
                column: "MaPhongBan");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiLienQuanCongViec_MaNguoiDung",
                table: "NguoiLienQuanCongViec",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiNhanEmail_MaNguoiDung",
                table: "NguoiNhanEmail",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoiCongViec_MaCongViec",
                table: "PhanHoiCongViec",
                column: "MaCongViec");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoiCongViec_MaNguoiDung",
                table: "PhanHoiCongViec",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_TepDinhKemEmail_MaTep",
                table: "TepDinhKemEmail",
                column: "MaTep");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoNguoiDung_MaChiTietCV",
                table: "ThongBaoNguoiDung",
                column: "MaChiTietCV");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoNguoiDung_MaNguoiDung",
                table: "ThongBaoNguoiDung",
                column: "MaNguoiDung");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaCongViecSequence");

            migrationBuilder.DropTable(
                name: "NguoiLienQuanCongViec");

            migrationBuilder.DropTable(
                name: "NguoiNhanEmail");

            migrationBuilder.DropTable(
                name: "PhanHoiCongViec");

            migrationBuilder.DropTable(
                name: "TepDinhKemEmail");

            migrationBuilder.DropTable(
                name: "ThongBaoNguoiDung");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "TepTin");

            migrationBuilder.DropTable(
                name: "ChiTietCongViec");

            migrationBuilder.DropTable(
                name: "CongViec");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "ChucVu");

            migrationBuilder.DropTable(
                name: "DonVi");

            migrationBuilder.DropTable(
                name: "PhongBan");
        }
    }
}
