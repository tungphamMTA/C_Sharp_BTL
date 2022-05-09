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
    public partial class QuanLyLopHoc : UserControl
    {
        DataTable data = new DataTable();
        function ft = new function();
        public QuanLyLopHoc()
        {
            InitializeComponent();
            LoadForm();
        }

        void LoadForm()
        {
            cbTenMon.Items.Clear();
            cbLop.Items.Clear();
            cbLop.Items.Add("Tất cả");
            for (int j = 1; j <= 12; j++)
            {
                cbLop.Items.Add(j.ToString());
            }

            EC_LopHoc ecLop = new EC_LopHoc();
            EC_MonHoc ecMon = new EC_MonHoc();

            LayDuLieu();

            List<EC_MonHoc> listMonHoc = new BUS_MonHoc().SelectAll();
            List<string> listTen_MonHoc = new List<string>();
            listTen_MonHoc.Add(listMonHoc[0].Ten_MonHoc);
            cbTenMon.Items.Add(listTen_MonHoc[0]);

            foreach (EC_MonHoc MonHoc in listMonHoc)
            {
                if (listTen_MonHoc.IndexOf(MonHoc.Ten_MonHoc) == -1)
                {
                    cbTenMon.Items.Add(MonHoc.Ten_MonHoc);
                }
                listTen_MonHoc.Add(MonHoc.Ten_MonHoc);
            }

            dtNgayBatdau.Value = DateTime.Now;
            cbLop.SelectedIndex = 12;
            cbTenMon.SelectedIndex = 0;
            cbTrinhDo.SelectedIndex = 0;
        }

        void LayDuLieu()
        {
            List<EC_LopHoc> listLopHoc = new BUS_LopHoc().SelectAll();
            int i = 1;
            foreach (EC_LopHoc ec in listLopHoc)
            {
                EC_GiaoVien GiaoVien = new BUS_GiaoVien().Select_ByPrimaryKey(ec.Ma_GiaoVien);
                EC_MonHoc MonHoc = new BUS_MonHoc().Select_ByPrimaryKey(ec.Ma_MonHoc);
                dgLopHoc.Rows.Add(i.ToString(), ec.Ma_LopHoc, ec.Ma_GiaoVien, GiaoVien.Ten_GiaoVien, MonHoc.Ten_MonHoc, MonHoc.Lop);
                i++;
            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            LayDuLieu();
        }

        string TaoMa_LopHoc()
        {
            string result = "";
            List<EC_LopHoc> listLopHoc = new BUS_LopHoc().SelectAll();
            if (listLopHoc.Count == 0)
            {
                return "LH00001";
            }
            string Ma_LopHoc_Cu = listLopHoc[0].Ma_LopHoc;
            foreach (EC_LopHoc LopHoc in listLopHoc)
            {
                if (Convert.ToInt32(LopHoc.Ma_LopHoc.Substring(2)) > Convert.ToInt32(Ma_LopHoc_Cu.Substring(2)))
                {
                    Ma_LopHoc_Cu = LopHoc.Ma_LopHoc;
                }
            }
            string Ma_1 = Ma_LopHoc_Cu.Substring(0, 2);
            result += Ma_1;
            string Ma_2 = Ma_LopHoc_Cu.Substring(2);
            int int_Ma_2 = Convert.ToInt32(Ma_2);
            int int_Ma_2_Moi = int_Ma_2 + 1;
            int x = Ma_1.Length + int_Ma_2_Moi.ToString().Length;
            int y = 7 - x;
            for (int i = 0; i < y; i++)
            {
                result += "0";
            }
            result += int_Ma_2_Moi.ToString();
            return result;
        }
        string TaoMa_BuoiHoc(string Ma_LopHoc_Cu)
        {
            string result = "";
            int x = 2;
            for (int i = 2; i < Ma_LopHoc_Cu.Length; i++)
            {
                if (Ma_LopHoc_Cu[i] != '0')
                {
                    x = i;
                    break;
                }
            }
            string Ma_1 = Ma_LopHoc_Cu.Substring(0, x);
            result += Ma_1;
            string Ma_2 = Ma_LopHoc_Cu.Substring(x);
            int int_Ma_2 = Convert.ToInt32(Ma_2);
            int int_Ma_2_Moi = int_Ma_2 + 1;
            int y = 6 - Ma_1.Length - int_Ma_2_Moi.ToString().Length;
            for (int i = 0; i < y; i++)
            {
                result += "0";
            }
            result += int_Ma_2_Moi.ToString();
            return result;
        }
        string LayMa_BuoiHoc_Cu(List<EC_LichHoc> list)
        {
            if (list.Count == 0)
            {
                return "BH00001";
            }
            string ma = list[0].Ma_BuoiHoc;
            foreach (EC_LichHoc LichHoc in list)
            {
                if (Convert.ToInt32(LichHoc.Ma_BuoiHoc.Substring(2)) > Convert.ToInt32(ma.Substring(2)))
                {
                    ma = LichHoc.Ma_BuoiHoc;
                }
            }
            return ma;
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            if (txbMa_LopHoc.Text != "")
            {
                return;
            }
            string Ma_LopHoc = TaoMa_LopHoc();
            string Ma_MonHoc = txbMa_MonHoc.Text;
            if (Ma_MonHoc == "")
            {
                return;
            }
            string Ma_GiaoVien = "";
            if (dgGiaoVien.Rows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow row in dgGiaoVien.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Check"].Value.ToString()) == true)
                {
                    Ma_GiaoVien = row.Cells["Ma"].Value.ToString();
                }
            }
            if (Ma_GiaoVien == "")
            {
                return;
            }
            //Thêm lớp học
            EC_LopHoc ecLop = new EC_LopHoc(Ma_LopHoc, txbMaGV.Text, Ma_MonHoc, cbTrinhDo.SelectedItem.ToString(), Convert.ToInt32(txbTongHP.Text)
                , Convert.ToInt32(txbSoBuoi.Text), dtNgayBatdau.Value);
            new BUS_LopHoc().ThemDuLieu(ecLop);

            //Thêm các buổi học với thời gian mặc định là ngày hôm nay
            int HocPhi_Buoi = Convert.ToInt32(txbTongHP.Text) / Convert.ToInt32(txbSoBuoi.Text);
            string Ma_BuoiHoc_Cu = LayMa_BuoiHoc_Cu(new BUS_LichHoc().SelectAll());
            for (int i = 0; i < Convert.ToInt32(txbSoBuoi.Text); i++)
            {
                string Ma_BuoiHoc = TaoMa_BuoiHoc(Ma_BuoiHoc_Cu);
                EC_LichHoc ecLich = new EC_LichHoc(Ma_LopHoc, DateTime.Now, 1, Ma_BuoiHoc, "", i + 1, HocPhi_Buoi, false);
                new BUS_LichHoc().ThemDuLieu(ecLich);
                Ma_BuoiHoc_Cu = Ma_BuoiHoc;
            }

            DialogResult result = MessageBox.Show("Thêm thành công lớp học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                txbMaGV.Text = "";
                txbSoBuoi.Text = "";
                txbTongHP.Text = "";
            }
            LoadForm();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public string Ma_LopHoc_2 = "";
        private void dgLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            string Ma_LopHoc = dgLopHoc.Rows[e.RowIndex].Cells["Ma_LopHoc"].Value.ToString();
            Ma_LopHoc_2 = Ma_LopHoc;
            EC_LopHoc LopHoc = new BUS_LopHoc().Select_ByPrimaryKey(Ma_LopHoc);
            EC_MonHoc MonHoc = new BUS_MonHoc().Select_ByPrimaryKey(LopHoc.Ma_MonHoc);
            EC_GiaoVien GiaoVien = new BUS_GiaoVien().Select_ByPrimaryKey(LopHoc.Ma_GiaoVien);

            txbMa_LopHoc.Text = LopHoc.Ma_LopHoc;
            cbTrinhDo.SelectedItem = LopHoc.TrinhDo;
            dtNgayBatdau.Value = LopHoc.Ngay_BatDau;
            txbTongHP.Text = LopHoc.TongHocPhi_KhoaHoc.ToString();
            txbSoBuoi.Text = LopHoc.SoBuoi.ToString();

            txbMa_MonHoc.Text = MonHoc.Ma_MonHoc;
            cbLop.SelectedItem = MonHoc.Lop.ToString();
            cbTenMon.SelectedItem = MonHoc.Ten_MonHoc;

            txbMaGV.Text = GiaoVien.Ma_GiaoVien;
            dgGiaoVien.Rows.Clear();
            string GioiTinh = GiaoVien.GioiTinh == true ? "Nam" : "Nữ";
            dgGiaoVien.Rows.Add(GiaoVien.Ma_GiaoVien, GiaoVien.Ten_GiaoVien, GiaoVien.NgaySinh.ToShortDateString(), GioiTinh, true);
        }

        private void cbTenMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLop.Text != "" || cbLop.Text != "Tất cả")
            {
                try
                {
                    txbMa_MonHoc.Text = getMa_MonHoc(cbTenMon.SelectedItem.ToString(), cbLop.SelectedItem.ToString());
                }
                catch
                {
                    return;
                }
            }
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTenMon.Text != "")
            {
                try
                {
                    txbMa_MonHoc.Text = getMa_MonHoc(cbTenMon.Text.ToString(), cbLop.SelectedItem.ToString());

                }
                catch
                {
                    return;
                }
            }
        }

        string getMa_MonHoc(string TenMon, string Lop)
        {
            string Ma_MonHoc = "";
            List<EC_MonHoc> list1 = new BUS_MonHoc().SelectByFields("Ten_MonHoc", TenMon);
            List<EC_MonHoc> list2 = new BUS_MonHoc().SelectByFields("Lop", Lop);
            foreach (EC_MonHoc i in list1)
            {
                foreach (EC_MonHoc j in list2)
                {
                    if (i.Ma_MonHoc == j.Ma_MonHoc)
                    {
                        Ma_MonHoc = i.Ma_MonHoc;
                    }
                }
            }
            return Ma_MonHoc;
        }

        private void btSearchGiaoVien_Click(object sender, EventArgs e)
        {
            List<EC_GiaoVien> listSearch = new List<EC_GiaoVien>();

            if (txbMaGV.Text != "")
            {
                EC_GiaoVien GiaoVien = new BUS_GiaoVien().Select_ByPrimaryKey(txbMaGV.Text);
                if (GiaoVien != null)
                {
                    listSearch.Add(GiaoVien);
                }
            }

            if (txbTen_GiaoVien.Text != "")
            {
                List<EC_GiaoVien> list = new BUS_GiaoVien().SelectByFields("Ten_GiaoVien", txbTen_GiaoVien.Text);
                if (listSearch == null || listSearch.Count == 0)
                {
                    listSearch = list;
                }
                else
                {
                    foreach (EC_GiaoVien i in list)
                    {
                        if (i.Ma_GiaoVien != listSearch[0].Ma_GiaoVien)
                        {
                            listSearch.Add(i);
                        }
                    }
                }
            }
            dgGiaoVien.Rows.Clear();
            foreach (EC_GiaoVien i in listSearch)
            {
                dgGiaoVien.Rows.Add(i.Ma_GiaoVien, i.Ten_GiaoVien, i.NgaySinh, i.GioiTinh, null);
            }
        }

        private void cbTenMon_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbLop.Text != "" || cbLop.Text != "Tất cả")
            {
                //txbMa_MonHoc.Text = getMa_MonHoc(cbTenMon.SelectedItem.ToString(), cbLop.SelectedItem.ToString());
            }
        }

        private void cbLop_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbTenMon.Text != "")
            {
                //txbMa_MonHoc.Text = getMa_MonHoc(cbTenMon.SelectedItem.ToString(), cbLop.SelectedItem.ToString());
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (txbMa_MonHoc.Text == "")
            {
                MessageBox.Show("Chọn lớp để xóa", "Thông báo");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa lớp học?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                try
                {
                    new BUS_LichHoc().XoaDuLieu_Ma_LopHoc(txbMa_LopHoc.Text);
                    new BUS_LopHoc().XoaDuLieu(txbMa_LopHoc.Text);
                    MessageBox.Show("Xoá thành công", "Thông báo");
                }
                catch
                {
                    MessageBox.Show("Xoá không thành công", "Thông báo");
                }
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            dtNgayBatdau.Value = DateTime.Now;
            txbMa_LopHoc.Text = cbTrinhDo.Text = txbTongHP.Text = txbSoBuoi.Text
                = txbMa_MonHoc.Text = cbLop.Text = cbTenMon.Text = txbMaGV.Text = "";
            dgGiaoVien.Rows.Clear();
        }

        private void btXemGiaoVien_Click(object sender, EventArgs e)
        {
            if (txbMa_MonHoc.Text == "")
            {
                return;
            }
            List<EC_PhanCong_Day> listPC_Day = new BUS_PhanCong_Day().SelectByFields("Ma_MonHoc", txbMa_MonHoc.Text);
            foreach (EC_PhanCong_Day ec in listPC_Day)
            {
                EC_GiaoVien GiaoVien = new BUS_GiaoVien().Select_ByPrimaryKey(ec.Ma_GiaoVien);
                string GioiTinh = GiaoVien.GioiTinh == true ? "Nam" : "Nữ";
                dgGiaoVien.Rows.Add(GiaoVien.Ma_GiaoVien, GiaoVien.Ten_GiaoVien, GiaoVien.NgaySinh.ToShortDateString(), GioiTinh, null);
            }
        }
    }
}
