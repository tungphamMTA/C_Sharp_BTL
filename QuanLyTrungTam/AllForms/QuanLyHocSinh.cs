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
    public partial class QuanLyHocSinh : UserControl
    {
        public QuanLyHocSinh()
        {
            InitializeComponent();
            List<EC_HocSinh> listHocSinh = new BUS_HocSinh().SelectAll();
            LoadForm(listHocSinh);
            cbLop.SelectedIndex = 0;
            cbGioiTinh.SelectedIndex = 0;
            cbLop_Search.SelectedIndex = 0;
            cbGioiTinh_Search.SelectedIndex = 0;
            dtNgaySinh.Value = DateTime.Now;
        }
        void LoadForm(List<EC_HocSinh> listGiaoVien)
        {
            dgDanhsach.Rows.Clear();
            int x = 1;
            if (listGiaoVien == null)
            {
                return;
            }
            foreach (EC_HocSinh i in listGiaoVien)
            {
                string GioiTinh = i.GioiTinh == true ? "Nam" : "Nữ";
                dgDanhsach.Rows.Add(x.ToString(), i.Ma_HocSinh, i.Ten_HocSinh, i.NgaySinh.ToShortDateString(), GioiTinh, i.SDT, i.Lop);
                x++;
            }
        }
        private void btTatCa_Click(object sender, EventArgs e)
        {
            List<EC_HocSinh> listGiaoVien = new BUS_HocSinh().SelectAll();
            LoadForm(listGiaoVien);
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string Ma_HocSinh = txbMa_HocSinh_Search.Text;
            string Ten_HocSinh = txbTen_HocSinh_Search.Text;
            string GioiTinh = cbGioiTinh_Search.SelectedItem.ToString();
            int Lop = cbLop_Search.SelectedIndex;

            List<EC_HocSinh> listResult = new List<EC_HocSinh>();

            if (Ma_HocSinh != "")
            {
                EC_HocSinh GV = new BUS_HocSinh().Select_ByPrimaryKey(Ma_HocSinh);
                if (GV != null)
                {
                    listResult.Add(GV);
                }
            }
            else if (Ten_HocSinh != "")
            {
                List<EC_HocSinh> list1 = new BUS_HocSinh().SelectByFields("Ten_HocSinh", Ten_HocSinh);
                foreach (EC_HocSinh i in list1)
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
                List<EC_HocSinh> list1 = new BUS_HocSinh().SelectByFields("GioiTinh", Gt);
                foreach (EC_HocSinh i in list1)
                {
                    if (listResult.IndexOf(i) == -1)
                    {
                        listResult.Add(i);
                    }
                }
            }
            else if (Lop != 0)
            {
                List<EC_HocSinh> list1 = new BUS_HocSinh().SelectByFields("Lop", Lop);
                foreach (EC_HocSinh i in list1)
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
                List<EC_HocSinh> listGiaoVien = null;
                LoadForm(listGiaoVien);
            }
        }
        private string TaoMa_HocSinh()
        {
            string result = "HS";
            List<EC_HocSinh> listHocSinh = new BUS_HocSinh().SelectAll();
            if (listHocSinh.Count == 0)
            {
                result = "HS00001";
                return result;
            }
            string Ma_Cu = listHocSinh[0].Ma_HocSinh;
            foreach (EC_HocSinh ec in listHocSinh)
            {
                int Int_MaCu = Convert.ToInt32(Ma_Cu.Substring(2));
                int Int_Ma = Convert.ToInt32(ec.Ma_HocSinh.Substring(2));
                if (Int_Ma > Int_MaCu)
                {
                    Ma_Cu = ec.Ma_HocSinh;
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
        private void btThem_Click_1(object sender, EventArgs e)
        {
            bool gt = cbGioiTinh.SelectedIndex == 0 ? true : false;
            int Lop;
            if (cbLop.SelectedIndex == 0)
            {
                MessageBox.Show("Trình độ không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                Lop = cbLop.SelectedIndex + 1;
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
                EC_HocSinh HocSinh = new EC_HocSinh(TaoMa_HocSinh(), txbTen_HocSinh.Text, dtNgaySinh.Value, gt, txbDiaChi.Text, txbSDT.Text, txbEmail.Text, Lop, "", ArrByte_Anh);
                BUS_HocSinh busHS = new BUS_HocSinh();
                busHS.ThemDuLieu(HocSinh);
                MessageBox.Show("Thêm học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<EC_HocSinh> listHocSinh = new BUS_HocSinh().SelectAll();
                LoadForm(listHocSinh);
            }
            catch
            {
                MessageBox.Show("Thêm học sinh thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btSua_Click_1(object sender, EventArgs e)
        {
            if (txbMa_HocSinh.Text == "")
            {
                return;
            }
            bool GioiTinh = cbGioiTinh.SelectedIndex == 0 ? true : false;
            EC_HocSinh HocSinh = new EC_HocSinh(txbMa_HocSinh.Text, txbTen_HocSinh.Text, dtNgaySinh.Value, GioiTinh,
                txbDiaChi.Text, txbSDT.Text, txbEmail.Text, Int32.Parse(cbLop.SelectedItem.ToString()), txbID.Text, null);
            try
            {
                new BUS_HocSinh().SuaDuLieu(HocSinh);
                MessageBox.Show("Sửa thành công!", "Thông báo");
            }
            catch
            {
                MessageBox.Show("Sửa không thành công!", "Thông báo");
            }
        }

        private void btXoa_Click_1(object sender, EventArgs e)
        {
            string Ma_HocSinh = txbMa_HocSinh.Text;
            if (Ma_HocSinh == "")
            {
                MessageBox.Show("Chọn giáo viên để xóa", "Thông báo");
                return;
            }
            EC_HocSinh HocSinh = new EC_HocSinh();
            HocSinh.Ma_HocSinh = Ma_HocSinh;
            try
            {
                new BUS_HocSinh().XoaDuLieu(HocSinh);
                MessageBox.Show("Xóa thành công!", "Thông báo");
            }
            catch
            {
                MessageBox.Show("Xóa không thành công!", "Thông báo");
            }
        }

        private void dgDanhsach_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            pictureBox1.Image = null;
            txbMa_HocSinh.Text = txbTen_HocSinh.Text = txbSDT.Text = txbDiaChi.Text = txbEmail.Text = txbSDT.Text
                = txbTenDangNhap.Text = txbID.Text = txbMatKhau.Text = "";
            dtNgaySinh.Value = DateTime.Now;
            cbGioiTinh.SelectedIndex = cbLop.SelectedIndex = 0;

            DataGridViewRow row = dgDanhsach.Rows[e.RowIndex];
            txbMa_HocSinh.Text = row.Cells["Ma_HocSinh"].Value.ToString();
            EC_HocSinh HocSinh = new BUS_HocSinh().Select_ByPrimaryKey(txbMa_HocSinh.Text);
            txbTen_HocSinh.Text = row.Cells["Ten_HocSinh"].Value.ToString();
            txbDiaChi.Text = HocSinh.DiaChi;
            txbEmail.Text = HocSinh.Email;
            txbSDT.Text = row.Cells["SDT"].Value.ToString();
            cbGioiTinh.SelectedIndex = HocSinh.GioiTinh == true ? 0 : 1;
            dtNgaySinh.Value = HocSinh.NgaySinh;
            if (HocSinh.Anh != null)
            {
                pictureBox1.Image = HinhAnh.ByteToImage(HocSinh.Anh);
            }
            cbLop.SelectedIndex = HocSinh.Lop - 1;

            string Ma_HocSinh = row.Cells["Ma_HocSinh"].Value.ToString();
            string ID = new BUS_HocSinh().Select_ByPrimaryKey(Ma_HocSinh).ID;
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
            txbMa_HocSinh.Text = txbTen_HocSinh.Text = txbSDT.Text = txbDiaChi.Text = txbEmail.Text = txbSDT.Text
                = txbTenDangNhap.Text = txbID.Text = txbMatKhau.Text = "";
            dtNgaySinh.Value = DateTime.Now;
            cbGioiTinh.Text = cbLop.Text = "";
            pictureBox1.Image = null;
            ArrByte_Anh = null;
            List<EC_HocSinh> listHocSinh = new BUS_HocSinh().SelectAll();
            LoadForm(listHocSinh);
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
            string Ma_HocSinh = txbMa_HocSinh.Text;
            if (Ma_HocSinh == "")
            {
                return;
            }
            EC_HocSinh HocSinh = new BUS_HocSinh().Select_ByPrimaryKey(Ma_HocSinh);
            if (HocSinh.ID == "")
            {
                try
                {
                    string MatKhau = Hash.getHashString(txbMatKhau.Text);
                    string ID = Tao_ID();
                    EC_TaiKhoan TaiKhoan = new EC_TaiKhoan(ID, txbTenDangNhap.Text, MatKhau);
                    new BUS_TaiKhoan().ThemDuLieu(TaiKhoan);
                    HocSinh.ID = ID;
                    new BUS_HocSinh().SuaDuLieu(HocSinh);
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
                    EC_TaiKhoan TaiKhoan = new EC_TaiKhoan(HocSinh.ID, txbTenDangNhap.Text, MatKhau);
                    new BUS_TaiKhoan().ThemDuLieu(TaiKhoan);
                }
                catch
                {
                    MessageBox.Show("Xem lại các thông tin đã tạo", "Thông báo");
                }

            }
        }
        byte[] ArrByte_Anh;
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
            ArrByte_Anh = arrByte;

            pictureBox1.Image = HinhAnh.ByteToImage(arrByte);
            if (txbMa_HocSinh.Text == "")
            {

            }
            else
            {
                EC_HocSinh hs = new BUS_HocSinh().Select_ByPrimaryKey(txbMa_HocSinh.Text);
                hs.Anh = arrByte;
                try
                {
                    new BUS_HocSinh().SuaDuLieu(hs);
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
