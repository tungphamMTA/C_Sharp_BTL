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
    public class DAL_PhanCong_Day
    {
        KetNoi_DB con = new KetNoi_DB();
        //Hàm thêm dữ liệu
        public bool ThemDuLieu(EC_PhanCong_Day ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[PhanCong_Day_Insert]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_MonHoc", ec.Ma_MonHoc);
                cmd.Parameters.AddWithValue("@Ma_GiaoVien", ec.Ma_GiaoVien);

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
        //Hàm xóa
        public bool XoaDuLieu(EC_PhanCong_Day ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[MonHoc_Delete_By_Ma]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_MonHoc", ec.Ma_MonHoc);
                cmd.Parameters.AddWithValue("@Ma_GiaoVien", ec.Ma_GiaoVien);

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

        public List<EC_PhanCong_Day> SelectAll()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[PhanCong_Day_Select_All]";
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
        //Hàm lấy dữ liệu
        public List<EC_PhanCong_Day> SelectByFields(string filedname, object value)
        {
            try
            {
                con.MoKetNoi();
                string query = "exec PhanCong_Day_Select_By_Field '" + filedname + "', N'" + value + "'";
                SqlCommand sqlCommand = new SqlCommand(query, con.connect);
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                return PopulateObjectsFromReader(dataReader);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy mon học", ex);
            }
            finally
            {
                con.DongKetNoi();
            }
        }

        /// <summary>
        /// Populate business object from data reader
        /// </summary>
        /// <param name="businessObject">business object</param>
        /// <param name="dataReader">data reader</param>
        internal void PopulateBusinessObjectFromReader(EC_PhanCong_Day businessObject, IDataReader dataReader)
        {


            businessObject.Ma_GiaoVien = dataReader.GetString(dataReader.GetOrdinal(EC_PhanCong_Day.PhanCong_DayFields.Ma_GiaoVien.ToString()));

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_PhanCong_Day.PhanCong_DayFields.Ma_MonHoc.ToString())))
            {
                businessObject.Ma_MonHoc = dataReader.GetString(dataReader.GetOrdinal(EC_PhanCong_Day.PhanCong_DayFields.Ma_MonHoc.ToString()));
            }

        }

        /// <summary>
        /// Populate business objects from the data reader
        /// </summary>
        /// <param name="dataReader">data reader</param>
        /// <returns>list of SinhVien</returns>
        internal List<EC_PhanCong_Day> PopulateObjectsFromReader(IDataReader dataReader)
        {

            List<EC_PhanCong_Day> list = new List<EC_PhanCong_Day>();

            while (dataReader.Read())
            {
                EC_PhanCong_Day businessObject = new EC_PhanCong_Day();
                PopulateBusinessObjectFromReader(businessObject, dataReader);
                list.Add(businessObject);
            }
            return list;

        }
    }
}
