using QuanLyTrungTam.DAL;
using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Bus
{
    public class BUS_HocSinh
    {
        DAL_HocSinh sql = new DAL_HocSinh();
        public void ThemDuLieu(EC_HocSinh ec)
        {
            sql.ThemDuLieu(ec);
        }
        public void SuaDuLieu(EC_HocSinh ec)
        {
            sql.SuaDuLieu(ec);
        }
        public void XoaDuLieu(EC_HocSinh ec)
        {
            sql.XoaDuLieu(ec);
        }
        public List<EC_HocSinh> SelectAll()
        {
            return sql.SelectAll();
        }
        public EC_HocSinh Select_ByPrimaryKey(string Ma_HocSinh)
        {
            return sql.Select_ByPrimaryKey(Ma_HocSinh);
        }

        public List<EC_HocSinh> SelectByFields(string fieldname, object value)
        {
            return sql.SelectByFields(fieldname, value);
        }

        public int ThemID(string Ma_HocSinh, string ID)
        {
            return sql.ThemID(Ma_HocSinh, ID);
        }
    }
}
