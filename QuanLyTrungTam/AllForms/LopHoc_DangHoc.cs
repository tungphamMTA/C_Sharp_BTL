using QuanLyTrungTam.Bus;
using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTrungTam.AllForms
{
    public partial class LopHoc_DangHoc : UserControl
    {
        private string Ma_HocSinh;
        int select;
        public LopHoc_DangHoc(string _Ma, int _select)
        {
            Ma_HocSinh = _Ma;
            select = _select;
            InitializeComponent();
            Load();
        }
        function ft = new function();
        void Load()
        {
            if (select == 1)
            {
                DataTable LopHoc_DangHoc = ft.LopHoc_DangHoc(Ma_HocSinh);
                if (LopHoc_DangHoc.Rows.Count == 0)
                {
                    return;
                }
                int i = 1;
                foreach (DataRow row in LopHoc_DangHoc.Rows)
                {
                    string Ma_LopHoc = row["Ma_LopHoc"].ToString();
                    EC_LopHoc LopHoc = new BUS_LopHoc().Select_ByPrimaryKey(Ma_LopHoc);
                    dgDanhSach_LopHoc.Rows.Add(i.ToString(), LopHoc.Ma_LopHoc,
                        new BUS_MonHoc().Select_ByPrimaryKey(LopHoc.Ma_MonHoc).Ten_MonHoc,
                        new BUS_GiaoVien().Select_ByPrimaryKey(LopHoc.Ma_GiaoVien).Ten_GiaoVien);
                    i++;
                }
            }
        }

        private void dgDanhSach_LopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            string Ma_LopHoc = dgDanhSach_LopHoc.Rows[e.RowIndex].Cells["Ma_LopHoc"].Value.ToString();
            List<EC_LichHoc> listLichHoc = new BUS_LichHoc().SelectByFields("Ma_LopHoc", Ma_LopHoc);
            foreach (EC_LichHoc i in listLichHoc)
            {
                EC_BuoiHoc_HocSinh BuoiHoc_HS = new BUS_BuoiHoc_HocSinh().SelectByMa(i.Ma_BuoiHoc, Ma_HocSinh);
                if (BuoiHoc_HS == null)
                {
                    continue;
                }
                string TrangThai = i.TrangThai == true ? "Đã học" : "Chưa học";
                string DiemDanh = BuoiHoc_HS.DiemDanh == true ? "Học" : "Nghỉ";
                dgDanhSach_BuoiHoc.Rows.Add(i.STT_Buoi, i.NgayHoc.ToShortDateString(), i.KipHoc, i.PhongHoc, TrangThai, DiemDanh);
            }
            EC_LopHoc LopHoc = new BUS_LopHoc().Select_ByPrimaryKey(Ma_LopHoc);
            txbTen_GiaoVien.Text = new BUS_GiaoVien().Select_ByPrimaryKey(LopHoc.Ma_GiaoVien).Ten_GiaoVien;
            txbSoBuoi.Text = LopHoc.SoBuoi.ToString();
            EC_MonHoc MonHoc = new BUS_MonHoc().Select_ByPrimaryKey(LopHoc.Ma_MonHoc);
            txbLop.Text = MonHoc.Lop.ToString();
            txbTen_MonHoc.Text = MonHoc.Ten_MonHoc;
            txbTongHP.Text = LopHoc.TongHocPhi_KhoaHoc.ToString();
            txbTrinhDo.Text = LopHoc.TrinhDo;
            dtNgayBatdau.Value = LopHoc.Ngay_BatDau;

            dgDanhSach_HocSinh.Rows.Clear();
            List<EC_LichHoc> listBuoiHoc = new BUS_LichHoc().SelectByFields("Ma_LopHoc", Ma_LopHoc);
            List<EC_BuoiHoc_HocSinh> listBuoiHoc_HocSinh = new BUS_BuoiHoc_HocSinh().SelectByFields("Ma_BuoiHoc", listBuoiHoc[0].Ma_BuoiHoc);
            List<EC_HocSinh> listHocSinh = new List<EC_HocSinh>();
            foreach (EC_BuoiHoc_HocSinh i in listBuoiHoc_HocSinh)
            {
                EC_HocSinh hocsinh = new BUS_HocSinh().Select_ByPrimaryKey(i.Ma_HocSinh);
                listHocSinh.Add(hocsinh);
            }
            foreach (EC_HocSinh i in listHocSinh)
            {
                string GioiTinh = i.GioiTinh == true ? "Nam" : "Nữ";
                dgDanhSach_HocSinh.Rows.Add(i.Ten_HocSinh, i.NgaySinh.ToShortDateString(), GioiTinh, i.DiaChi);
            }
            txbSoHocSinh.Text = listHocSinh.Count.ToString();
        }
    }
}
