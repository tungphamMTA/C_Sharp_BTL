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
    public class DAL_MonHoc
    {
        KetNoi_DB con = new KetNoi_DB();
        //Hàm thêm dữ liệu
        public bool ThemDuLieu(EC_MonHoc ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[MonHoc_Insert]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_MonHoc", ec.Ma_MonHoc);
                cmd.Parameters.AddWithValue("@Ten_MonHoc", ec.Ten_MonHoc);
                cmd.Parameters.AddWithValue("@Lop", ec.Lop);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi thêm mon học");
                return false;
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }

        }
        //Hàm sửa
        public bool SuaDuLieu(EC_MonHoc ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[MonHoc_Update]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_MonHoc", ec.Ma_MonHoc);
                cmd.Parameters.AddWithValue("@Ten_MonHoc", ec.Ten_MonHoc);
                cmd.Parameters.AddWithValue("@Lop", ec.Lop);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi sửa mon học: " + e.ToString());
                return false;
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }
        }
        //Hàm xóa
        public bool XoaDuLieu(string Ma_MonHoc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[MonHoc_Delete_By_Ma]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_MonHoc", Ma_MonHoc);

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

        public List<EC_MonHoc> SelectAll()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[MonHoc_Select_All]";
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

        public EC_MonHoc Select_ByPrimaryKey(string Ma_MonHoc)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[MonHoc_Select_By_Ma]";
            sqlCommand.CommandType = CommandType.StoredProcedure;


            try
            {

                sqlCommand.Parameters.AddWithValue("@Ma_MonHoc", Ma_MonHoc);

                con.MoKetNoi();
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    EC_MonHoc businessObject = new EC_MonHoc();

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
                throw new Exception("Lỗi khi lấy tất cả mon học.", ex);
            }
            finally
            {
                con.DongKetNoi();
                sqlCommand.Dispose();
            }
        }
        //Hàm lấy dữ liệu
        public List<EC_MonHoc> SelectByFields(string filedname, object value)
        {
            try
            {
                con.MoKetNoi();
                string query = "exec MonHoc_Select_By_Fields '" + filedname + "', N'" + value + "'";
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
        internal void PopulateBusinessObjectFromReader(EC_MonHoc businessObject, IDataReader dataReader)
        {


            businessObject.Ma_MonHoc = dataReader.GetString(dataReader.GetOrdinal(EC_MonHoc.MonHocField.Ma_MonHoc.ToString()));

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_MonHoc.MonHocField.Ten_MonHoc.ToString())))
            {
                businessObject.Ten_MonHoc = dataReader.GetString(dataReader.GetOrdinal(EC_MonHoc.MonHocField.Ten_MonHoc.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_MonHoc.MonHocField.Ma_MonHoc.ToString())))
            {
                businessObject.Lop = dataReader.GetInt32(dataReader.GetOrdinal(EC_MonHoc.MonHocField.Lop.ToString()));
            }

        }

        /// <summary>
        /// Populate business objects from the data reader
        /// </summary>
        /// <param name="dataReader">data reader</param>
        /// <returns>list of SinhVien</returns>
        internal List<EC_MonHoc> PopulateObjectsFromReader(IDataReader dataReader)
        {

            List<EC_MonHoc> list = new List<EC_MonHoc>();

            while (dataReader.Read())
            {
                EC_MonHoc businessObject = new EC_MonHoc();
                PopulateBusinessObjectFromReader(businessObject, dataReader);
                list.Add(businessObject);
            }
            return list;

        }
    }
}
