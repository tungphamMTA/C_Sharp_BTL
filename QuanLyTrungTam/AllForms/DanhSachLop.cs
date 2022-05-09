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
    public partial class DanhSachLop : UserControl
    {
        string Ma_LopHoc;
        List<EC_HocSinh> listSearch;
        public DanhSachLop(string _Ma_LopHoc)
        {
            InitializeComponent();
            Ma_LopHoc = _Ma_LopHoc;
            Load();
        }

        void Load()
        {
            dgDanhSachLop.Rows.Clear();
            List<EC_LichHoc> listLichHoc = new BUS_LichHoc().SelectByFields("Ma_LopHoc", Ma_LopHoc);
            List<EC_BuoiHoc_HocSinh> listBuoiHoc_HocSinh = new BUS_BuoiHoc_HocSinh().SelectByFields("Ma_BuoiHoc", listLichHoc[0].Ma_BuoiHoc);
            List<EC_HocSinh> listHocSinh = new List<EC_HocSinh>();
            foreach (EC_BuoiHoc_HocSinh i in listBuoiHoc_HocSinh)
            {
                EC_HocSinh hocsinh = new BUS_HocSinh().Select_ByPrimaryKey(i.Ma_HocSinh);
                listHocSinh.Add(hocsinh);
            }

            foreach (EC_HocSinh i in listHocSinh)
            {
                string GioiTinh = i.GioiTinh == true ? "Nam" : "Nữ";
                dgDanhSachLop.Rows.Add(i.Ma_HocSinh, i.Ten_HocSinh, i.NgaySinh.ToShortDateString(), GioiTinh, i.SDT);
            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            dgSearch.Rows.Clear();
            string Ma_HocSinh = txbMa_HocSinh.Text;
            string Ten_HocSinh = txbTen_HocSinh.Text;
            int Lop = 0;
            if (txbLop.Text != "")
            {
                Lop = Int32.Parse(txbLop.Text);
            }
            listSearch = new List<EC_HocSinh>();

            if (Ma_HocSinh != "")
            {
                EC_HocSinh hocsinh = new BUS_HocSinh().Select_ByPrimaryKey(Ma_HocSinh);
                listSearch.Add(hocsinh);
            }

            if (Ten_HocSinh != "")
            {
                List<EC_HocSinh> list = new BUS_HocSinh().SelectByFields("Ten_HocSinh", Ten_HocSinh);
                foreach (EC_HocSinh i in list)
                {
                    if (listSearch.IndexOf(i) == -1)
                    {
                        listSearch.Add(i);
                    }
                }
            }

            if (Lop > 0 && Lop <= 12)
            {
                List<EC_HocSinh> list = new BUS_HocSinh().SelectByFields("Lop", Lop);
                foreach (EC_HocSinh i in list)
                {
                    if (listSearch.IndexOf(i) == -1)
                    {
                        listSearch.Add(i);
                    }
                }
            }
            foreach (EC_HocSinh i in listSearch)
            {
                string GioiTinh = i.GioiTinh == true ? "Nam" : "Nữ";
                dgSearch.Rows.Add(i.Ma_HocSinh, i.Ten_HocSinh, i.NgaySinh.ToShortDateString(), GioiTinh, i.SDT, false);
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgSearch.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Check"].Value.ToString()) == true)
                {
                    new function().Them_HocSinh_Lop(Ma_LopHoc, row.Cells["Ma_HocSinh"].Value.ToString());
                    dgSearch.Rows.RemoveAt(row.Index);
                }
            }
            Load();
        }
    }
}
