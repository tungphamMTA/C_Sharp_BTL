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
    public class DAL_LichHoc
    {
        KetNoi_DB con = new KetNoi_DB();
        //Hàm thêm dữ liệu
        public bool ThemDuLieu(EC_LichHoc ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[LichHoc_Insert]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_LopHoc", ec.Ma_LopHoc);
                cmd.Parameters.AddWithValue("@NgayHoc", ec.NgayHoc);
                cmd.Parameters.AddWithValue("@KipHoc", ec.KipHoc);
                cmd.Parameters.AddWithValue("@Ma_BuoiHoc", ec.Ma_BuoiHoc);
                cmd.Parameters.AddWithValue("@TongHocPhi_Buoi", ec.TongHocPhi_Buoi);
                cmd.Parameters.AddWithValue("@PhongHoc", ec.PhongHoc);
                cmd.Parameters.AddWithValue("@STT_Buoi", ec.STT_Buoi);
                cmd.Parameters.AddWithValue("@TrangThai", ec.TrangThai);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi thêm buoi hoc");
                return false;
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }

        }
        //Hàm sửa
        public bool SuaDuLieu(EC_LichHoc ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[LichHoc_Update]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_LopHoc", ec.Ma_LopHoc);
                cmd.Parameters.AddWithValue("@NgayHoc", ec.NgayHoc);
                cmd.Parameters.AddWithValue("@KipHoc", ec.KipHoc);
                cmd.Parameters.AddWithValue("@Ma_BuoiHoc", ec.Ma_BuoiHoc);
                cmd.Parameters.AddWithValue("@TongHocPhi_Buoi", ec.TongHocPhi_Buoi);
                cmd.Parameters.AddWithValue("@PhongHoc", ec.PhongHoc);
                cmd.Parameters.AddWithValue("@STT_Buoi", ec.STT_Buoi);
                cmd.Parameters.AddWithValue("@TrangThai", ec.TrangThai);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi sửa lich hoc: " + e.ToString());
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }
        }
        //Hàm xóa
        public bool LichHoc_Delete_By_Ma_BuoiHoc(string Ma_BuoiHoc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[LichHoc_Delete_By_Ma_BuoiHoc]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_BuoiHoc", Ma_BuoiHoc);

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

        public bool LichHoc_Delete_By_Ma_LopHoc(string Ma_LopHoc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[LichHoc_Delete_By_Ma_LopHoc]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_LopHoc", Ma_LopHoc);

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

        public List<EC_LichHoc> SelectAll()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[LichHoc_Select_All]";
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
                throw new Exception("Lỗi khi lấy tất cả hoc sinh: ", ex);
            }
            finally
            {
                con.DongKetNoi();
                sqlCommand.Dispose();
            }
        }

        public EC_LichHoc Select_ByPrimaryKey(string Ma_BuoiHoc)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[LichHoc_Select_By_Ma]";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {

                sqlCommand.Parameters.AddWithValue("@Ma_BuoiHoc", Ma_BuoiHoc);
                con.MoKetNoi();
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    EC_LichHoc businessObject = new EC_LichHoc();

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
                throw new Exception("Lỗi khi lấy tất cả buổi học.", ex);
            }
            finally
            {
                con.DongKetNoi();
                sqlCommand.Dispose();
            }
        }
        //Hàm lấy dữ liệu
        public List<EC_LichHoc> SelectByFields(string filedname, object value)
        {
            try
            {
                con.MoKetNoi();
                string query = "exec LichHoc_Select_By_Field '" + filedname + "', N'" + value + "'";
                SqlCommand sqlCommand = new SqlCommand(query, con.connect);
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                return PopulateObjectsFromReader(dataReader);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy tất cả buổi học", ex);
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
        internal void PopulateBusinessObjectFromReader(EC_LichHoc businessObject, IDataReader dataReader)
        {


            businessObject.Ma_BuoiHoc = dataReader.GetString(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.Ma_BuoiHoc.ToString()));

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.Ma_LopHoc.ToString())))
            {
                businessObject.Ma_LopHoc = dataReader.GetString(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.Ma_LopHoc.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.KipHoc.ToString())))
            {
                businessObject.KipHoc = dataReader.GetInt32(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.KipHoc.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.NgayHoc.ToString())))
            {
                businessObject.NgayHoc = dataReader.GetDateTime(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.NgayHoc.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.PhongHoc.ToString())))
            {
                businessObject.PhongHoc = dataReader.GetString(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.PhongHoc.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.STT_Buoi.ToString())))
            {
                businessObject.STT_Buoi = dataReader.GetInt32(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.STT_Buoi.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.TongHocPhi_Buoi.ToString())))
            {
                businessObject.TongHocPhi_Buoi = dataReader.GetInt32(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.TongHocPhi_Buoi.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.TrangThai.ToString())))
            {
                businessObject.TrangThai = dataReader.GetBoolean(dataReader.GetOrdinal(EC_LichHoc.LichHocFields.TrangThai.ToString()));
            }
        }

        /// <summary>
        /// Populate business objects from the data reader
        /// </summary>
        /// <param name="dataReader">data reader</param>
        /// <returns>list of SinhVien</returns>
        internal List<EC_LichHoc> PopulateObjectsFromReader(IDataReader dataReader)
        {

            List<EC_LichHoc> list = new List<EC_LichHoc>();

            while (dataReader.Read())
            {
                EC_LichHoc businessObject = new EC_LichHoc();
                PopulateBusinessObjectFromReader(businessObject, dataReader);
                list.Add(businessObject);
            }
            return list;

        }

        public void SuaTrangThai(string Ma_BuoiHoc, bool tt)
        {
            string trangthai = tt == true ? "True" : "False";
            con.ThucThiCauLenhSQL("UPDATE LichHoc SET TrangThai='" + trangthai + "' where Ma_BuoiHoc='" + Ma_BuoiHoc + "'");
        }
    }
}
