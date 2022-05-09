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
    public partial class QuanLy_ThuHocPhi : UserControl
    {
        DataTable data;
        public QuanLy_ThuHocPhi()
        {
            InitializeComponent();
            Load();
        }

        void Load()
        {
            dgHocPhi.Rows.Clear();
            DateTime date = dtThang.Value;
            data = new function().Select_HocPhi_Thang(date);
            int i = 1;

            foreach (DataRow row in data.Rows)
            {
                string DaDong = row["DaDong"].ToString();
                int IntDaDong = 0;
                if (DaDong == "")
                {
                    DaDong = "0";
                }
                else
                {
                    IntDaDong = Int32.Parse(DaDong);
                }
                dgHocPhi.Rows.Add(i.ToString(), row["Ma_HocSinh"].ToString(), row["Ten_HocSinh"].ToString(), row["TongSoTien"].ToString(),
                    DaDong, (int)row["TongSoTien"] - IntDaDong);
                i++;
            }
        }
        private void dgLichHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data == null)
            {
                return;
            }
            if (e.RowIndex == -1)
            {
                return;
            }
            string Ma_HocSinh = dgHocPhi.Rows[e.RowIndex].Cells["Ma"].Value.ToString();
            EC_HocSinh HocSinh = new BUS_HocSinh().Select_ByPrimaryKey(Ma_HocSinh);
            txbMa_HocSinh.Text = Ma_HocSinh;
            txbTen_HocSinh.Text = HocSinh.Ten_HocSinh;
            txbSDT.Text = HocSinh.SDT;
            txbEmail.Text = HocSinh.Email;

            DateTime ThoiGian = dtThang.Value;
            function ft = new function();
            DataTable SoBuoiHoc_Thang = ft.SoBuoiHoc_Thang(Ma_HocSinh, dtThang.Value.Year);
            foreach (DataRow row in SoBuoiHoc_Thang.Rows)
            {
                if ((int)row["Thang"] == ThoiGian.Month)
                {
                    txbTongSoBuoi.Text = row["TongSoBuoi"].ToString();
                    txbDiHoc.Text = row["SoBuoi_DiHoc"].ToString();
                    txbVang.Text = row["SoBuoi_Vang"].ToString();
                    if (txbVang.Text == "")
                    {
                        txbVang.Text = "0";
                    }
                }
            }

            DataTable HocPhi_Thang = ft.Select_HocPhi_Thang(dtThang.Value);
            foreach (DataRow row in HocPhi_Thang.Rows)
            {
                if (row["Ma_HocSinh"].ToString() == Ma_HocSinh)
                {
                    txbTongHocPhi.Text = row["TongSoTien"].ToString();
                    if (txbTongHocPhi.Text == "")
                    {
                        txbTongHocPhi.Text = "0";
                    }
                    txbDaDong.Text = row["DaDong"].ToString();
                    int DaDong = 0;
                    if (txbDaDong.Text == "")
                    {
                        txbDaDong.Text = "0";
                    }
                    else
                    {
                        DaDong = Int32.Parse(txbTongHocPhi.Text) - Int32.Parse(txbDaDong.Text);
                    }
                    txbChuaDong.Text = DaDong.ToString();
                }
            }

            dgChiTiet.Rows.Clear();
            int Stt = 1;
            DataTable BuoiHoc_Thang = ft.BuoiHoc_Thang(Ma_HocSinh, dtThang.Value);
            foreach (DataRow row in BuoiHoc_Thang.Rows)
            {
                string TrangThai = (bool)row["TrangThai"] == true ? "Đã học" : "Chưa học";
                string DiemDanh = (bool)row["DiemDanh"] == true ? "Đi học" : "Vắng";
                dgChiTiet.Rows.Add(Stt, row["Ma_LopHoc"].ToString(), row["Ma_BuoiHoc"].ToString(), TrangThai, DiemDanh);
                Stt++;
            }
        }

        private void btThangTruoc_Click(object sender, EventArgs e)
        {
            DateTime date = dtThang.Value;
            if (date.Month > 1)
            {
                DateTime date1 = new DateTime(date.Year, date.Month - 1, date.Day);
                dtThang.Value = date1;
            }
            else
            {
                DateTime date1 = new DateTime(date.Year - 1, 12, date.Day);
                dtThang.Value = date1;
            }
            Load();
        }

        private void bbtThangSau_Click(object sender, EventArgs e)
        {
            DateTime date = dtThang.Value;
            if (date.Month < 12)
            {
                DateTime date1 = new DateTime(date.Year, date.Month + 1, date.Day);
                dtThang.Value = date1;
            }
            else
            {
                DateTime date1 = new DateTime(date.Year + 1, 1, date.Day);
                dtThang.Value = date1;
            }
            Load();
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            dgHocPhi.Rows.Clear();
            string text = txbSearch.Text;
            if (text == "")
            {
                return;
            }
            DataTable table = new function().Select_HocPhi_Thang(dtThang.Value);
            DataTable result = table;
            result.Rows.Clear();
            foreach (DataRow row in table.Rows)
            {
                if (row["Ma_HocSinh"].ToString() == text)
                {
                    result.Rows.Add(row);
                }
                if (row["Ten_HocSinh"].ToString() == text)
                {
                    result.Rows.Add(row);
                }
            }

            int i = 1;
            foreach (DataRow row in result.Rows)
            {
                string DaDong = row["DaDong"].ToString();
                int IntDaDong = 0;
                if (DaDong == "")
                {
                    DaDong = "0";
                }
                else
                {
                    IntDaDong = Int32.Parse(DaDong);
                }
                dgHocPhi.Rows.Add(i.ToString(), row["Ma_HocSinh"].ToString(), row["Ten_HocSinh"].ToString(), row["TongSoTien"].ToString(),
                    DaDong, (int)row["TongSoTien"] - IntDaDong);
                i++;
            }
        }

        private void btTatCa_Click(object sender, EventArgs e)
        {
            dgHocPhi.Rows.Clear();
            Load();
        }
    }
}
