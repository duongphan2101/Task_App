GO
CREATE DATABASE TASK_APP_DB
GO

USE TASK_APP_DB

-- Đơn vị
CREATE TABLE DonVi (
    maDonVi VARCHAR(10) PRIMARY KEY,
    tenDonVi NVARCHAR(100) NOT NULL
);

-- Phòng ban
CREATE TABLE PhongBan (
    maPhongBan VARCHAR(10) PRIMARY KEY,
    tenPhongBan NVARCHAR(100) NOT NULL
);

-- Chức vụ
CREATE TABLE ChucVu (
    maChucVu VARCHAR(10) PRIMARY KEY,
    tenChucVu NVARCHAR(100) NOT NULL
);

-- Người dùng
CREATE TABLE NguoiDung (
    maNguoiDung INT PRIMARY KEY IDENTITY(1,1),
    hoTen NVARCHAR(100) NOT NULL,
    email NVARCHAR(150) NOT NULL,
    matKhau NVARCHAR(100) NOT NULL,
    maPhongBan VARCHAR(10),
    maDonVi VARCHAR(10),
    maChucVu VARCHAR(10),
    laLanhDao BIT DEFAULT 0,
    FOREIGN KEY (maPhongBan) REFERENCES PhongBan(maPhongBan),
    FOREIGN KEY (maDonVi) REFERENCES DonVi(maDonVi),
    FOREIGN KEY (maChucVu) REFERENCES ChucVu(maChucVu)
);

-- Công việc
CREATE TABLE CongViec (
    maCongViec VARCHAR(20) PRIMARY KEY,
    nguoiGiao INT,
    ngayGiao DATETIME DEFAULT GETDATE(),
    lapLai BIT DEFAULT 0,
    tanSuat NVARCHAR(20),
    ngayBatDau DATETIME,
    ngayKetThuc DATETIME,
    FOREIGN KEY (nguoiGiao) REFERENCES NguoiDung(maNguoiDung)
);

-- Chi tiết công việc
CREATE TABLE ChiTietCongViec (
    maChiTietCV INT PRIMARY KEY IDENTITY(1,1),
    maCongViec VARCHAR(20),
    tieuDe NVARCHAR(200) NOT NULL,
    noiDung NVARCHAR(MAX),
    ngayNhanCongViec DATETIME,
    ngayKetThucCongViec DATETIME,
    ngayHoanThanh DATETIME,
    soNgayHoanThanh INT,
    trangThai INT DEFAULT 0, --0 : chua xu li, 1: dang xu li, 2: hoan thanh, 3: tre, 4: huy
    tienDo INT DEFAULT 0,
    mucDoUuTien INT DEFAULT 0, --0: binh thuong, 1: quan trong, 2: khancap
    FOREIGN KEY (maCongViec) REFERENCES CongViec(maCongViec)
);

-- Người liên quan công việc
CREATE TABLE NguoiLienQuanCongViec (
    maCongViec VARCHAR(20),
    maNguoiDung INT,
    vaiTro NVARCHAR(20) CHECK (vaiTro IN ('to', 'cc', 'bcc')),
    PRIMARY KEY (maCongViec, maNguoiDung, vaiTro),
    FOREIGN KEY (maCongViec) REFERENCES CongViec(maCongViec),
    FOREIGN KEY (maNguoiDung) REFERENCES NguoiDung(maNguoiDung)
);

-- Tệp tin
CREATE TABLE TepTin (
    maTep INT PRIMARY KEY IDENTITY(1,1),
    tenTep NVARCHAR(255) NOT NULL,
    duongDan NVARCHAR(500) NOT NULL,
    tenTepGoc NVARCHAR(255)
);

-- Email
CREATE TABLE Email (
    maEmail VARCHAR(100) PRIMARY KEY,
    nguoiGui INT,
    maChiTietCV INT,
    tieuDe NVARCHAR(255),
    noiDung NVARCHAR(MAX),
    ngayGui DATETIME DEFAULT GETDATE(),
    trangThai INT DEFAULT 0, --0: chua gui, 1: da gui
    FOREIGN KEY (nguoiGui) REFERENCES NguoiDung(maNguoiDung),
    FOREIGN KEY (maChiTietCV) REFERENCES ChiTietCongViec(maChiTietCV)
);

-- Người nhận Email
CREATE TABLE NguoiNhanEmail (
    maEmail VARCHAR(100),
    maNguoiDung INT,
    vaiTro NVARCHAR(20) CHECK (vaiTro IN ('to', 'cc', 'bcc')),
    PRIMARY KEY (maEmail, maNguoiDung, vaiTro),
    FOREIGN KEY (maEmail) REFERENCES Email(maEmail),
    FOREIGN KEY (maNguoiDung) REFERENCES NguoiDung(maNguoiDung)
);

-- Tệp đính kèm Email
CREATE TABLE TepDinhKemEmail (
    maEmail VARCHAR(100),
    maTep INT,
    PRIMARY KEY (maEmail, maTep),
    FOREIGN KEY (maEmail) REFERENCES Email(maEmail),
    FOREIGN KEY (maTep) REFERENCES TepTin(maTep)
);

-- Phản hồi công việc
CREATE TABLE PhanHoiCongViec (
    maPhanHoi INT PRIMARY KEY IDENTITY(1,1),
    maCongViec VARCHAR(20) NOT NULL,
    maNguoiDung INT NOT NULL,
    noiDung NVARCHAR(MAX) NOT NULL,
    thoiGian DATETIME DEFAULT GETDATE(),
    loai NVARCHAR(20) DEFAULT 'Feedback',
    FOREIGN KEY (maCongViec) REFERENCES CongViec(maCongViec),
    FOREIGN KEY (maNguoiDung) REFERENCES NguoiDung(maNguoiDung)
);

-- Quản lý mã công việc
CREATE TABLE MaCongViecSequence (
    maDonVi VARCHAR(10),
    maPhongBan VARCHAR(10),
    stt INT DEFAULT 0,
    PRIMARY KEY (maDonVi, maPhongBan),
    FOREIGN KEY (maDonVi) REFERENCES DonVi(maDonVi),
    FOREIGN KEY (maPhongBan) REFERENCES PhongBan(maPhongBan)
);

CREATE TABLE ThongBaoNguoiDung (
    maThongBao INT PRIMARY KEY IDENTITY(1,1),
    maNguoiDung INT NOT NULL,
    maChiTietCV INT NOT NULL,
    noiDung NVARCHAR(MAX) NOT NULL,
    ngayThongBao DATETIME DEFAULT GETDATE(),
    trangThai INT DEFAULT 0, -- 0: chưa đọc, 1: đã đọc

    FOREIGN KEY (maNguoiDung) REFERENCES NguoiDung(maNguoiDung),
    FOREIGN KEY (maChiTietCV) REFERENCES ChiTietCongViec(maChiTietCV)
);

