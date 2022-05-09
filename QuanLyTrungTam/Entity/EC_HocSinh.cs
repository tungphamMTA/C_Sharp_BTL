using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Entity
{
    public class EC_HocSinh
    {
        private string _Ma_HocSinh;
        private string _Ten_HocSinh;
        private DateTime _NgaySinh;
        private bool _GioiTinh;
        private string _DiaChi;
        private string _SDT;
        private string _Email;
        private int _Lop;
        private string _ID;
        private byte[] _Anh;
        public enum HocSinhFields
        {
            Ma_HocSinh,
            Ten_HocSinh,
            NgaySinh,
            GioiTinh,
            DiaChi,
            SDT,
            Email,
            Lop,
            ID,
            Anh
        }
        public EC_HocSinh()
        {
            Ma_HocSinh = "";
            Ten_HocSinh = "";
            NgaySinh = DateTime.Now;
            GioiTinh = true;
            DiaChi = "";
            SDT = "";
            Email = "";
            Lop = 0;
            ID = "";
            Anh = null;
        }
        public EC_HocSinh(string _ma_HocSinh, string _ten_HocSinh, DateTime _ngaySinh,
            bool _gioiTinh, string _diaChi, string _sdt, string _email, int _lop, string _id, byte[] _anh)
        {
            Ma_HocSinh = _ma_HocSinh;
            Ten_HocSinh = _ten_HocSinh;
            NgaySinh = _ngaySinh;
            GioiTinh = _gioiTinh;
            DiaChi = _diaChi;
            SDT = _sdt;
            Email = _email;
            Lop = _lop;
            ID = _id;
            Anh = _anh;
        }
        public string Ma_HocSinh { get => _Ma_HocSinh; set => _Ma_HocSinh = value; }
        public string Ten_HocSinh { get => _Ten_HocSinh; set => _Ten_HocSinh = value; }
        public DateTime NgaySinh { get => _NgaySinh; set => _NgaySinh = value; }
        public bool GioiTinh { get => _GioiTinh; set => _GioiTinh = value; }
        public string DiaChi { get => _DiaChi; set => _DiaChi = value; }
        public string SDT { get => _SDT; set => _SDT = value; }
        public string Email { get => _Email; set => _Email = value; }
        public int Lop { get => _Lop; set => _Lop = value; }
        public string ID { get => _ID; set => _ID = value; }
        public byte[] Anh { get => _Anh; set => _Anh = value; }
    }
}
