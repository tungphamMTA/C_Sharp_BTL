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
    public partial class QuanLyLichHoc : UserControl
    {
        string Ma_LopHoc;
        int Quyen;
        DataTable LichHoc;
        public QuanLyLichHoc(string _ma_LopHoc, int _Quyen)
        {
            InitializeComponent();
            Ma_LopHoc = _ma_LopHoc;
            Quyen = _Quyen;
            LoadForm();
        }

        string TaoMa_BuoiHoc(string Ma_LopHoc_Cu)
        {

            int x = 2;
            for (int i = 2; i < Ma_LopHoc_Cu.Length; i++)
            {
                if (Ma_LopHoc_Cu[i] != '0')
                {
                    x = i;
                }
            }
            string Ma_1 = Ma_LopHoc_Cu.Substring(0, x);
            string Ma_2 = Ma_LopHoc_Cu.Substring(x);
            int int_Ma_2 = Convert.ToInt32(Ma_2);
            int int_Ma_2_Moi = int_Ma_2 + 1;
            return Ma_1 + int_Ma_2_Moi.ToString();
        }

        void LoadForm()
        {
            if (Quyen == 0)
            {
                btThem.Visible = btSua.Visible = btXoa.Visible = false;
            }
            dgLichHoc.Rows.Clear();
            List<EC_LichHoc> listBuoiHoc = new BUS_LichHoc().SelectByFields("Ma_LopHoc", Ma_LopHoc);
            int stt = 1;
            foreach (EC_LichHoc BuoiHoc in listBuoiHoc)
            {
                string TrangThai = BuoiHoc.TrangThai == true ? "Đã học" : "Chưa học";
                dgLichHoc.Rows.Add(stt, BuoiHoc.Ma_BuoiHoc, BuoiHoc.NgayHoc, BuoiHoc.KipHoc, BuoiHoc.PhongHoc,
                    BuoiHoc.TongHocPhi_Buoi, TrangThai);
                stt++;
            }

            txbMa_LopHoc.Text = Ma_LopHoc;

            EC_LopHoc LopHoc = new BUS_LopHoc().Select_ByPrimaryKey(Ma_LopHoc);

            EC_GiaoVien GiaoVien = new BUS_GiaoVien().Select_ByPrimaryKey(LopHoc.Ma_GiaoVien);
            txbTen_Giaoien.Text = GiaoVien.Ten_GiaoVien;

            EC_MonHoc MonHoc = new BUS_MonHoc().Select_ByPrimaryKey(LopHoc.Ma_MonHoc);
            txbTen_MonHoc.Text = MonHoc.Ten_MonHoc + " " + MonHoc.Lop.ToString();
        }

        public enum BuoiHoc1
        {
            Ma_BuoiHoc,
            NgayHoc,
            KipHoc,
            Phong,
            TongHocPhi_Buoi,
            TrangThai
        }

        private void dgLichHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            if (e.ColumnIndex == 7)
            {
                string ThaoTac = dgLichHoc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (ThaoTac == "Thêm")
                {
                    string Ma_BuoiHoc_Cu = new BUS_LichHoc().SelectAll()[0].Ma_BuoiHoc;
                    string Ma_BuoiHoc_Moi = TaoMa_BuoiHoc(Ma_BuoiHoc_Cu);
                    DataGridViewRow cell = dgLichHoc.Rows[e.RowIndex];
                    int Kip = Int32.Parse(cell.Cells["KipHoc"].Value.ToString());
                    string Phong = cell.Cells["Phong"].Value.ToString();
                    DateTime NgayHoc = Convert.ToDateTime(cell.Cells["NgayHoc"].Value.ToString());
                    int TongHocPhi_Buoi = Convert.ToInt32(cell.Cells["TongHocPhi_Buoi"].Value.ToString());
                    bool TrangThai = cell.Cells[6].Value.ToString() == "Đã học" ? true : false;

                    int Stt_Buoi = Convert.ToInt32(cell.Cells["STT"].Value);
                    List<EC_LichHoc> ListLichHoc = new BUS_LichHoc().SelectByFields("Ma_LopHoc", Ma_LopHoc);
                    foreach (EC_LichHoc Buoi in ListLichHoc)
                    {
                        if (Stt_Buoi == Buoi.STT_Buoi)
                        {
                            MessageBox.Show("Số thứ tự buổi học đã tồn tại", "Thông báo");
                            return;
                        }
                    }

                    EC_LichHoc BuoiHoc = new EC_LichHoc(Ma_LopHoc, DateTime.Now, Kip, Ma_BuoiHoc_Moi,
                        Phong, Stt_Buoi, TongHocPhi_Buoi, TrangThai);

                    new BUS_LichHoc().ThemDuLieu(BuoiHoc);
                }
                else if (ThaoTac == "Sửa")
                {
                    DataGridViewRow cell = dgLichHoc.Rows[e.RowIndex];
                    EC_LichHoc BuoiHoc = new BUS_LichHoc().Select_ByPrimaryKey(cell.Cells["Ma_BuoiHoc"].Value.ToString());

                    BuoiHoc.STT_Buoi = Int32.Parse(cell.Cells["STT"].Value.ToString());
                    BuoiHoc.NgayHoc = Convert.ToDateTime(cell.Cells["NgayHoc"].Value);
                    BuoiHoc.PhongHoc = cell.Cells["PhongHoc"].Value.ToString();

                    new BUS_LichHoc().SuaDuLieu(BuoiHoc);
                }
            }
            else
            {
                string Ma_BuoiHoc = dgLichHoc.Rows[e.RowIndex].Cells["Ma_BuoiHoc"].Value.ToString();
                EC_LichHoc LichHoc = new BUS_LichHoc().Select_ByPrimaryKey(Ma_BuoiHoc);
                txbMa_BuoiHoc.Text = Ma_BuoiHoc;
                dtNgayHoc.Value = LichHoc.NgayHoc;
                txbHocPhi_Buoi.Text = LichHoc.TongHocPhi_Buoi.ToString();
                cbKip.SelectedItem = LichHoc.KipHoc.ToString();
                cbTrangThai.SelectedIndex = LichHoc.TrangThai == true ? 0 : 1;
                txbPhongHoc.Text = LichHoc.PhongHoc;
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            string Ma_BuoiHoc = txbMa_BuoiHoc.Text;
            if (Ma_BuoiHoc == "")
            {
                return;
            }
            try
            {
                EC_LichHoc LichHoc = new BUS_LichHoc().Select_ByPrimaryKey(Ma_BuoiHoc);
                LichHoc.NgayHoc = dtNgayHoc.Value;
                LichHoc.KipHoc = Int32.Parse(cbKip.SelectedItem.ToString());
                LichHoc.TongHocPhi_Buoi = Int32.Parse(txbHocPhi_Buoi.Text);
                LichHoc.PhongHoc = txbPhongHoc.Text;
                new BUS_LichHoc().SuaDuLieu(LichHoc);
                MessageBox.Show("Sửa thành công!", "Thông báo");
            }
            catch
            {
                MessageBox.Show("Sửa không thành công!", "Thông báo");

            }
            LoadForm();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            string Ma_BuoiHoc = txbMa_BuoiHoc.Text;
            if (Ma_BuoiHoc == "")
            {
                return;
            }
            try
            {
                BUS_LichHoc busLichHoc = new BUS_LichHoc();
                BUS_LopHoc busLopHoc = new BUS_LopHoc();
                EC_LichHoc LichHoc = busLichHoc.Select_ByPrimaryKey(Ma_BuoiHoc);
                EC_LopHoc LopHoc = busLopHoc.Select_ByPrimaryKey(LichHoc.Ma_LopHoc);
                if (LichHoc.TrangThai == true)
                {
                    MessageBox.Show("Buổi học không thể xóa do đã học rồi", "Thông báo");
                    return;
                }

                LopHoc.SoBuoi -= 1;
                busLopHoc.SuaDuLieu(LopHoc);
                busLichHoc.XoaDuLieu_Ma_BuoiHoc(LichHoc.Ma_BuoiHoc);

                List<EC_LichHoc> listBuoiHoc = busLichHoc.SelectByFields("Ma_LopHoc", LichHoc.Ma_LopHoc);

                int SoBuoi_ChuaHoc = 0;
                int TongTien_DaHoc = 0;
                foreach (EC_LichHoc ec in listBuoiHoc)
                {
                    if (ec.TrangThai == false)
                    {
                        SoBuoi_ChuaHoc++;
                    }
                    else
                    {
                        TongTien_DaHoc += ec.TongHocPhi_Buoi;
                    }
                }
                int TongHocPhi_Buoi = (LopHoc.TongHocPhi_KhoaHoc - TongTien_DaHoc) / SoBuoi_ChuaHoc;
                foreach (EC_LichHoc ec in listBuoiHoc)
                {
                    if (ec.TrangThai == false)
                    {
                        if (ec.STT_Buoi > LichHoc.STT_Buoi)
                        {
                            ec.STT_Buoi -= 1;
                        }
                        ec.TongHocPhi_Buoi = TongHocPhi_Buoi;
                    }
                }
                foreach (EC_LichHoc ec in listBuoiHoc)
                {
                    busLichHoc.SuaDuLieu(ec);
                }
                MessageBox.Show("Xóa thành công buổi học", "Thông báo");
            }
            catch
            {
                MessageBox.Show("Xóa không thành công buổi học", "Thông báo");
            }
            LoadForm();
        }
    }
}
