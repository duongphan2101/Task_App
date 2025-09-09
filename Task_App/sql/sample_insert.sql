-- ======================= SAMPLE DATA =======================
-- Đơn vị
INSERT INTO DonVi (maDonVi, tenDonVi) VALUES
('DV01', N'Trung tâm CNTT'),
('DV02', N'Phòng Hành chính');

-- Phòng ban
INSERT INTO PhongBan (maPhongBan, tenPhongBan) VALUES
('PB01', N'Phòng Phát triển phần mềm'),
('PB02', N'Phòng Kế toán');

-- Chức vụ
INSERT INTO ChucVu (maChucVu, tenChucVu) VALUES
('CV01', N'Giám đốc'),
('CV02', N'Lập trình viên'),
('CV03', N'Nhân viên kế toán');

-- NguoiDung
INSERT INTO NguoiDung (hoTen, email, matKhau, maPhongBan, maDonVi, maChucVu, laLanhDao, IsAdmin, TrangThai) VALUES
(N'Admin', 'admin', 'admin', null, null, null, 0, 1, 1),
(N'Hệ Thống', 'c@example.com', '123', null, null, null, 0, 1, 1),
(N'SPPP', 'a@gmail.com', '123', 'PB01', 'DV01', 'CV01', 1, 0, 1),
(N'Trần Văn NxC', 'b@gmail.com', '123', 'PB01', 'DV01', 'CV02', 0, 0, 1),
(N'Lê Văn C', 'c@example.com', '123', 'PB02', 'DV02', 'CV02', 0, 0, 1),
(N'Nguyễn Văn D', 'd@example.com', '123', 'PB01', 'DV01', 'CV01', 0, 0, 1),
(N'Trần Thị E', 'e@example.com', '123', 'PB01', 'DV01', 'CV02', 0, 0, 1);


---- Công việc
--INSERT INTO CongViec (maCongViec, nguoiGiao, lapLai, tanSuat, ngayBatDau, ngayKetThuc) VALUES
--('CV001', 1, 1, N'ngay', '2025-07-20', '2025-07-30'),
--('CV002', 1, 0, NULL, '2025-07-20', '2025-07-25');

---- Chi tiết công việc
--INSERT INTO ChiTietCongViec (maCongViec, tieuDe, noiDung, ngayNhanCongViec, ngayKetThucCongViec, trangThai, tienDo) VALUES
--('CV001', N'Phân tích hệ thống', N'Phân tích yêu cầu người dùng', '2025-07-20', '2025-07-25', 1, 40),
--('CV002', N'Viết tài liệu', N'Viết tài liệu hướng dẫn sử dụng', '2025-07-21', '2025-07-23', 0, 0);

---- Người liên quan công việc
--INSERT INTO NguoiLienQuanCongViec (maCongViec, maNguoiDung, vaiTro) VALUES
--('CV001', 2, 'to'),
--('CV001', 3, 'cc'),
--('CV001', 4, 'bcc'),
--('CV002', 5, 'to');

---- Tệp tin
--INSERT INTO TepTin (tenTep, duongDan, tenTepGoc) VALUES
--(N'phan-tich.docx', N'/files/phan-tich.docx', N'Phân tích.docx'),
--(N'huong-dan.pdf', N'/files/huong-dan.pdf', N'Hướng dẫn.pdf');

---- Email
--INSERT INTO Email (maEmail, nguoiGui, maChiTietCV, tieuDe, noiDung) VALUES
--('E001', 1, 1, N'Báo cáo tiến độ', N'Tiến độ công việc đạt 40%'),
--('E002', 1, 2, N'Nhắc nhở hoàn thành', N'Vui lòng hoàn thành đúng hạn');

---- Người nhận email
--INSERT INTO NguoiNhanEmail (maEmail, maNguoiDung, vaiTro) VALUES
--('E001', 2, 'to'),
--('E001', 3, 'cc'),
--('E002', 4, 'to');

---- Tệp đính kèm email
--INSERT INTO TepDinhKemEmail (maEmail, maTep) VALUES
--('E001', 1),
--('E002', 2);

---- Phản hồi công việc
--INSERT INTO PhanHoiCongViec (maCongViec, maNguoiDung, noiDung, loai, ThoiGian) VALUES
--('DV01PB01_1', 2, N'Công việc đang tiến triển tốt.', N'Feedback', GETDATE()),
--('DV01PB01_1', 1, N'Yêu cầu thêm thông tin.', N'Feedback', GETDATE());

---- Quản lý mã công việc
--INSERT INTO MaCongViecSequence (maDonVi, maPhongBan, stt) VALUES
--('DV01', 'PB01', 2),
--('DV02', 'PB02', 1);