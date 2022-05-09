using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Entity
{
    public class EC_LopHoc
    {
        private string _Ma_LopHoc;
        private string _Ma_GiaoVien;
        private string _Ma_MonHoc;
        private string _TrinhDo;
        private int _TongHocPhi_KhoaHoc;
        private int _SoBuoi;
        private DateTime _Ngay_BatDau;

        public enum LopHocFields
        {
            Ma_LopHoc,
            Ma_GiaoVien,
            Ma_MonHoc,
            TrinhDo,
            TongHocPhi_KhoaHoc,
            SoBuoi,
            Ngay_BatDau
        }

        public EC_LopHoc()
        {
            Ma_LopHoc = "All";
            Ma_GiaoVien = "All";
            Ma_MonHoc = "All";
            TrinhDo = "All";
            TongHocPhi_KhoaHoc = 0;
            SoBuoi = 0;
            Ngay_BatDau = DateTime.Now;
        }

        public EC_LopHoc(string _ma_LopHoc, string _ma_GiaoVien, string _ma_MonHoc, string _trinhDo, int _tonghp, int _soBuoi, DateTime _ngay_BatDau)
        {
            Ma_LopHoc = _ma_LopHoc;
            Ma_GiaoVien = _ma_GiaoVien;
            Ma_MonHoc = _ma_MonHoc;
            TrinhDo = _trinhDo;
            TongHocPhi_KhoaHoc = _tonghp;
            SoBuoi = _soBuoi;
            Ngay_BatDau = _ngay_BatDau;
        }

        public string Ma_LopHoc { get => _Ma_LopHoc; set => _Ma_LopHoc = value; }
        public string Ma_GiaoVien { get => _Ma_GiaoVien; set => _Ma_GiaoVien = value; }
        public string Ma_MonHoc { get => _Ma_MonHoc; set => _Ma_MonHoc = value; }
        public string TrinhDo { get => _TrinhDo; set => _TrinhDo = value; }
        public int TongHocPhi_KhoaHoc { get => _TongHocPhi_KhoaHoc; set => _TongHocPhi_KhoaHoc = value; }
        public int SoBuoi { get => _SoBuoi; set => _SoBuoi = value; }
        public DateTime Ngay_BatDau { get => _Ngay_BatDau; set => _Ngay_BatDau = value; }
    }
}
