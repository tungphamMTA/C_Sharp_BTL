using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.Entity
{
    public class EC_LichHoc
    {
        private string _Ma_LopHoc;
        private DateTime _NgayHoc;
        private int _KipHoc;
        private string _Ma_BuoiHoc;
        private string _PhongHoc;
        private int _STT_Buoi;
        private int _TongHocPhi_Buoi;
        private bool _TrangThai;

        public enum LichHocFields
        {
            Ma_LopHoc,
            NgayHoc,
            KipHoc,
            Ma_BuoiHoc,
            PhongHoc,
            STT_Buoi,
            TongHocPhi_Buoi,
            TrangThai
        }

        public EC_LichHoc()
        {

        }

        public EC_LichHoc(string _ma_LopHoc, DateTime _ngayHoc, int _kip, string _ma_BUoiHoc, string _phongHoc, int _stt_Buoi, int _tongHocPhi_Buoi, bool _trangThai)
        {
            Ma_LopHoc = _ma_LopHoc;
            Ma_BuoiHoc = _ma_BUoiHoc;
            NgayHoc = _ngayHoc;
            KipHoc = _kip;
            PhongHoc = _phongHoc;
            STT_Buoi = _stt_Buoi;
            TongHocPhi_Buoi = _tongHocPhi_Buoi;
            TrangThai = _trangThai;
        }
        public string Ma_LopHoc { get => _Ma_LopHoc; set => _Ma_LopHoc = value; }
        public DateTime NgayHoc { get => _NgayHoc; set => _NgayHoc = value; }
        public int KipHoc { get => _KipHoc; set => _KipHoc = value; }
        public string Ma_BuoiHoc { get => _Ma_BuoiHoc; set => _Ma_BuoiHoc = value; }
        public int TongHocPhi_Buoi { get => _TongHocPhi_Buoi; set => _TongHocPhi_Buoi = value; }
        public string PhongHoc { get => _PhongHoc; set => _PhongHoc = value; }
        public int STT_Buoi { get => _STT_Buoi; set => _STT_Buoi = value; }
        public bool TrangThai { get => _TrangThai; set => _TrangThai = value; }
    }
}
