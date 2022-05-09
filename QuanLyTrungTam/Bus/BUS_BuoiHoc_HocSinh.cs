using QuanLyTrungTam.DAL;
using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Bus
{
    public class BUS_BuoiHoc_HocSinh
    {
        DAL_BuoiHoc_HocSinh sql = new DAL_BuoiHoc_HocSinh();
        public void ThemDuLieu(EC_BuoiHoc_HocSinh ec)
        {
            sql.ThemDuLieu(ec);
        }
        public void SuaDuLieu(EC_BuoiHoc_HocSinh ec)
        {
            sql.SuaDuLieu(ec);
        }
        public void XoaDuLieu(EC_BuoiHoc_HocSinh ec)
        {
            sql.XoaDuLieu(ec);
        }

        public List<EC_BuoiHoc_HocSinh> SelectAll()
        {
            return sql.SelectAll();
        }
        public EC_BuoiHoc_HocSinh SelectByMa(string Ma_BuoiHoc, string Ma_HocSinh)
        {
            return sql.Select_ByPrimaryKey(Ma_BuoiHoc, Ma_HocSinh);
        }

        public List<EC_BuoiHoc_HocSinh> SelectByFields(string fieldname, object value)
        {
            return sql.SelectByFields(fieldname, value);
        }
    }

}
