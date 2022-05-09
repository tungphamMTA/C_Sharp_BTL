using QuanLyTrungTam.DAL;
using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Bus
{
    public class BUS_LichHoc
    {
        DAL_LichHoc sql = new DAL_LichHoc();
        public void ThemDuLieu(EC_LichHoc ec)
        {
            sql.ThemDuLieu(ec);
        }
        public void SuaDuLieu(EC_LichHoc ec)
        {
            sql.SuaDuLieu(ec);
        }
        public void XoaDuLieu_Ma_BuoiHoc(string Ma_BuoiHoc)
        {
            sql.LichHoc_Delete_By_Ma_BuoiHoc(Ma_BuoiHoc);
        }
        public void XoaDuLieu_Ma_LopHoc(string Ma_LopHoc)
        {
            sql.LichHoc_Delete_By_Ma_LopHoc(Ma_LopHoc);
        }
        public List<EC_LichHoc> SelectAll()
        {
            return sql.SelectAll();
        }
        public EC_LichHoc Select_ByPrimaryKey(string Ma_BuoiHoc)
        {
            return sql.Select_ByPrimaryKey(Ma_BuoiHoc);
        }

        public List<EC_LichHoc> SelectByFields(string fieldname, object value)
        {
            return sql.SelectByFields(fieldname, value);
        }

        public void SuaTrangThai(string Ma_LopHoc, bool tt)
        {
            sql.SuaTrangThai(Ma_LopHoc, tt);
        }
    }
}
