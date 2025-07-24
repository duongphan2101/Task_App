using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_App.Model;
using Task_App.TaskApp_Dao;

namespace Task_App.Services
{
    internal class CongViecService
    {
        private TcpClientDAO tcpClientDAO;
        public CongViecService(TcpClientDAO tcpClientDAO)
        {
            this.tcpClientDAO = tcpClientDAO;
        }

        public DataTable GetCongViecDaGiao(int nguoiGiaoId, bool sortByTrangThai = false)
        {
            return sortByTrangThai 
                ? tcpClientDAO.GetCongViecDaGiaoByUserId_SortByTrangThai(nguoiGiaoId) 
                : tcpClientDAO.GetCongViecDaGiaoByUserId(nguoiGiaoId);
        }

        public DataTable GetCongViecDuocGiao(int nguoiGiaoId)
        {
            return tcpClientDAO.GetCongViecDuocGiaoByUserId(nguoiGiaoId);
        }

        public DataTable GetCongViecChiTiet(int maChiTietCV)
        {
            return tcpClientDAO.GetCongViecByIdCongViec(maChiTietCV);
        }
        //
        public DataTable GetDanhSachNguoiLienQuanCongViecByIdCongViec(string maCongViec)
        {
            return tcpClientDAO.GetDanhSachNguoiLienQuanByIdCongViec(maCongViec);
        }

        public List<NguoiLienQuanCongViec> GetListNguoiLienQuanCongViecByIdCongViec(string maCongViec)
        {
            return tcpClientDAO.GetListNguoiLienQuanByIdCongViec(maCongViec);
        }

    }
}

