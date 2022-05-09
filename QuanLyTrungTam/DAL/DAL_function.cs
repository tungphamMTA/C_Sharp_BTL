using QuanLyTrungTam.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.DAL
{
    public class DAL_function
    {
        private string connect = @"Data Source=LAPTOP-3EAD9E6J\SQLEXPRESS;Initial Catalog=QuanLyTrungTam;Integrated Security=True";
        public DataTable LayLichhoc_Ngay(string Ma_HocSinh, DateTime dt)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from LichHoc_Ngay(@Ma_HocSinh, @Date)", con);

                cmd.Parameters.AddWithValue("@Ma_HocSinh", Ma_HocSinh);
                string thoigian = string.Format("{0:yyyy-MM-dd hh:mm:ss}", dt);
                cmd.Parameters.AddWithValue("@Date", dt);

                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adt.Fill(table);
                con.Close();
                return table;
            }
        }
        public DataTable LayLopHoc_DangHoc(string Ma_HocSinh)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from LopHoc_DangHoc(@Ma_HocSinh)", con);

                cmd.Parameters.AddWithValue("@Ma_HocSinh", Ma_HocSinh);

                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adt.Fill(table);
                con.Close();
                return table;
            }
        }
        public DataTable LayLopHoc_DaHoc(string Ma_HocSinh)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from LopHoc_DaHoc(@Ma_HocSinh)", con);

                cmd.Parameters.AddWithValue("@Ma_HocSinh", Ma_HocSinh);

                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adt.Fill(table);
                con.Close();
                return table;
            }
        }
        public DataTable TongTien_Thang(string Ma_HocSinh)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from dbo.HocPhi_Thang(@Ma_HocSinh)", con);
                cmd.Parameters.AddWithValue("@Ma_HocSinh", Ma_HocSinh);

                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adt.Fill(table);
                con.Close();
                return table;
            }
        }

        public DataTable LopHoc_Select_Manager(EC_LopHoc ecLop, EC_MonHoc ecMon)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from LopHoc_Select_Manager(@Ten_MonHoc, @Lop, @Ma_GiaoVien, @TrinhDo, @TongSoBuoi)", con);

                cmd.Parameters.AddWithValue("@Ten_MonHoc", ecMon.Ten_MonHoc);
                cmd.Parameters.Add(new SqlParameter("@Lop", SqlDbType.Int)).Value = ecMon.Lop;
                cmd.Parameters.AddWithValue("@Ma_GiaoVien", ecLop.Ma_GiaoVien);
                cmd.Parameters.AddWithValue("@TrinhDo", ecLop.TrinhDo);
                cmd.Parameters.Add(new SqlParameter("@TongSoBuoi", SqlDbType.Int)).Value = ecLop.SoBuoi;

                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                adt.Fill(tb);
                con.Close();
                return tb;
            }
        }
        public DataTable DSHocSinh_Lop(string Ma_LopHoc)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from dbo.HocSinh_in_Lop(@Ma_LopHoc)", conn);
                cmd.Parameters.AddWithValue("@Ma_LopHoc", Ma_LopHoc);

                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                adt.Fill(tb);
                conn.Close();
                return tb;
            }
        }

        public void Them_HocSinh_Lop(string Ma_LopHoc, string Ma_HocSinh)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.HocSinh_Them_Lop @Ma_LopHoc, @Ma_HocSinh", conn);

                cmd.Parameters.AddWithValue("@Ma_LopHoc", Ma_LopHoc);
                cmd.Parameters.AddWithValue("@Ma_HocSinh", Ma_HocSinh);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataTable Select_HocPhi_Thang(DateTime date)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [dbo].[Select_HocPhi_Thang] (@Date)", conn);

                cmd.Parameters.AddWithValue("@Date", date);

                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable result = new DataTable();
                adt.Fill(result);
                conn.Close();
                return result;
            }
        }

        public void DongTien_Thang(string Ma_HocSinh, DateTime ThoiGian)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("exec dbo.DongTien @Ma_HocSinh, @ThoiGian");

                cmd.Parameters.AddWithValue("@Ma_HocSinh", Ma_HocSinh);
                cmd.Parameters.AddWithValue("@ThoiGian", ThoiGian);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataTable SoBuoiHoc_Thang(string Ma_HocSinh, int Nam)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [dbo].SoBuoiHoc_Thang(@Ma_HocSinh, @Nam)", conn);

                try
                {
                    cmd.Parameters.AddWithValue("@Ma_HocSinh", Ma_HocSinh);
                    cmd.Parameters.AddWithValue("@Nam", Nam);

                    DataTable table = new DataTable();
                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    adt.Fill(table);
                    return table;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    conn.Close();
                    cmd.Dispose();
                }
            }
        }

        public DataTable BuoiHoc_Thang(string Ma_HocSinh, DateTime date)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from [dbo].BuoiHoc_Thang(@Ma_HocSinh, @Date)", conn);

                try
                {
                    cmd.Parameters.AddWithValue("@Ma_HocSinh", Ma_HocSinh);
                    cmd.Parameters.AddWithValue("@Date", date);

                    DataTable table = new DataTable();
                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    adt.Fill(table);
                    return table;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    conn.Close();
                    cmd.Dispose();
                }
            }
        }
    }
}
