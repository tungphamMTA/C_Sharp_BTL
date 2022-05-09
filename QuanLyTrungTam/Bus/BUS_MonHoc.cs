using QuanLyTrungTam.DAL;
using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Bus
{
    public class BUS_MonHoc
    {
        DAL_MonHoc sql = new DAL_MonHoc();
        public void ThemDuLieu(EC_MonHoc ec)
        {
            sql.ThemDuLieu(ec);
        }
        public void SuaDuLieu(EC_MonHoc ec)
        {
            sql.SuaDuLieu(ec);
        }
        public void XoaDuLieu(string Ma_MonHoc)
        {
            sql.XoaDuLieu(Ma_MonHoc);
        }
        public List<EC_MonHoc> SelectAll()
        {
            return sql.SelectAll();
        }
        public EC_MonHoc Select_ByPrimaryKey(string Ma_MonHoc)
        {
            return sql.Select_ByPrimaryKey(Ma_MonHoc);
        }

        public List<EC_MonHoc> SelectByFields(string fieldname, object value)
        {
            return sql.SelectByFields(fieldname, value);
        }
    }
}
