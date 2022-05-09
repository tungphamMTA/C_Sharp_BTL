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
    public partial class DangNhap : Form
    {
        int PhanQuyen;
        private string ID;
        public DangNhap()
        {
            InitializeComponent();
            txbMatKhau.UseSystemPasswordChar = true;
        }

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            BUS_TaiKhoan busTK = new BUS_TaiKhoan();
            List<EC_TaiKhoan> ListTaiKhoan = busTK.SelectByFields(txbTenDangNhap.Text, Hash.getHashString(txbMatKhau.Text));
            EC_TaiKhoan TaiKhoan = new EC_TaiKhoan();
            if (ListTaiKhoan.Count == 0)
            {
                DialogResult result = MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    txbMatKhau.Text = "";
                }
                return;
            }
            TaiKhoan = ListTaiKhoan[0];
            ID = TaiKhoan.ID;

            BUS_QuanLyTrungTam busQL = new BUS_QuanLyTrungTam();
            BUS_GiaoVien busGV = new BUS_GiaoVien();
            BUS_HocSinh busHS = new BUS_HocSinh();

            if (busQL.Select_BYPrimaryKey(ID).Count > 0)
            {
                PhanQuyen = 1; // admin co quyen la 1
            }
            else if (busGV.SelectByFields("ID", ID).Count != 0)
            {
                PhanQuyen = 2; //Giao vien co quyen la 2
            }
            else if (busHS.SelectByFields("ID", ID).Count != 0)
            {
                PhanQuyen = 3;
            }
            else
            {
                PhanQuyen = 0;
            }

            MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Thread thr = new Thread(NewForm);
            thr.Start();
            this.Close();
        }
        void NewForm()
        {
            Form1 form = new Form1(PhanQuyen, ID);
            form.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txbMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txbMatKhau.UseSystemPasswordChar = true;
            }
        }
    }
}
