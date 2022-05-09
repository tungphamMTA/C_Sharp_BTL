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
    public partial class LichHoc_Tuan : UserControl
    {
        private string ID;
        BUS_HocSinh busHocSinh = new BUS_HocSinh();
        public string Ma_HocSinh;
        public string Ma_LopHoc;
        public LichHoc_Tuan(string _ID)
        {
            ID = _ID;
            InitializeComponent();
            EC_HocSinh HocSinh = busHocSinh.SelectByFields("ID", ID)[0];
            Ma_HocSinh = HocSinh.Ma_HocSinh;
            LayLichHoc();
        }
        private void LayLichHoc()
        {
            DateTime dt = dateTimePicker1.Value;
            DateTime monday = LayMonday(dt);

            dataGridView1.Rows.Clear();

            for (int i = 0; i < 6; i++)
            {
                dataGridView1.Rows.Add((i + 1).ToString(), "-----", "-----", "-----", "-----", "-----", "-----", "-----");
            }

            TimeSpan sp = new TimeSpan(1, 0, 0, 0);
            for (int i = 0; i < 7; i++)
            {
                LayLichHoc_Ngay(monday);
                monday += sp;
            }
        }

        private void LayLichHoc_Ngay(DateTime dt)
        {
            int CollumnIndex = (int)dt.DayOfWeek;
            DataTable LichHoc = new function().LayLichHoc_Ngay(Ma_HocSinh, dt);
            if (LichHoc == null || LichHoc.Rows.Count == 0)
            {

            }
            foreach (DataRow row in LichHoc.Rows)
            {
                int Kip = Convert.ToInt32(row["KipHoc"].ToString());

                string strShow = row["Ten_MonHoc"].ToString() + " " + row["Lop"].ToString();

                dataGridView1.Rows[Kip - 1].Cells[CollumnIndex].Value = strShow;
            }
        }

        DateTime LayMonday(DateTime dt)
        {
            TimeSpan sp = new TimeSpan(6, 0, 0, 0);
            TimeSpan sp2 = new TimeSpan(1, 0, 0, 0);
            DateTime dt2 = dt - sp;
            for (int i = 0; i <= 6; i++)
            {
                if (dt2.DayOfWeek.ToString() == "Monday")
                {
                    return dt2;
                }
                dt2 += sp2;
            }
            return dt;
        }
        DateTime LaySunday(DateTime dt)
        {
            TimeSpan sp = new TimeSpan(6, 0, 0, 0);
            TimeSpan sp2 = new TimeSpan(1, 0, 0, 0);
            DateTime dt2 = dt + sp;
            for (int i = 0; i <= 6; i++)
            {
                if (dt2.DayOfWeek.ToString() == "Sunday")
                {
                    return dt2;
                }
                dt2 -= sp2;
            }
            return dt;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LayLichHoc();
        }

        private void right_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(7, 0, 0, 0);
            dateTimePicker1.Value += ts;
            LayLichHoc();
        }

        private void left_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(7, 0, 0, 0);
            dateTimePicker1.Value -= ts;
            LayLichHoc();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txbMa_BuoiHoc.Text = txbMa_LopHoc.Text = txbPhongHoc.Text = txbTen_GiaoVien.Text = txbTen_MonHoc.Text = "";
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int Collumn = dataGridView1.CurrentCell.ColumnIndex;

            string MonHoc = dataGridView1.Rows[rowIndex].Cells[Collumn].Value.ToString();
            if (MonHoc == "-----")
            {
                return;
            }

            TimeSpan sp = new TimeSpan(Collumn - 1, 0, 0, 0, 0);
            dateTimePicker1.Value = LayMonday(dateTimePicker1.Value) + sp;
            DataTable LichHoc = new function().LayLichHoc_Ngay(Ma_HocSinh, dateTimePicker1.Value);
            foreach (DataRow row in LichHoc.Rows)
            {
                if (row["KipHoc"].ToString() == (rowIndex + 1).ToString())
                {
                    Ma_LopHoc = row["Ma_LopHoc"].ToString();
                    txbMa_LopHoc.Text = Ma_LopHoc;
                    txbTen_MonHoc.Text = new BUS_MonHoc().Select_ByPrimaryKey(
                        new BUS_LopHoc().Select_ByPrimaryKey(Ma_LopHoc).Ma_MonHoc).Ten_MonHoc;
                    txbMa_BuoiHoc.Text = row["Ma_BuoiHoc"].ToString();
                    txbPhongHoc.Text = row["PhongHoc"].ToString();
                    txbTen_GiaoVien.Text = row["Ten_GiaoVien"].ToString();
                }
            }
        }
    }
}
