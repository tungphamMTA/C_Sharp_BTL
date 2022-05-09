using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Entity
{
    public class EC_BuoiHoc_HocSinh
    {
        private string _Ma_HocSinh;
        private string _Ma_BuoiHoc;
        private int _SoTien_Buoi;
        private bool _DiemDanh;
        private string _DanhGia;
        private bool _DongTien;

        public enum BuoiHoc_HocSinh_Fields
        {
            Ma_HocSinh,
            Ma_BuoiHoc,
            SoTien_Buoi,
            DiemDanh,
            DanhGia,
            DongTien
        }

        public EC_BuoiHoc_HocSinh()
        {

        }
        public EC_BuoiHoc_HocSinh(string _ma_HocSinh, string _ma_BuoiHoc, int _soTien_Buoi, bool _diemDanh, string _danhGia, bool _dongTien)
        {
            Ma_HocSinh = _ma_HocSinh;
            Ma_BuoiHoc = _ma_BuoiHoc;
            SoTien_Buoi = _soTien_Buoi;
            DiemDanh = _diemDanh;
            DanhGia = _danhGia;
            DongTien = _dongTien;
        }

        public string Ma_HocSinh { get => _Ma_HocSinh; set => _Ma_HocSinh = value; }
        public string Ma_BuoiHoc { get => _Ma_BuoiHoc; set => _Ma_BuoiHoc = value; }
        public int SoTien_Buoi { get => _SoTien_Buoi; set => _SoTien_Buoi = value; }
        public bool DiemDanh { get => _DiemDanh; set => _DiemDanh = value; }
        public string DanhGia { get => _DanhGia; set => _DanhGia = value; }
        public bool DongTien { get => _DongTien; set => _DongTien = value; }
    }
}
