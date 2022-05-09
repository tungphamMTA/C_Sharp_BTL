using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Entity
{
    public class EC_TaiKhoan
    {
        private string _ID;
        private string _TenDangNhap;
        private string _MatKhau;

        public enum TaiKhoanFields
        {
            ID,
            TenDangNhap,
            MatKhau
        }
        public EC_TaiKhoan()
        {
            ID = "";
            TenDangNhap = "";
            MatKhau = "";
        }
        public EC_TaiKhoan(string id, string tendnn, string matkhau)
        {
            ID = id;
            TenDangNhap = tendnn;
            MatKhau = matkhau;
        }

        public string ID { get => _ID; set => _ID = value; }
        public string TenDangNhap { get => _TenDangNhap; set => _TenDangNhap = value; }
        public string MatKhau { get => _MatKhau; set => _MatKhau = value; }
    }
}
