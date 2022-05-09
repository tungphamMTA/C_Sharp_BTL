using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Entity
{
    public class EC_MonHoc
    {
        private string _Ma_MonHoc;
        private string _Ten_MonHoc;
        private int _Lop;

        public EC_MonHoc()
        {
            Ma_MonHoc = "All";
            Ten_MonHoc = "All";
            Lop = 0;
        }

        public enum MonHocField
        {
            Ma_MonHoc,
            Ten_MonHoc,
            Lop
        }

        public EC_MonHoc(string _maMonHoc, string _tenMonHoc, int _lop)
        {
            Ma_MonHoc = _maMonHoc;
            Ten_MonHoc = _tenMonHoc;
            Lop = _lop;
        }

        public string Ma_MonHoc { get => _Ma_MonHoc; set => _Ma_MonHoc = value; }
        public string Ten_MonHoc { get => _Ten_MonHoc; set => _Ten_MonHoc = value; }
        public int Lop { get => _Lop; set => _Lop = value; }
    }
}
