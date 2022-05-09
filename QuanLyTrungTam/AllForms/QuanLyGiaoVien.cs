using QuanLyTrungTam.Bus;
using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTrungTam.AllForms
{
    public partial class QuanLyGiaoVien : UserControl
    {
        public QuanLyGiaoVien()
        {
            InitializeComponent();
            List<EC_GiaoVien> listGiaoVien = new BUS_GiaoVien().SelectAll();
            LoadForm(listGiaoVien);
            cbTrinhDo.SelectedIndex = 0;
            cbGioiTinh.SelectedIndex = 0;
            cbTrinhDo_Search.SelectedIndex = 0;
            cbGioiTinh_Search.SelectedIndex = 0;
            dtNgaySinh.Value = DateTime.Now;
        }
        void LoadForm(List<EC_GiaoVien> listGiaoVien)
        {
            dgDanhsach.Rows.Clear();
            int x = 1;
            foreach (EC_GiaoVien i in listGiaoVien)
            {
                string GioiTinh = i.GioiTinh == true ? "Nam" : "Nữ";
                dgDanhsach.Rows.Add(x.ToString(), i.Ma_GiaoVien, i.Ten_GiaoVien, i.NgaySinh.ToShortDateString(), GioiTinh, i.SDT, i.TrinhDo);
                x++;
            }
        }

        private void btShowAll_Click(object sender, EventArgs e)
        {
            List<EC_GiaoVien> listGiaoVien = new BUS_GiaoVien().SelectAll();
            LoadForm(listGiaoVien);
        }

        private void btTimKiem_Click_1(object sender, EventArgs e)
        {
            string Ma_GiaoVien = txbMa_GiaoVien_Search.Text;
            string Ten_GiaoVien = txbTen_GiaoVien_Search.Text;
            string GioiTinh = cbGioiTinh_Search.SelectedItem.ToString();
            string TrinhDo = cbTrinhDo_Search.SelectedItem.ToString();

            List<EC_GiaoVien> listResult = new List<EC_GiaoVien>();

            if (Ma_GiaoVien != "")
            {
                EC_GiaoVien GV = new BUS_GiaoVien().Select_ByPrimaryKey(Ma_GiaoVien);
                if (GV != null)
                {
                    listResult.Add(GV);
                }
            }
            else if (Ten_GiaoVien != "")
            {
                List<EC_GiaoVien> list1 = new BUS_GiaoVien().SelectByFields("Ten_GiaoVien", Ten_GiaoVien);
                foreach (EC_GiaoVien i in list1)
                {
                    if (listResult.IndexOf(i) == -1)
                    {
                        listResult.Add(i);
                    }
                }
            }
            else if (GioiTinh != " ")
            {
                int Gt = GioiTinh == "Nam" ? 1 : 0;
                List<EC_GiaoVien> list1 = new BUS_GiaoVien().SelectByFields("GioiTinh", Gt);
                foreach (EC_GiaoVien i in list1)
                {
                    if (listResult.IndexOf(i) == -1)
                    {
                        listResult.Add(i);
                    }
                }
            }
            else if (TrinhDo != " ")
            {
                List<EC_GiaoVien> list1 = new BUS_GiaoVien().SelectByFields("TrinhDo", TrinhDo);
                foreach (EC_GiaoVien i in list1)
                {
                    if (listResult.IndexOf(i) == -1)
                    {
                        listResult.Add(i);
                    }
                }
            }
            else
            {

            }
            if (listResult.Count > 0)
            {
                LoadForm(listResult);
            }
            else
            {
                List<EC_GiaoVien> listGiaoVien = new BUS_GiaoVien().SelectAll();
                LoadForm(listGiaoVien);
            }
        }
        private string TaoMa_GiaoVien()
        {
            string result = "GV";
            List<EC_GiaoVien> listGiaoVien = new BUS_GiaoVien().SelectAll();
            if (listGiaoVien.Count == 0)
            {
                result = "GV00001";
                return result;
            }
            string Ma_Cu = listGiaoVien[0].Ma_GiaoVien;
            foreach (EC_GiaoVien ec in listGiaoVien)
            {
                int Int_MaCu = Convert.ToInt32(Ma_Cu.Substring(2));
                int Int_Ma = Convert.ToInt32(ec.Ma_GiaoVien.Substring(2));
                if (Int_Ma > Int_MaCu)
                {
                    Ma_Cu = ec.Ma_GiaoVien;
                }
            }
            int Int_MaMoi = Convert.ToInt32(Ma_Cu.Substring(2)) + 1;
            int length = 7 - 2 - Int_MaMoi.ToString().Length;
            for (int i = 0; i < length; i++)
            {
                result += "0";
            }
            result += Int_MaMoi.ToString();
            return result;
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            bool gt = cbGioiTinh.SelectedIndex == 0 ? true : false;
            string TrinhDo;
            if (cbTrinhDo.SelectedIndex == 0)
            {
                MessageBox.Show("Trình độ không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                TrinhDo = cbTrinhDo.SelectedItem.ToString();
            }
            if (dtNgaySinh.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Bạn hãy thêm ảnh bằng cách nhấn đúp vào khung hình ảnh nhé!", "Thông báo");
                return;
            }
            try
            {
                EC_GiaoVien HocSinh = new EC_GiaoVien(TaoMa_GiaoVien(), txbTen_GiaoVien.Text, dtNgaySinh.Value, gt, txbDiaChi.Text, txbSDT.Text, txbEmail.Text, TrinhDo, "", byteAnh);
                BUS_GiaoVien busHS = new BUS_GiaoVien();
                busHS.ThemDuLieu(HocSinh);
                MessageBox.Show("Thêm giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<EC_GiaoVien> listGiaoVien = new BUS_GiaoVien().SelectAll();
                LoadForm(listGiaoVien);
            }
            catch
            {
                MessageBox.Show("Thêm giáo viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            pictureBox1.Image = null;
            txbMa_GiaoVien.Text = txbTen_GiaoVien.Text = txbSDT.Text = txbDiaChi.Text = txbEmail.Text = txbSDT.Text
                = txbTenDangNhap.Text = txbID.Text = txbMatKhau.Text = "";
            dtNgaySinh.Value = DateTime.Now;
            cbGioiTinh.SelectedIndex = cbTrinhDo.SelectedIndex = 0;

            DataGridViewRow row = dgDanhsach.Rows[e.RowIndex];
            txbMa_GiaoVien.Text = row.Cells["Ma_GiaoVien"].Value.ToString();
            EC_GiaoVien GiaoVien = new BUS_GiaoVien().Select_ByPrimaryKey(txbMa_GiaoVien.Text);
            txbTen_GiaoVien.Text = row.Cells["Ten_GiaoVien"].Value.ToString();
            txbDiaChi.Text = GiaoVien.DiaChi;
            txbEmail.Text = GiaoVien.Email;
            txbSDT.Text = GiaoVien.SDT;
            cbGioiTinh.SelectedIndex = GiaoVien.GioiTinh == true ? 0 : 1;
            dtNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value.ToString());
            if (GiaoVien.Anh != null)
            {
                pictureBox1.Image = HinhAnh.ByteToImage(GiaoVien.Anh);
            }
            foreach (string i in cbTrinhDo.Items)
            {
                if (i == row.Cells["TrinhDo"].Value.ToString())
                {
                    cbTrinhDo.SelectedItem = i;
                }
            }

            string Ma_GiaoVien = row.Cells["Ma_GiaoVien"].Value.ToString();
            dgDayHoc.Rows.Clear();
            List<EC_PhanCong_Day> listPhanCongDay = new BUS_PhanCong_Day().SelectByFields("Ma_GiaoVien", Ma_GiaoVien);
            int j = 1;
            foreach (EC_PhanCong_Day i in listPhanCongDay)
            {
                EC_MonHoc MonHoc = new BUS_MonHoc().Select_ByPrimaryKey(i.Ma_MonHoc);
                dgDayHoc.Rows.Add(j.ToString(), MonHoc.Ten_MonHoc, MonHoc.Lop.ToString());
                j++;
            }

            string ID = new BUS_GiaoVien().Select_ByPrimaryKey(Ma_GiaoVien).ID;
            if (ID == "")
            {
                txbID.Enabled = txbTenDangNhap.Enabled = true;
                txbMatKhau.Visible = true;
            }
            else
            {
                EC_TaiKhoan TaiKhoan = new BUS_TaiKhoan().SelectByMa(ID);
                txbID.Text = ID;
                if (TaiKhoan == null)
                {
                    txbID.Enabled = false;
                    txbTenDangNhap.Enabled = true;
                    txbMatKhau.Visible = true;
                }
                else
                {
                    txbID.Enabled = txbTenDangNhap.Enabled = false;
                    txbMatKhau.Visible = false;
                    txbTenDangNhap.Text = TaiKhoan.TenDangNhap;
                }
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            txbMa_GiaoVien.Text = txbTen_GiaoVien.Text = txbSDT.Text = txbDiaChi.Text = txbEmail.Text = txbSDT.Text
                = txbTenDangNhap.Text = txbID.Text = txbMatKhau.Text = "";
            dtNgaySinh.Value = DateTime.Now;
            byteAnh = null;
            cbGioiTinh.SelectedIndex = cbTrinhDo.SelectedIndex = 0;
            List<EC_GiaoVien> listGiaoVien = new BUS_GiaoVien().SelectAll();
            LoadForm(listGiaoVien);
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (txbMa_GiaoVien.Text == "")
            {
                return;
            }
            bool GioiTinh = cbGioiTinh.SelectedIndex == 0 ? true : false;
            EC_GiaoVien GiaoVien = new EC_GiaoVien(txbMa_GiaoVien.Text, txbTen_GiaoVien.Text, dtNgaySinh.Value, GioiTinh,
                txbDiaChi.Text, txbSDT.Text, txbEmail.Text, cbTrinhDo.SelectedItem.ToString(), txbID.Text, byteAnh);
            try
            {
                new BUS_GiaoVien().SuaDuLieu(GiaoVien);
                MessageBox.Show("Sửa thành công!", "Thông báo");
            }
            catch
            {
                MessageBox.Show("Sửa không thành công!", "Thông báo");
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            string Ma_GiaoVien = txbMa_GiaoVien.Text;
            if (Ma_GiaoVien == "")
            {
                MessageBox.Show("Chọn giáo viên để xóa", "Thông báo");
                return;
            }
            EC_GiaoVien GiaoVien = new EC_GiaoVien();
            GiaoVien.Ma_GiaoVien = Ma_GiaoVien;
            try
            {
                new BUS_GiaoVien().XoaDuLieu(GiaoVien);
                MessageBox.Show("Xóa thành công!", "Thông báo");
            }
            catch
            {
                MessageBox.Show("Xóa không thành công!", "Thông báo");
            }
        }

        string Tao_ID()
        {
            List<EC_TaiKhoan> listTaiKhoan = new BUS_TaiKhoan().SelectAll();
            int ID_Max = 0;
            foreach (EC_TaiKhoan i in listTaiKhoan)
            {
                int ID_Int = Int32.Parse(i.ID.Substring(4));
                if (ID_Int > ID_Max)
                {
                    ID_Max = ID_Int;
                }
            }

            string So_Moi = (ID_Max + 1).ToString();
            string ID_Moi = "TTHT";
            int x = 9 - So_Moi.Length - 4;
            for (int i = 0; i < x; i++)
            {
                ID_Moi += "0";
            }
            ID_Moi += So_Moi;
            return ID_Moi;
        }
        private void btThem_TaiKhoan_Click(object sender, EventArgs e)
        {
            string Ma_GiaoVien = txbMa_GiaoVien.Text;
            if (Ma_GiaoVien == "")
            {
                return;
            }
            EC_GiaoVien GiaoVien = new BUS_GiaoVien().Select_ByPrimaryKey(Ma_GiaoVien);
            if (GiaoVien.ID == "")
            {
                try
                {
                    string MatKhau = Hash.getHashString(txbMatKhau.Text);
                    string ID = Tao_ID();
                    EC_TaiKhoan TaiKhoan = new EC_TaiKhoan(ID, txbTenDangNhap.Text, MatKhau);
                    new BUS_TaiKhoan().ThemDuLieu(TaiKhoan);
                    GiaoVien.ID = ID;
                    new BUS_GiaoVien().SuaDuLieu(GiaoVien);
                    MessageBox.Show("Tạo tài khoản thành công", "Thông báo");
                }
                catch
                {
                    MessageBox.Show("Xem lại các thông tin đã tạo", "Thông báo");
                }
            }
            else
            {
                try
                {
                    string MatKhau = Hash.getHashString(txbMatKhau.Text);
                    EC_TaiKhoan TaiKhoan = new EC_TaiKhoan(GiaoVien.ID, txbTenDangNhap.Text, MatKhau);
                    new BUS_TaiKhoan().ThemDuLieu(TaiKhoan);
                }
                catch
                {
                    MessageBox.Show("Xem lại các thông tin đã tạo", "Thông báo");
                }
            }
        }
        byte[] byteAnh;
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            string filename = "";
            Thread thr = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Multiselect = false;
                if (open.ShowDialog() == DialogResult.OK)
                {
                    filename = open.FileName.ToString();
                }
            }));
            thr.SetApartmentState(ApartmentState.STA);
            thr.Start();
            thr.Join();

            if (filename == "")
            {
                return;
            }

            byte[] arrByte = HinhAnh.StringToByte(filename);
            byteAnh = arrByte;
            pictureBox1.Image = HinhAnh.ByteToImage(arrByte);
            if (txbMa_GiaoVien.Text == "")
            {
                return;
            }
            else
            {
                EC_GiaoVien gv = new BUS_GiaoVien().Select_ByPrimaryKey(txbMa_GiaoVien.Text);
                gv.Anh = arrByte;
                try
                {
                    new BUS_GiaoVien().SuaDuLieu(gv);
                    MessageBox.Show("Lưu ảnh thành công", "Thông báo");
                }
                catch
                {
                    MessageBox.Show("Lưu ảnh không thành công", "Thông báo");
                }
            }
        }
    }
}
