using QuanLyTrungTam.DAL;
using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Bus
{
    public class BUS_LopHoc
    {
        DAL_LopHoc sql = new DAL_LopHoc();
        public void ThemDuLieu(EC_LopHoc ec)
        {
            sql.ThemDuLieu(ec);
        }
        public void SuaDuLieu(EC_LopHoc ec)
        {
            sql.SuaDuLieu(ec);
        }
        public void XoaDuLieu(string Ma_LopHoc)
        {
            sql.XoaDuLieu(Ma_LopHoc);
        }
        public List<EC_LopHoc> SelectAll()
        {
            return sql.SelectAll();
        }
        public EC_LopHoc Select_ByPrimaryKey(string Ma_LopHoc)
        {
            return sql.SelectByPrimaryKey(Ma_LopHoc);
        }

        public List<EC_LopHoc> SelectByFields(string fieldname, object value)
        {
            return sql.SelectByFields(fieldname, value);
        }
    }
}
