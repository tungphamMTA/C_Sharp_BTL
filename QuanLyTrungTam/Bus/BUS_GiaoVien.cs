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
    public class BUS_GiaoVien
    {
        DAL_GiaoVien sql = new DAL_GiaoVien();
        public void ThemDuLieu(EC_GiaoVien ec)
        {
            sql.ThemDuLieu(ec);
        }
        public void SuaDuLieu(EC_GiaoVien ec)
        {
            sql.SuaDuLieu(ec);
        }
        public void XoaDuLieu(EC_GiaoVien ec)
        {
            sql.XoaDuLieu(ec);
        }
        public List<EC_GiaoVien> SelectAll()
        {
            return sql.SelectAll();
        }
        public EC_GiaoVien Select_ByPrimaryKey(string Ma_GiaoVien)
        {
            return sql.Select_ByPrimaryKey(Ma_GiaoVien);
        }

        public List<EC_GiaoVien> SelectByFields(string fieldname, object value)
        {
            return sql.SelectByFields(fieldname, value);
        }
        public DataTable LayLichHoc_Ngay(string Ma_GiaoVien, DateTime date)
        {
            return sql.LayLichDay_Ngay(Ma_GiaoVien, date);
        }

        public string getMa_GiaoVien(string ID)
        {
            return sql.getMa_GiaoVien(ID);
        }

        public int ThemID(string Ma_GiaoVien, string ID)
        {
            return sql.ThemID(Ma_GiaoVien, ID);
        }
    }
}
