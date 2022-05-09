using QuanLyTrungTam.DAL;
using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Bus
{
    public class function
    {
        DAL_function ft = new DAL_function();
        public DataTable LayLichHoc_Ngay(string Ma_HocSinh, DateTime date)
        {
            DataTable tb = ft.LayLichhoc_Ngay(Ma_HocSinh, date);
            return tb;
        }
        public DataTable LopHoc_DangHoc(string Ma_HocSinh)
        {
            return ft.LayLopHoc_DangHoc(Ma_HocSinh);
        }
        public DataTable LopHoc_DaHoc(string Ma_HocSinh)
        {
            return ft.LayLopHoc_DangHoc(Ma_HocSinh);
        }

        public DataTable TongTien_Thang(string Ma_HocSinh)
        {
            return ft.TongTien_Thang(Ma_HocSinh);
        }

        public DataTable LopHoc_Select_Manager(EC_LopHoc ecLop, EC_MonHoc ecMon)
        {
            return ft.LopHoc_Select_Manager(ecLop, ecMon);
        }

        public DataTable DSHocSinh_Lop(string Ma_LopHoc)
        {
            return ft.DSHocSinh_Lop(Ma_LopHoc);
        }

        public void Them_HocSinh_Lop(string MA_Lophoc, string Ma_HocSinh)
        {
            ft.Them_HocSinh_Lop(MA_Lophoc, Ma_HocSinh);
        }

        public DataTable Select_HocPhi_Thang(DateTime date)
        {
            return ft.Select_HocPhi_Thang(date);
        }

        public void DongTien_Thang(string Ma_HocSinh, DateTime ThoiGian)
        {
            ft.DongTien_Thang(Ma_HocSinh, ThoiGian);
        }

        public DataTable SoBuoiHoc_Thang(string Ma_HocSinh, int Nam)
        {
            return ft.SoBuoiHoc_Thang(Ma_HocSinh, Nam);
        }

        public DataTable BuoiHoc_Thang(string Ma_HocSinh, DateTime date)
        {
            return ft.BuoiHoc_Thang(Ma_HocSinh, date);
        }
    }
}
