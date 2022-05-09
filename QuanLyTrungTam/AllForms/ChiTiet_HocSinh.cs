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
    public partial class ChiTiet_HocSinh : UserControl
    {
        string Ma_HocSinh;
        public ChiTiet_HocSinh(string _Ma_HocSinh)
        {
            InitializeComponent();
            Ma_HocSinh = _Ma_HocSinh;
            Load();
        }
        void Load()
        {
            EC_HocSinh HocSinh = new BUS_HocSinh().Select_ByPrimaryKey(Ma_HocSinh);
            if (HocSinh.Anh != null)
            {
                picAvt.Image = HinhAnh.ByteToImage(HocSinh.Anh);
            }
            txbMa_HocSinh.Text = HocSinh.Ma_HocSinh;
            txbTen_HocSinh.Text = HocSinh.Ten_HocSinh;
            txbDiaChi.Text = HocSinh.DiaChi;
            txbEmail.Text = HocSinh.Email;
            txbSDT.Text = HocSinh.SDT;
            dtNgaySinh.Value = HocSinh.NgaySinh;
            cbGioiTinh.SelectedItem = HocSinh.GioiTinh == true ? "Nam" : "Nữ";
            cbLop.SelectedItem = HocSinh.Lop.ToString();

            function ft = new function();
            DataTable LopHoc_DangHoc = ft.LopHoc_DangHoc(Ma_HocSinh);
            DataTable LopHoc_DaHoc = ft.LopHoc_DaHoc(Ma_HocSinh);
            int index1 = 1;
            foreach (DataRow row in LopHoc_DangHoc.Rows)
            {
                string Ma_LopHoc = row[0].ToString();
                EC_LopHoc LopHoc = new BUS_LopHoc().Select_ByPrimaryKey(Ma_LopHoc);
                EC_MonHoc MonHoc = new BUS_MonHoc().Select_ByPrimaryKey(LopHoc.Ma_MonHoc);
                dgLopHoc.Rows.Add(index1.ToString(), Ma_LopHoc, MonHoc.Ten_MonHoc, MonHoc.Lop, LopHoc.SoBuoi, "Đang học");
                index1++;
            }
            foreach (DataRow row in LopHoc_DaHoc.Rows)
            {
                string Ma_LopHoc = row[0].ToString();
                EC_LopHoc LopHoc = new BUS_LopHoc().Select_ByPrimaryKey(Ma_LopHoc);
                EC_MonHoc MonHoc = new BUS_MonHoc().Select_ByPrimaryKey(LopHoc.Ma_MonHoc);
                dgLopHoc.Rows.Add(index1.ToString(), Ma_LopHoc, MonHoc.Ten_MonHoc, MonHoc.Lop, LopHoc.SoBuoi, "Đã học");
                index1++;
            }

            DataTable HocPhi_Thang = ft.TongTien_Thang(Ma_HocSinh);
            dgHocPhi.Rows.Clear();
            foreach (DataRow row in HocPhi_Thang.Rows)
            {
                string ChuaDong = row["TongTien_ChuaDong"].ToString();
                int TongTien_ChuaDong = 0;
                if (ChuaDong == "" || ChuaDong == null)
                {
                    TongTien_ChuaDong = 0;
                }
                else
                {
                    TongTien_ChuaDong = (int)row["TongTien_ChuaDong"];
                }
                dgHocPhi.Rows.Add(row["Thang"].ToString() + "/" + row["Nam"].ToString(), row["TongTien"].ToString(), (int)row["TongTien"] - TongTien_ChuaDong, TongTien_ChuaDong);
            }
        }

        private void dgLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string Ma_LopHoc = dgLopHoc.Rows[e.RowIndex].Cells["Ma_LopHoc"].Value.ToString();
            if (Ma_LopHoc == "")
            {
                return;
            }
            EC_LopHoc LopHoc = new BUS_LopHoc().Select_ByPrimaryKey(Ma_LopHoc);
            EC_MonHoc MonHoc = new BUS_MonHoc().Select_ByPrimaryKey(LopHoc.Ma_MonHoc);
            EC_GiaoVien GiaoVien = new BUS_GiaoVien().Select_ByPrimaryKey(LopHoc.Ma_GiaoVien);
            txbMa_LopHoc.Text = Ma_LopHoc;
            txbTen.Text = GiaoVien.Ten_GiaoVien;
            txbTen_MonHoc.Text = MonHoc.Ten_MonHoc;
            txbLop.Text = MonHoc.Lop.ToString();
            txbMucDo.Text = LopHoc.TrinhDo;
            txbTongHP.Text = LopHoc.TongHocPhi_KhoaHoc.ToString();
            txbSoBuoi.Text = LopHoc.SoBuoi.ToString();
        }
    }
}
