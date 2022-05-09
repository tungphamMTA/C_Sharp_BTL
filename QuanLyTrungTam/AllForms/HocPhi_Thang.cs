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
    public partial class HocPhi_Thang : UserControl
    {
        function ft = new function();
        string Ma_HocSinh;
        string ID;
        public HocPhi_Thang(string _ID)
        {
            InitializeComponent();
            ID = _ID;
            Ma_HocSinh = new BUS_HocSinh().SelectByFields("ID", ID)[0].Ma_HocSinh;
            Load();
        }

        void Load()
        {
            dtThang.Value = DateTime.Now;
            LayDuLieu();
        }

        void LayDuLieu()
        {
            DateTime Thang = dtThang.Value;
            DataTable HocPhi_Thang = ft.TongTien_Thang(Ma_HocSinh);
            dgHocPhi.Rows.Clear();
            foreach (DataRow row in HocPhi_Thang.Rows)
            {
                string ChuaDong = row["TongTien_ChuaDong"].ToString();
                int IntChuaDong = 0;
                if (ChuaDong == "")
                {
                    IntChuaDong = 0;
                }
                else
                {
                    IntChuaDong = Convert.ToInt32(ChuaDong);
                }

                dgHocPhi.Rows.Add(row["Thang"].ToString() + "/" + row["Nam"].ToString(), row["TongTien"].ToString(), (int)row["TongTien"] - IntChuaDong, IntChuaDong);
            }
        }

        private void dgHocPhi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ThoiGian = dgHocPhi.Rows[e.RowIndex].Cells["Thang"].Value.ToString();
            int index = 0;
            for (int i = 0; i < ThoiGian.Length; i++)
            {
                if (ThoiGian[i] == '/')
                {
                    index = i;
                }
            }
            int Thang = Int32.Parse(ThoiGian.Substring(0, index));
            int Nam = Int32.Parse(ThoiGian.Substring(index + 1));
            dtThang.Value = new DateTime(Nam, Thang, 1);
            DataTable SoBuoiHoc_Thang = ft.SoBuoiHoc_Thang(Ma_HocSinh, dtThang.Value.Year);
            foreach (DataRow row in SoBuoiHoc_Thang.Rows)
            {
                if ((int)row["Thang"] == Thang)
                {
                    txbSoBuoiHoc.Text = row["TongSoBuoi"].ToString();
                    txbBuoi_DaHoc.Text = row["SoBuoi_DiHoc"].ToString();
                    txbBuoi_Nghi.Text = row["SoBuoi_Vang"].ToString();
                    if (txbBuoi_Nghi.Text == "")
                    {
                        txbBuoi_Nghi.Text = "0";
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
                    if (txbDaDong.Text == "")
                    {
                        txbDaDong.Text = "0";
                    }
                    txbChuaDong.Text = row["ChuaDong"].ToString();
                    if (txbChuaDong.Text == "")
                    {
                        txbChuaDong.Text = "0";
                    }
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

        private void dgChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string Ma_LopHoc = dgChiTiet.Rows[e.RowIndex].Cells["Ma_LopHoc"].Value.ToString();
            string Ma_BuoiHoc = dgChiTiet.Rows[e.RowIndex].Cells["Ma_BuoiHoc"].Value.ToString();

            EC_LopHoc LopHoc = new BUS_LopHoc().Select_ByPrimaryKey(Ma_LopHoc);
            EC_LichHoc LichHoc = new BUS_LichHoc().Select_ByPrimaryKey(Ma_BuoiHoc);
            EC_BuoiHoc_HocSinh BHHS = new BUS_BuoiHoc_HocSinh().SelectByMa(Ma_BuoiHoc, Ma_HocSinh);

            txbMa_LopHoc.Text = LopHoc.Ma_LopHoc;
            txbMa_BuoiHoc.Text = LichHoc.Ma_BuoiHoc;
            txbGiaoVien.Text = new BUS_GiaoVien().Select_ByPrimaryKey(LopHoc.Ma_GiaoVien).Ten_GiaoVien;
            txbMonHoc.Text = new BUS_MonHoc().Select_ByPrimaryKey(LopHoc.Ma_MonHoc).Ten_MonHoc;
            txbSoTien_Buoi.Text = BHHS.SoTien_Buoi.ToString();
            dtNgayHoc.Value = LichHoc.NgayHoc;
        }

        private void dtThang_ValueChanged(object sender, EventArgs e)
        {
            LayDuLieu();
        }
    }
}
