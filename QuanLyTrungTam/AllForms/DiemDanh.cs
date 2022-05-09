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
    public partial class DiemDanh : UserControl
    {
        string Ma_Lop = "";
        List<EC_HocSinh> DanhSachLop;
        List<EC_LichHoc> listBuoiHoc;
        public DiemDanh(string _maLopHoc)
        {
            Ma_Lop = _maLopHoc;
            InitializeComponent();
            LoadForm();
        }
        int Stt = 0;
        void LoadForm()
        {
            txbMa_LopHoc.Text = Ma_Lop;
            EC_LopHoc Lop = new BUS_LopHoc().Select_ByPrimaryKey(Ma_Lop);
            string Ten_GiaoVien = new BUS_GiaoVien().Select_ByPrimaryKey(Lop.Ma_GiaoVien).Ten_GiaoVien;
            txbTen_GiaoVien.Text = Ten_GiaoVien;
            txbTongSoBuoi.Text = Lop.SoBuoi.ToString();
            txbTongHocPhi.Text = Lop.TongHocPhi_KhoaHoc.ToString();

            BUS_LichHoc busLH = new BUS_LichHoc();
            listBuoiHoc = busLH.SelectByFields("Ma_LopHoc", Ma_Lop);
            LayDuLieu(Stt);
        }

        void LayDuLieu(int SttBuoi)
        {
            int SoHS_DiHoc = 0;
            EC_LichHoc ecLichHoc = new EC_LichHoc();
            DanhSachLop = new List<EC_HocSinh>();
            dgLopHoc.Rows.Clear();

            foreach (EC_LichHoc ec in listBuoiHoc)
            {
                if (ec.STT_Buoi == (SttBuoi + 1))
                {
                    ecLichHoc = ec;
                }
            }
            if (ecLichHoc == null)
            {
                return;
            }

            List<EC_BuoiHoc_HocSinh> listBH_HS = new BUS_BuoiHoc_HocSinh().SelectByFields("Ma_BuoiHoc", ecLichHoc.Ma_BuoiHoc);
            if (listBH_HS.Count == 0)
            {
                return;
            }
            foreach (EC_BuoiHoc_HocSinh bhhs in listBH_HS)
            {
                EC_HocSinh hs = new BUS_HocSinh().Select_ByPrimaryKey(bhhs.Ma_HocSinh);
                DanhSachLop.Add(hs);
                if (bhhs.DiemDanh == true)
                {
                    SoHS_DiHoc++;
                }
            }

            int i = 1;

            foreach (EC_HocSinh HocSinh in DanhSachLop)
            {
                string gioitinh = HocSinh.GioiTinh == true ? "Nam" : "Nữ";
                bool DiemDanh = false;
                foreach (EC_BuoiHoc_HocSinh bhhs in listBH_HS)
                {
                    if (bhhs.Ma_HocSinh == HocSinh.Ma_HocSinh)
                    {
                        DiemDanh = bhhs.DiemDanh;
                    }
                }
                dgLopHoc.Rows.Add(i.ToString(), HocSinh.Ten_HocSinh, HocSinh.NgaySinh,
                    gioitinh, HocSinh.SDT, DiemDanh);
                i++;
            }

            DateTime NgayHoc = ecLichHoc.NgayHoc;
            if (NgayHoc.Day == DateTime.Now.Day && NgayHoc.Month == DateTime.Now.Month && NgayHoc.Year == DateTime.Now.Year)
            {
                btDiemDanh.Visible = true;
            }
            else
            {
                btDiemDanh.Visible = false;
            }

            dateTimePicker1.Value = NgayHoc;
            txbKip.Text = ecLichHoc.KipHoc.ToString();
            txbSoHocSinh.Text = DanhSachLop.Count.ToString();

            int SoHS_Vang = DanhSachLop.Count - SoHS_DiHoc;
            txbDiHoc.Text = SoHS_DiHoc.ToString();
            txbVang.Text = SoHS_Vang.ToString();

            txbTrangThai.Text = ecLichHoc.TrangThai == true ? "Đã học" : "Chưa học";

            txbHocPhi_Buoi.Text = ecLichHoc.TongHocPhi_Buoi.ToString();
            txbMa_BuoiHoc.Text = ecLichHoc.Ma_BuoiHoc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Stt == 1)
            {
                return;
            }
            else
            {
                Stt--;
                LayDuLieu(Stt);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int SoBuoi = Convert.ToInt32(txbTongSoBuoi.Text);
            int SoBuoi_DaHoc = listBuoiHoc.Count;
            if (Stt == SoBuoi || SoBuoi_DaHoc == Stt)
            {
                return;
            }
            else
            {
                Stt++;
                LayDuLieu(Stt);
            }
        }

        private void btDiemDanh_Click(object sender, EventArgs e)
        {
            EC_LichHoc BuoiHoc = listBuoiHoc[Stt];
            string Ma_BuoiHoc = BuoiHoc.Ma_BuoiHoc;

            int SoDiHoc = 0;
            foreach (DataGridViewRow row in dgLopHoc.Rows)
            {
                if (Convert.ToBoolean(row.Cells["DiemDanhh"].Value) == true)
                {
                    SoDiHoc++;
                }
            }
            int TongSoTien_Buoi = BuoiHoc.TongHocPhi_Buoi;
            int SoTien_Buoi;
            if (SoDiHoc == 0)
            {
                SoTien_Buoi = 0;
                return;
            }
            else
            {
                SoTien_Buoi = TongSoTien_Buoi / SoDiHoc;
            }
            foreach (DataGridViewRow row in dgLopHoc.Rows)
            {
                string Ten_HocSinh = row.Cells["Ten_HocSinh"].Value.ToString();
                string SDT = row.Cells["SDT"].Value.ToString();
                string Ma_HocSinh = "";
                foreach (EC_HocSinh hs in DanhSachLop)
                {
                    if (hs.Ten_HocSinh == Ten_HocSinh && hs.SDT == SDT)
                    {
                        Ma_HocSinh = hs.Ma_HocSinh;
                    }
                }
                EC_BuoiHoc_HocSinh bhhs = new EC_BuoiHoc_HocSinh();
                bhhs.Ma_HocSinh = Ma_HocSinh;
                bhhs.Ma_BuoiHoc = Ma_BuoiHoc;
                bhhs.DiemDanh = Convert.ToBoolean(row.Cells["DiemDanhh"].Value);
                bhhs.DongTien = false;
                bhhs.SoTien_Buoi = SoTien_Buoi;
                bhhs.DanhGia = "";
                new BUS_BuoiHoc_HocSinh().SuaDuLieu(bhhs);

            }

            new BUS_LichHoc().SuaTrangThai(Ma_BuoiHoc, true);

            LoadForm();
        }

        private void dgLopHoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string Ma_HocSinh = dgLopHoc.Rows[e.RowIndex].Cells["Ma_HocSinh"].Value.ToString();
            string Ma_BuoiHoc = "";

            List<EC_LichHoc> listLichHoc = new BUS_LichHoc().SelectByFields("Ma_LopHoc", Ma_Lop);
            foreach (EC_LichHoc BuoiHoc in listLichHoc)
            {
                if (BuoiHoc.STT_Buoi == Stt)
                {
                    Ma_BuoiHoc = BuoiHoc.Ma_BuoiHoc;
                }
            }

            if (Ma_BuoiHoc == "")
            {
                MessageBox.Show("Lỗi");
                return;
            }

            try
            {
                new BUS_BuoiHoc_HocSinh().XoaDuLieu(new EC_BuoiHoc_HocSinh(Ma_HocSinh, Ma_BuoiHoc, 0, true, "", true));
                MessageBox.Show("Xóa thành công", "Thông báo");
                LayDuLieu(Stt);
            }
            catch
            {
                MessageBox.Show("Xóa không thành công", "Lỗi");
            }
        }

        //private void btThem_Click(object sender, EventArgs e)
        //{
        //    EC_HocSinh HocSinh = new BUS_HocSinh().Select_ByPrimaryKey(txbMa_HocSinh.Text);
        //    List<EC_HocSinh> HocSinh2 = new BUS_HocSinh().SelectByFields("Ten_HocSinh", txbTen_HocSinh.Text);

        //    if(HocSinh==null && HocSinh2.Count == 0)
        //    {
        //        MessageBox.Show("Không tìm thấy học sinh cần thêm", "Thông báo");
        //        return;
        //    }
        //    string Ma_BuoiHoc = "";

        //    List<EC_LichHoc> listLichHoc = new BUS_LichHoc().SelectByFields("Ma_LopHoc", Ma_Lop);
        //    foreach (EC_LichHoc BuoiHoc in listLichHoc)
        //    {
        //        if (BuoiHoc.STT_Buoi == Stt)
        //        {
        //            Ma_BuoiHoc = BuoiHoc.Ma_BuoiHoc;
        //        }
        //    }

        //    if (HocSinh != null)
        //    {
        //        EC_BuoiHoc_HocSinh check = new BUS_BuoiHoc_HocSinh().SelectByMa(Ma_BuoiHoc, HocSinh.Ma_HocSinh);
        //        if (check == null)
        //        {
        //            EC_BuoiHoc_HocSinh BuoiHoc_HocSinh = new EC_BuoiHoc_HocSinh(HocSinh.Ma_HocSinh, Ma_BuoiHoc, 0, false, "", false);
        //            new BUS_BuoiHoc_HocSinh().ThemDuLieu(BuoiHoc_HocSinh);
        //        }
        //    }

        //    if (HocSinh2.Count > 0)
        //    {
        //        foreach(EC_HocSinh i in HocSinh2)
        //        {
        //            EC_BuoiHoc_HocSinh check = new BUS_BuoiHoc_HocSinh().SelectByMa(Ma_BuoiHoc, i.Ma_HocSinh);
        //            if (check == null)
        //            {
        //                EC_BuoiHoc_HocSinh BuoiHoc_HocSinh = new EC_BuoiHoc_HocSinh(i.Ma_HocSinh, Ma_BuoiHoc, 0, false, "", false);
        //                new BUS_BuoiHoc_HocSinh().ThemDuLieu(BuoiHoc_HocSinh);
        //            }
        //        }
        //    }

        //    List<EC_BuoiHoc_HocSinh> list = new BUS_BuoiHoc_HocSinh().SelectByFields("Ma_BuoiHoc", Ma_BuoiHoc);
        //    int SoHocSinh = list.Count;
        //    int TongSoTien_Buoi = Convert.ToInt32(txbHocPhi_Buoi.Text);
        //    int SoTien = TongSoTien_Buoi / SoHocSinh;

        //    foreach(EC_BuoiHoc_HocSinh i in list)
        //    {
        //        i.SoTien_Buoi = SoTien;
        //        new BUS_BuoiHoc_HocSinh().SuaDuLieu(i);
        //    }
        //}
    }
}
