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
    public class DAL_LopHoc
    {
        KetNoi_DB con = new KetNoi_DB();
        //Hàm thêm dữ liệu
        public bool ThemDuLieu(EC_LopHoc ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[LopHoc_Insert]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_LopHoc", ec.Ma_LopHoc);
                cmd.Parameters.AddWithValue("@Ma_GiaoVien", ec.Ma_GiaoVien);
                cmd.Parameters.AddWithValue("@Ma_MonHoc", ec.Ma_MonHoc);
                cmd.Parameters.AddWithValue("@TrinhDo", ec.TrinhDo);
                cmd.Parameters.AddWithValue("@TongHocPhi_KhoaHoc", ec.TongHocPhi_KhoaHoc);
                cmd.Parameters.AddWithValue("@SoBuoi", ec.SoBuoi);
                cmd.Parameters.AddWithValue("@Ngay_BatDau", ec.Ngay_BatDau);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi thêm lớp học");
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }

        }
        //Hàm sửa
        public bool SuaDuLieu(EC_LopHoc ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[LopHoc_Update]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_LopHoc", ec.Ma_LopHoc);
                cmd.Parameters.AddWithValue("@Ma_GiaoVien", ec.Ma_GiaoVien);
                cmd.Parameters.AddWithValue("@Ma_MonHoc", ec.Ma_MonHoc);
                cmd.Parameters.AddWithValue("@TrinhDo", ec.TrinhDo);
                cmd.Parameters.AddWithValue("@TongHocPhi_KhoaHoc", ec.TongHocPhi_KhoaHoc);
                cmd.Parameters.AddWithValue("@SoBuoi", ec.SoBuoi);
                cmd.Parameters.AddWithValue("@Ngay_BatDau", ec.Ngay_BatDau);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi sửa lớp học: " + e.ToString());
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }
        }
        //Hàm xóa
        public bool XoaDuLieu(string Ma_LopHoc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[LopHoc_Delete_By_Ma]";
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

        public List<EC_LopHoc> SelectAll()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[LopHoc_Select_All]";
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
                throw new Exception("Lỗi khi lấy tất cả lớp học: ", ex);
            }
            finally
            {
                con.DongKetNoi();
                sqlCommand.Dispose();
            }
        }

        public EC_LopHoc SelectByPrimaryKey(string Ma_LopHoc)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[LopHoc_Select_By_Ma]";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {

                sqlCommand.Parameters.AddWithValue("@Ma_LopHoc", Ma_LopHoc);

                con.MoKetNoi();
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    EC_LopHoc businessObject = new EC_LopHoc();

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
                throw new Exception("Lỗi khi lấy tất cả lớp học.", ex);
            }
            finally
            {
                con.DongKetNoi();
                sqlCommand.Dispose();
            }
        }
        //Hàm lấy dữ liệu
        public List<EC_LopHoc> SelectByFields(string filedname, object value)
        {
            try
            {
                con.MoKetNoi();
                string query = "exec LopHoc_Select_By_Fields '" + filedname + "', N'" + value + "'";
                SqlCommand sqlCommand = new SqlCommand(query, con.connect);
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                return PopulateObjectsFromReader(dataReader);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy lớp học", ex);
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
        internal void PopulateBusinessObjectFromReader(EC_LopHoc businessObject, IDataReader dataReader)
        {


            businessObject.Ma_LopHoc = dataReader.GetString(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.Ma_LopHoc.ToString()));

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.Ma_GiaoVien.ToString())))
            {
                businessObject.Ma_GiaoVien = dataReader.GetString(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.Ma_GiaoVien.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.Ma_MonHoc.ToString())))
            {
                businessObject.Ma_MonHoc = dataReader.GetString(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.Ma_MonHoc.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.Ngay_BatDau.ToString())))
            {
                businessObject.Ngay_BatDau = dataReader.GetDateTime(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.Ngay_BatDau.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.SoBuoi.ToString())))
            {
                businessObject.SoBuoi = dataReader.GetInt32(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.SoBuoi.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.TongHocPhi_KhoaHoc.ToString())))
            {
                businessObject.TongHocPhi_KhoaHoc = dataReader.GetInt32(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.TongHocPhi_KhoaHoc.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.TrinhDo.ToString())))
            {
                businessObject.TrinhDo = dataReader.GetString(dataReader.GetOrdinal(EC_LopHoc.LopHocFields.TrinhDo.ToString()));
            }
        }

        /// <summary>
        /// Populate business objects from the data reader
        /// </summary>
        /// <param name="dataReader">data reader</param>
        /// <returns>list of SinhVien</returns>
        internal List<EC_LopHoc> PopulateObjectsFromReader(IDataReader dataReader)
        {

            List<EC_LopHoc> list = new List<EC_LopHoc>();

            while (dataReader.Read())
            {
                EC_LopHoc businessObject = new EC_LopHoc();
                PopulateBusinessObjectFromReader(businessObject, dataReader);
                list.Add(businessObject);
            }
            return list;

        }
    }
}
