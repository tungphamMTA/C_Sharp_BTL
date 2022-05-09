using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Entity
{
    public class EC_PhanCong_Day
    {
        private string _Ma_GiaoVien;
        private string _Ma_MonHoc;

        public enum PhanCong_DayFields
        {
            Ma_GiaoVien,
            Ma_MonHoc
        }
        public EC_PhanCong_Day()
        {

        }
        public EC_PhanCong_Day(string _ma_GiaoVien, string _ma_MonHoc)
        {
            Ma_GiaoVien = _ma_GiaoVien;
            Ma_MonHoc = _ma_GiaoVien;
        }
        public string Ma_GiaoVien { get => _Ma_GiaoVien; set => _Ma_GiaoVien = value; }
        public string Ma_MonHoc { get => _Ma_MonHoc; set => _Ma_MonHoc = value; }
    }
}
