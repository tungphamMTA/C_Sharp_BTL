using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Entity
{
    public class EC_GiaoVien
    {
        private string _Ma_GiaoVien;
        private string _Ten_GiaoVien;
        private DateTime _NgaySinh;
        private bool _GioiTinh;
        private string _DiaChi;
        private string _SDT;
        private string _Email;
        private string _TrinhDo;
        private string _ID;
        private byte[] _Anh;

        public enum GiaoVienFields
        {
            Ma_GiaoVien,
            Ten_GiaoVien,
            NgaySinh,
            GioiTinh,
            DiaChi,
            SDT,
            Email,
            TrinhDo,
            ID,
            Anh
        }

        public EC_GiaoVien()
        {
            Ma_GiaoVien = "";
            Ten_GiaoVien = "";
            NgaySinh = DateTime.Now;
            GioiTinh = true;
            DiaChi = "";
            SDT = "";
            Email = "";
            TrinhDo = "";
            ID = "";
            Anh = null;
        }
        public EC_GiaoVien(string _ma_GiaoVien, string _ten_GiaoVien, DateTime _ngaySinh,
            bool _gioiTinh, string _diaChi, string _sdt, string _email, string _trinhDo, string _id, byte[] _anh)
        {
            Ma_GiaoVien = _ma_GiaoVien;
            Ten_GiaoVien = _ten_GiaoVien;
            NgaySinh = _ngaySinh;
            GioiTinh = _gioiTinh;
            DiaChi = _diaChi;
            SDT = _sdt;
            Email = _email;
            TrinhDo = _trinhDo;
            ID = _id;
            Anh = _anh;
        }

        public string Ma_GiaoVien { get => _Ma_GiaoVien; set => _Ma_GiaoVien = value; }
        public string Ten_GiaoVien { get => _Ten_GiaoVien; set => _Ten_GiaoVien = value; }
        public DateTime NgaySinh { get => _NgaySinh; set => _NgaySinh = value; }
        public bool GioiTinh { get => _GioiTinh; set => _GioiTinh = value; }
        public string DiaChi { get => _DiaChi; set => _DiaChi = value; }
        public string SDT { get => _SDT; set => _SDT = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string TrinhDo { get => _TrinhDo; set => _TrinhDo = value; }
        public string ID { get => _ID; set => _ID = value; }
        public byte[] Anh { get => _Anh; set => _Anh = value; }
    }
}
