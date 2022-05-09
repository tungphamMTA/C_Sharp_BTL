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
    public class DAL_QuanLyTrungTam
    {
        KetNoi_DB con = new KetNoi_DB();
        //Hàm thêm dữ liệu
        public void ThemDuLieu(EC_QuanLyTrungTam ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[QuanLyTrungTam_Insert]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@ID", ec.ID);
                cmd.Parameters.AddWithValue("@HoTen", ec.HoTen);
                cmd.Parameters.AddWithValue("LienHe", ec.LienHe);
                cmd.Parameters.Add(new SqlParameter("@Anh", SqlDbType.Image)).Value = ec.Anh;

                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                cmd.Dispose();
                con.DongKetNoi();
            }
        }
        //Hàm sửa
        public void SuaDuLieu(EC_QuanLyTrungTam ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[QuanLyTrungTam_Update]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@ID", ec.ID);
                cmd.Parameters.AddWithValue("@HoTen", ec.HoTen);
                cmd.Parameters.AddWithValue("LienHe", ec.LienHe);
                cmd.Parameters.Add(new SqlParameter("@Anh", SqlDbType.Image)).Value = ec.Anh;

                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                cmd.Dispose();
                con.DongKetNoi();
            }
        }
        //Hàm xóa
        public void XoaDuLieu(EC_QuanLyTrungTam ec)
        {

        }
        //Hàm lấy dữ liệu
        public List<EC_QuanLyTrungTam> Select_ByPrimaryKey(string ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[QuanLyTrungTam_Select_ByPrimaryKey]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@ID", ID);

                IDataReader dataReader = cmd.ExecuteReader();
                return PopulateObjectsFromReader(dataReader);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy lớp học", ex);
            }
            finally
            {
                cmd.Dispose();
                con.DongKetNoi();
            }
        }

        public List<EC_QuanLyTrungTam> Select_All()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[QuanLyTrungTam_SelectAll]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                cmd.Connection = con.connect;

                IDataReader dataReader = cmd.ExecuteReader();
                return PopulateObjectsFromReader(dataReader);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy lớp học", ex);
            }
            finally
            {
                cmd.Dispose();
                con.DongKetNoi();
            }
        }
        internal void PopulateBusinessObjectFromReader(EC_QuanLyTrungTam businessObject, IDataReader dataReader)
        {


            businessObject.ID = dataReader.GetString(dataReader.GetOrdinal(EC_QuanLyTrungTam.QuanLyTrungTam_Fields.ID.ToString()));

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_QuanLyTrungTam.QuanLyTrungTam_Fields.HoTen.ToString())))
            {
                businessObject.HoTen = dataReader.GetString(dataReader.GetOrdinal(EC_QuanLyTrungTam.QuanLyTrungTam_Fields.HoTen.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_QuanLyTrungTam.QuanLyTrungTam_Fields.LienHe.ToString())))
            {
                businessObject.LienHe = dataReader.GetString(dataReader.GetOrdinal(EC_QuanLyTrungTam.QuanLyTrungTam_Fields.LienHe.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_QuanLyTrungTam.QuanLyTrungTam_Fields.Anh.ToString())))
            {
                int length = (int)dataReader.GetBytes(dataReader.GetOrdinal(EC_QuanLyTrungTam.QuanLyTrungTam_Fields.Anh.ToString()), 0, null, 0, 0);
                businessObject.Anh = new byte[length];
                dataReader.GetBytes(dataReader.GetOrdinal(EC_QuanLyTrungTam.QuanLyTrungTam_Fields.Anh.ToString()), 0, businessObject.Anh, 0, length);
            }
        }

        /// <summary>
        /// Populate business objects from the data reader
        /// </summary>
        /// <param name="dataReader">data reader</param>
        /// <returns>list of SinhVien</returns>
        internal List<EC_QuanLyTrungTam> PopulateObjectsFromReader(IDataReader dataReader)
        {

            List<EC_QuanLyTrungTam> list = new List<EC_QuanLyTrungTam>();

            while (dataReader.Read())
            {
                EC_QuanLyTrungTam businessObject = new EC_QuanLyTrungTam();
                PopulateBusinessObjectFromReader(businessObject, dataReader);
                list.Add(businessObject);
            }
            return list;

        }
    }
}
