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
    public partial class QuanLyTaiKhoan : UserControl
    {
        private bool XacNhan = false;
        public QuanLyTaiKhoan()
        {
            InitializeComponent();
            LoadForm();
        }
        void LoadForm()
        {
            List<EC_TaiKhoan> listTaiKhoan = new BUS_TaiKhoan().SelectAll();
            foreach (EC_TaiKhoan i in listTaiKhoan)
            {
                if (new BUS_GiaoVien().SelectByFields("ID", i.ID).Count > 0)
                {
                    dgDanhsach.Rows.Add(i.ID, i.TenDangNhap, "Giáo viên");
                }
                else if (new BUS_HocSinh().SelectByFields("ID", i.ID).Count > 0)
                {
                    dgDanhsach.Rows.Add(i.ID, i.TenDangNhap, "Học sinh");
                }
                else
                {
                    dgDanhsach.Rows.Add(i.ID, i.TenDangNhap, "Quản lý");
                }
            }
            btLuu.Text = "Thêm";
            lbThongBao.Visible = false;
            pnXacNhan.Visible = false;
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string ID = txbID1.Text;
            dgDanhsach.DataSource = new BUS_TaiKhoan().SelectByMa(ID);

        }

        private void dgDanhsach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            string ID = dgDanhsach.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            string TenDangNhap = dgDanhsach.Rows[e.RowIndex].Cells["TenDangNhap"].Value.ToString();
            txbID.Text = ID;
            txbTenDN.Text = TenDangNhap;
            btLuu.Text = "Thêm";
            txbMatKhau.Enabled = false;

            BUS_GiaoVien busGv = new BUS_GiaoVien();
            BUS_HocSinh busHs = new BUS_HocSinh();
            BUS_QuanLyTrungTam busQl = new BUS_QuanLyTrungTam();

            if (busQl.Select_BYPrimaryKey(ID).Count > 0)
            {
                cbLoai.SelectedIndex = 0;
                txbMa.Text = "";
            }
            else if (busHs.SelectByFields("ID", ID).Count != 0)
            {
                cbLoai.SelectedIndex = 2;
                txbMa.Text = busHs.SelectByFields("ID", ID)[0].Ma_HocSinh;
            }
            else if (busGv.SelectByFields("ID", ID).Count != 0)
            {
                cbLoai.SelectedIndex = 1;
                txbMa.Text = busGv.SelectByFields("ID", ID)[0].Ma_GiaoVien;
            }
            else
            {
                return;
            }
            txbID.Enabled = txbMa.Enabled = txbTenDN.Enabled = cbLoai.Enabled = false;
        }

        string TaoID()
        {
            List<EC_TaiKhoan> tb = new BUS_TaiKhoan().SelectAll();
            string old_id = tb[0].ID;
            string old_number = old_id.Substring(2);
            string new_id = (Convert.ToInt32(old_number) + 1).ToString();
            return new_id;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            if (XacNhan == false)
            {
                lbThongBao.Visible = true;
                pnXacNhan.Visible = true;
                return;
            }
            if (btLuu.Text == "Thêm")
            {
                txbMatKhau.Enabled = true;
                btLuu.Text = "Lưu";
            }
            else if (btLuu.Text == "Lưu")
            {
                string ID = TaoID();
                string MatKhau = txbMatKhau.Text;
                if (MatKhau == "")
                {
                    return;
                }
                EC_TaiKhoan ecTK = new EC_TaiKhoan(ID, txbTenDN.Text, Hash.getHashString(MatKhau));
                new BUS_TaiKhoan().ThemDuLieu(ecTK);
                if (cbLoai.SelectedIndex == 0)
                {
                    EC_QuanLyTrungTam ecQl = new EC_QuanLyTrungTam();
                    ecQl.ID = ID;
                    new BUS_QuanLyTrungTam().ThemDuLieu(ecQl);
                }
                else if (cbLoai.SelectedIndex == 1)
                {
                    BUS_GiaoVien busGv = new BUS_GiaoVien();
                    if (busGv.Select_ByPrimaryKey(txbMa.Text) == null)
                    {
                        MessageBox.Show("Không tồn tại giáo viên");
                        return;
                    }
                    busGv.ThemID(txbMa.Text, ID);
                }
                else if (cbLoai.SelectedIndex == 2)
                {
                    BUS_HocSinh busHS = new BUS_HocSinh();
                    if (busHS.Select_ByPrimaryKey(txbMa.Text) == null)
                    {
                        MessageBox.Show("Không tồn tại học sinh");
                        return;
                    }
                    busHS.ThemID(txbMa.Text, ID);
                }
                btLuu.Text = "Thêm";
            }
            else
            {

            }
            LoadForm();
        }

        private void cbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoai.SelectedIndex == 0)
            {
                txbMa.Enabled = false;
            }
            else
            {
                txbMa.Enabled = true;
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (XacNhan == false)
            {
                lbThongBao.Visible = true;
                pnXacNhan.Visible = true;
                return;
            }
            string ID = txbID.Text;
            if (ID == "")
            {
                return;
            }
            txbMatKhau.Enabled = true;
            EC_TaiKhoan TaiKhoan = new BUS_TaiKhoan().SelectByMa(ID);
            string MatKhau = txbMatKhau.Text;
            if (MatKhau == "")
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu!", "Thông báo");
                return;
            }
            TaiKhoan.MatKhau = Hash.getHashString(MatKhau);
            try
            {
                new BUS_TaiKhoan().SuaDuLieu(TaiKhoan);
                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo");
            }
            catch
            {

            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (XacNhan == false)
            {
                lbThongBao.Visible = true;
                pnXacNhan.Visible = true;
                return;
            }
            string ID = txbID.Text;
            if (ID == "")
            {
                return;
            }

            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa tài khoản?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                try
                {
                    new BUS_TaiKhoan().XoaDuLieu(ID);
                    MessageBox.Show("Xóa thành công tài khoản", "Thông báo");
                }
                catch
                {
                    MessageBox.Show("Xóa không thành công", "Thông báo");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (XacNhan == true)
            {
                XacNhan = false;
            }
        }

        private void btXacNhan_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            List<EC_TaiKhoan> listTaiKhoan = new BUS_TaiKhoan().SelectByFields(txbTenDangNhap_XN.Text, Hash.getHashString(txbMatKhau_XN.Text));
            if (listTaiKhoan.Count == 0)
            {
                MessageBox.Show("Xác nhận thất bại.");
                return;
            }
            else
            {
                MessageBox.Show("Xác nhận thành công", "Thông báo");
                XacNhan = true;
                txbMatKhau.Enabled = true;
                pnXacNhan.Visible = false;
                lbThongBao.Visible = false;
                timer1.Start();
            }
        }
    }
}
