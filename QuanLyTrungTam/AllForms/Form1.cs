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
    public partial class Form1 : Form
    {
        private string ID;
        private int Quyen;
        static string Ma_GiaoVien;
        LopDay usLopDay;

        public Form1(int _quyen, string _ID)
        {
            Quyen = _quyen;
            ID = _ID;
            InitializeComponent();
            btDaHocXong.Visible = btDangHoc.Visible = false;
            PhanQuyen();
            Main.Controls.Add(new TrangChu());
        }

        void PhanQuyen()
        {
            if (Quyen == 1)
            {
                btDaHocXong.Visible = btDangHoc.Visible = btHocPhi.Visible = btLichHoc.Visible = btLopHoc.Visible
                    = btLichDay.Visible = btLopDangDay.Visible = false;
                btGmail.Visible = true;
                QlLopHoc = new QuanLyLopHoc();
                QlLopHoc.btChiTiet.Click += BtChiTiet_Click2;
            }
            else if (Quyen == 2)
            {
                btDaHocXong.Visible = btDangHoc.Visible = btHocPhi.Visible = btLichHoc.Visible = btLopHoc.Visible
                    = btQl_ThuHocPhi.Visible = btQuanLyGiaoVien.Visible = btQuanLyHocSinh.Visible = btQuanLyLopHoc.Visible = btQuanLyTaiKhoan.Visible = false;
                btGmail.Visible = true;
                EC_GiaoVien GiaoVien = new BUS_GiaoVien().SelectByFields("ID", ID)[0];
                btAcc.Text = GiaoVien.Ten_GiaoVien;
                if (GiaoVien.Anh != null)
                {
                    picAvt.Image = HinhAnh.ByteToImage(GiaoVien.Anh);
                }
                Ma_GiaoVien = GiaoVien.Ma_GiaoVien;
                usLopDay = new LopDay(Ma_GiaoVien);
                usLopDay.dgLopHoc.CellClick += DgLopHoc_CellClick;
            }
            else if (Quyen == 3)
            {
                btLichDay.Visible = btLopDangDay.Visible
                    = btQuanLyGiaoVien.Visible = btQuanLyHocSinh.Visible = btQl_ThuHocPhi.Visible = btQuanLyLopHoc.Visible = btQuanLyTaiKhoan.Visible = false;
                btGmail.Visible = false;
                EC_HocSinh HocSinh = new BUS_HocSinh().SelectByFields("ID", ID)[0];
                btAcc.Text = HocSinh.Ten_HocSinh;
                if (HocSinh.Anh != null)
                {
                    picAvt.Image = HinhAnh.ByteToImage(HocSinh.Anh);
                }
                LH_Tuan = new LichHoc_Tuan(ID);
            }
            else
            {
                btDaHocXong.Visible = btDangHoc.Visible = btHocPhi.Visible = btLichHoc.Visible = btLopHoc.Visible
                    = btQuanLyGiaoVien.Visible = btQuanLyHocSinh.Visible = btQuanLyLopHoc.Visible = btQuanLyTaiKhoan.Visible
                    = btLichDay.Visible = btLopDangDay.Visible = false;
            }
        }

        private void BtChiTiet_Click2(object sender, EventArgs e)
        {
            Ma_LopHoc_1 = QlLopHoc.txbMa_LopHoc.Text;
            if (Ma_LopHoc_1 == "")
            {
                MessageBox.Show("Vui lòng chọn lớp học", "Thông báo");
                return;
            }
            QlLichHoc = new QuanLyLichHoc(Ma_LopHoc_1, 1);
            Main.Controls.Clear();
            Main.Controls.Add(QlLichHoc);
            QlLichHoc.btChiTiet.Click += BtChiTiet_Click;
            QlLichHoc.btQuayLai.Click += BtQuayLai_Click;
            QlLichHoc.btDanhSachLop.Click += BtDanhSachLop_Click;
        }

        QuanLyLichHoc QlLichHoc;
        string Ma_LopHoc_1;
        DanhSachLop DanhSachLop;

        private void BtDanhSachLop_Click(object sender, EventArgs e)
        {
            DanhSachLop = new DanhSachLop(Ma_LopHoc_1);
            Main.Controls.Clear();
            Main.Controls.Add(DanhSachLop);
            DanhSachLop.btQuayLai.Click += BtQuayLai_Click4;
        }

        private void BtQuayLai_Click4(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(QlLichHoc);
        }

        private void BtQuayLai_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(QlLopHoc);
        }

        private void BtChiTiet_Click(object sender, EventArgs e)
        {
            DiemDanh dd = new DiemDanh(Ma_LopHoc_1);
            Main.Controls.Clear();
            Main.Controls.Add(dd);
            dd.btQuayLai.Click += BtQuayLai_Click2;
        }

        private void BtQuayLai_Click2(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(QlLichHoc);
        }

        private void DgLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DiemDanh dd = new DiemDanh(usLopDay.dgLopHoc.Rows[e.RowIndex].Cells["Ma_LopHoc"].Value.ToString());
            dd.btQuayLai.Click += BtQuayLai_Click1;
            Main.Controls.Clear();
            Main.Controls.Add(dd);
        }

        private void BtQuayLai_Click1(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(usLopDay);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool sttBtLopHoc = false;

        private void btLopHoc_Click(object sender, EventArgs e)
        {
            if (sttBtLopHoc == true)
            {
                btDaHocXong.Visible = btDangHoc.Visible = false;
                sttBtLopHoc = false;
            }
            else
            {
                btDaHocXong.Visible = btDangHoc.Visible = true;
                sttBtLopHoc = true;
            }
        }

        LichHoc_Tuan LH_Tuan;

        private void btLichHoc_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            LH_Tuan = new LichHoc_Tuan(ID);
            Main.Controls.Add(LH_Tuan);
            LH_Tuan.btChiTiet.Click += BtChiTiet_Click1;

        }

        private void BtChiTiet_Click1(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            QuanLyLichHoc QL_LichHoc = new QuanLyLichHoc(LH_Tuan.Ma_LopHoc, 0);
            QL_LichHoc.btQuayLai.Click += BtQuayLai_Click3;
            QL_LichHoc.btChiTiet.Visible = false;
            Main.Controls.Add(QL_LichHoc);
        }

        private void BtQuayLai_Click3(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(LH_Tuan);
        }

        private void btDangHoc_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(new LopHoc_DangHoc("HS00001", 1));
        }

        private void btDaHocXong_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(new LopHoc_DangHoc("HS00001", 2));
        }

        private void btHocPhi_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(new HocPhi_Thang(ID));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(new QuanLyTaiKhoan());
        }
        QuanLyLopHoc QlLopHoc;
        private void button5_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(QlLopHoc);
        }
        QuanLyGiaoVien QlGiaoVien;
        private void button6_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            QlGiaoVien = new QuanLyGiaoVien();
            QlGiaoVien.btChiTiet.Click += BtChiTiet_Click3;
            Main.Controls.Add(QlGiaoVien);
        }

        private void BtChiTiet_Click3(object sender, EventArgs e)
        {
            if (QlGiaoVien.txbMa_GiaoVien.Text == "")
            {
                MessageBox.Show("Vui lòng chọn giáo viên để xem chi tiết!", "Thông báo");
                return;
            }
            Main.Controls.Clear();
            ChiTiet_GiaoVien QLDay = new ChiTiet_GiaoVien(QlGiaoVien.txbMa_GiaoVien.Text);
            QLDay.btQuayLai.Click += BtQuayLai_Click5;
            Main.Controls.Add(QLDay);
        }

        private void BtQuayLai_Click5(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(QlGiaoVien);
        }
        QuanLyHocSinh QlHocSinh;
        private void button7_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            QlHocSinh = new QuanLyHocSinh();
            QlHocSinh.btChiTiet.Click += BtChiTiet_Click4;
            Main.Controls.Add(QlHocSinh);
        }

        private void BtChiTiet_Click4(object sender, EventArgs e)
        {
            if (QlHocSinh.txbMa_HocSinh.Text == "")
            {
                MessageBox.Show("Vui lòng chọn học sinh để xem chi tiết", "Thông báo");
                return;
            }
            Main.Controls.Clear();
            ChiTiet_HocSinh CT_HS = new ChiTiet_HocSinh(QlHocSinh.txbMa_HocSinh.Text);
            CT_HS.btQuayLai.Click += BtQuayLai_Click6;
            Main.Controls.Add(CT_HS);
        }

        private void BtQuayLai_Click6(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(QlHocSinh);
        }

        private void btLichDay_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(new LichDay_Tuan(ID));
        }
        ThongTinTaiKhoan tttk;
        private void btAcc_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            tttk = new ThongTinTaiKhoan(Quyen, ID);
            tttk.picAvt.DoubleClick += PicAvt_DoubleClick;
            Main.Controls.Add(tttk);
        }

        private void PicAvt_DoubleClick(object sender, EventArgs e)
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

            picAvt.Image = HinhAnh.ByteToImage(arrByte);
            tttk.picAvt.Image = HinhAnh.ByteToImage(arrByte);
            if (Quyen == 1)
            {
                EC_QuanLyTrungTam ql = new BUS_QuanLyTrungTam().Select_BYPrimaryKey(ID)[0];
                ql.Anh = arrByte;
                try
                {
                    new BUS_QuanLyTrungTam().SuaDuLieu(ql);
                    MessageBox.Show("Lưu ảnh thành công", "Thông báo");
                }
                catch
                {
                    MessageBox.Show("Lưu ảnh không thành công", "Thông báo");
                }
            }
            else if (Quyen == 2)
            {
                EC_GiaoVien hs = new BUS_GiaoVien().SelectByFields("ID", ID)[0];
                hs.Anh = arrByte;
                try
                {
                    new BUS_GiaoVien().SuaDuLieu(hs);
                    MessageBox.Show("Lưu ảnh thành công", "Thông báo");
                }
                catch
                {
                    MessageBox.Show("Lưu ảnh không thành công", "Thông báo");
                }
            }
            else if (Quyen == 3)
            {
                EC_HocSinh hs = new BUS_HocSinh().SelectByFields("ID", ID)[0];
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

        void ShowLogin()
        {
            DangNhap dn = new DangNhap();
            dn.ShowDialog();
        }

        private void btDangXuat_Click_1(object sender, EventArgs e)
        {
            Thread thr = new Thread(ShowLogin);
            thr.Start();
            this.Close();
        }

        private void btLopDangDay_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(usLopDay);
        }

        private void btQl_ThuHocPhi_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(new QuanLy_ThuHocPhi());
        }

        private void btGmail_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(new Mail());
        }

        private void btTrangChu_Click(object sender, EventArgs e)
        {
            Main.Controls.Clear();
            Main.Controls.Add(new TrangChu());
        }
    }
}
