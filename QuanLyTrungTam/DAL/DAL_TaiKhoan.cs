using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyTrungTam.Entity;

namespace QuanLyTrungTam.DAL
{
    public class DAL_TaiKhoan
    {
        KetNoi_DB con = new KetNoi_DB();
        //Hàm thêm dữ liệu
        public bool ThemDuLieu(EC_TaiKhoan ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[TaiKhoan_Insert]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@ID", ec.ID);
                cmd.Parameters.AddWithValue("@TenDangNhap", ec.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", ec.MatKhau);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi thêm mon học");
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }

        }
        //Ham sua
        public bool SuaDuLieu(EC_TaiKhoan ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[TaiKhoan_Update]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@ID", ec.ID);
                cmd.Parameters.AddWithValue("@TenDangNhap", ec.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", ec.MatKhau);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi sua mon học", e);
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }

        }

        //Hàm xóa
        public bool XoaDuLieu(string ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[TaiKhoan_Delete_By_ID]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi xóa: " + e.ToString());
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }
        }

        public List<EC_TaiKhoan> SelectAll()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[TaiKhoan_Select_All]";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {

                con.MoKetNoi();
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                return PopulateObjectsFromReader(dataReader);

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy tất cả mon học: ", ex);
            }
            finally
            {
                con.DongKetNoi();
                sqlCommand.Dispose();
            }
        }
        public EC_TaiKhoan Select_ByPrimaryKey(string ID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[TaiKhoan_Select_By_ID]";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {

                con.MoKetNoi();
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                sqlCommand.Parameters.AddWithValue("@ID", ID);

                IDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    EC_TaiKhoan businessObject = new EC_TaiKhoan();

                    PopulateBusinessObjectFromReader(businessObject, dataReader);

                    return businessObject;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy tất cả mon học: ", ex);
            }
            finally
            {
                con.DongKetNoi();
                sqlCommand.Dispose();
            }
        }
        //Hàm lấy dữ liệu
        public List<EC_TaiKhoan> SelectByFields(string TenDangNhap, string MatKhau)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "[dbo].[TaiKhoan_Select_By_Field]";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            // Use connection object of base class
            con.MoKetNoi();

            try
            {
                sqlCommand.Connection = con.connect;
                sqlCommand.Parameters.Add(new SqlParameter("@TenDangNhap", TenDangNhap));
                sqlCommand.Parameters.Add(new SqlParameter("@MatKhau", MatKhau));

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                return PopulateObjectsFromReader(dataReader);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy mon học", ex);
            }
            finally
            {
                con.DongKetNoi();
                sqlCommand.Dispose();
            }
        }

        /// <summary>
        /// Populate business object from data reader
        /// </summary>
        /// <param name="businessObject">business object</param>
        /// <param name="dataReader">data reader</param>
        internal void PopulateBusinessObjectFromReader(EC_TaiKhoan businessObject, IDataReader dataReader)
        {


            businessObject.ID = dataReader.GetString(dataReader.GetOrdinal(EC_TaiKhoan.TaiKhoanFields.ID.ToString()));

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_TaiKhoan.TaiKhoanFields.TenDangNhap.ToString())))
            {
                businessObject.TenDangNhap = dataReader.GetString(dataReader.GetOrdinal(EC_TaiKhoan.TaiKhoanFields.TenDangNhap.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_TaiKhoan.TaiKhoanFields.MatKhau.ToString())))
            {
                businessObject.MatKhau = dataReader.GetString(dataReader.GetOrdinal(EC_TaiKhoan.TaiKhoanFields.MatKhau.ToString()));
            }

        }

        /// <summary>
        /// Populate business objects from the data reader
        /// </summary>
        /// <param name="dataReader">data reader</param>
        /// <returns>list of SinhVien</returns>
        internal List<EC_TaiKhoan> PopulateObjectsFromReader(IDataReader dataReader)
        {

            List<EC_TaiKhoan> list = new List<EC_TaiKhoan>();

            while (dataReader.Read())
            {
                EC_TaiKhoan businessObject = new EC_TaiKhoan();
                PopulateBusinessObjectFromReader(businessObject, dataReader);
                list.Add(businessObject);
            }
            return list;

        }
    }
}
