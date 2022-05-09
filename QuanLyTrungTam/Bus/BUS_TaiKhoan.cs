using QuanLyTrungTam.DAL;
using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Bus
{
    public class BUS_TaiKhoan
    {
        DAL_TaiKhoan sql = new DAL_TaiKhoan();
        public void ThemDuLieu(EC_TaiKhoan ec)
        {
            sql.ThemDuLieu(ec);
        }
        public void SuaDuLieu(EC_TaiKhoan ec)
        {
            sql.SuaDuLieu(ec);
        }
        public void XoaDuLieu(string ID)
        {
            sql.XoaDuLieu(ID);
        }
        public List<EC_TaiKhoan> SelectAll()
        {
            return sql.SelectAll();
        }
        public EC_TaiKhoan SelectByMa(string ID)
        {
            return sql.Select_ByPrimaryKey(ID);
        }

        public List<EC_TaiKhoan> SelectByFields(string TenDangNhap, string MatKhau)
        {
            return sql.SelectByFields(TenDangNhap, MatKhau);
        }
    }
}
