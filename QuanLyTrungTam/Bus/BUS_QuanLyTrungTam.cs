using QuanLyTrungTam.DAL;
using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Bus
{
    public class BUS_QuanLyTrungTam
    {
        DAL_QuanLyTrungTam sql = new DAL_QuanLyTrungTam();
        public void ThemDuLieu(EC_QuanLyTrungTam ec)
        {
            sql.ThemDuLieu(ec);
        }
        public void SuaDuLieu(EC_QuanLyTrungTam ec)
        {
            sql.SuaDuLieu(ec);
        }
        public void XoaDuLieu(EC_QuanLyTrungTam ec)
        {
            sql.XoaDuLieu(ec);
        }

        public List<EC_QuanLyTrungTam> Select_BYPrimaryKey(string ID)
        {
            return sql.Select_ByPrimaryKey(ID);
        }
        public List<EC_QuanLyTrungTam> Select_All()
        {
            return sql.Select_All();
        }
    }
}
