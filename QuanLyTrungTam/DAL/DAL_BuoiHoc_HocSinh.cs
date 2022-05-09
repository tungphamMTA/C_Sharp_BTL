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
    public class DAL_BuoiHoc_HocSinh
    {
        KetNoi_DB con = new KetNoi_DB();
        //Hàm thêm dữ liệu
        public void ThemDuLieu(EC_BuoiHoc_HocSinh ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[BuoiHoc_HocSinh_Insert]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_HocSinh", ec.Ma_HocSinh);
                cmd.Parameters.AddWithValue("@Ma_BuoiHoc", ec.Ma_BuoiHoc);
                cmd.Parameters.AddWithValue("@SoTien_Buoi", ec.SoTien_Buoi);
                cmd.Parameters.AddWithValue("@DiemDanh", ec.DiemDanh);
                cmd.Parameters.AddWithValue("@DanhGia", ec.DanhGia);
                cmd.Parameters.AddWithValue("@DongTien", ec.DongTien);

                cmd.ExecuteNonQuery();
                return;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi thêm buổi học của học sinh");
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }

        }
        //Hàm sửa
        public void SuaDuLieu(EC_BuoiHoc_HocSinh ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[BuoiHoc_HocSinh_Update]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_HocSinh", ec.Ma_HocSinh);
                cmd.Parameters.AddWithValue("@Ma_BuoiHoc", ec.Ma_BuoiHoc);
                cmd.Parameters.AddWithValue("@SoTien_Buoi", ec.SoTien_Buoi);
                cmd.Parameters.AddWithValue("@DiemDanh", ec.DiemDanh);
                cmd.Parameters.AddWithValue("@DanhGia", ec.DanhGia);
                cmd.Parameters.AddWithValue("@DongTien", ec.DongTien);

                cmd.ExecuteNonQuery();
                return;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi sửa buổi học của học sinh");
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }
        }
        //Hàm xóa
        public void XoaDuLieu(EC_BuoiHoc_HocSinh ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[BuoiHoc_HocSinh_Delete]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_HocSinh", ec.Ma_HocSinh);
                cmd.Parameters.AddWithValue("@Ma_BuoiHoc", ec.Ma_BuoiHoc);

                cmd.ExecuteNonQuery();
                return;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi xóa buổi học của học sinh");
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }
        }

        public List<EC_BuoiHoc_HocSinh> SelectAll()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[BuoiHoc_HocSinh_Select_All]";
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
                throw new Exception("Lỗi khi lấy tất cả buổi học", ex);
            }
            finally
            {
                con.DongKetNoi();
                sqlCommand.Dispose();
            }
        }

        public EC_BuoiHoc_HocSinh Select_ByPrimaryKey(string Ma_BuoiHoc, string Ma_HocSinh)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[BuoiHoc_HocSinh_Select_By_Ma]";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {

                sqlCommand.Parameters.AddWithValue("@Ma_BuoiHoc", Ma_BuoiHoc);
                sqlCommand.Parameters.AddWithValue("@Ma_HocSinh", Ma_HocSinh);

                con.MoKetNoi();
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    EC_BuoiHoc_HocSinh businessObject = new EC_BuoiHoc_HocSinh();

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
        public List<EC_BuoiHoc_HocSinh> SelectByFields(string filedname, object value)
        {

            try
            {
                con.MoKetNoi();
                string query = "exec BuoiHoc_HocSinh_Select_By_Field '" + filedname + "', N'" + value + "'";
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
        internal void PopulateBusinessObjectFromReader(EC_BuoiHoc_HocSinh businessObject, IDataReader dataReader)
        {


            businessObject.Ma_BuoiHoc = dataReader.GetString(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.Ma_BuoiHoc.ToString()));

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.Ma_HocSinh.ToString())))
            {
                businessObject.Ma_HocSinh = dataReader.GetString(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.Ma_HocSinh.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.DiemDanh.ToString())))
            {
                businessObject.DiemDanh = dataReader.GetBoolean(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.DiemDanh.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.DanhGia.ToString())))
            {
                businessObject.DanhGia = dataReader.GetString(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.DanhGia.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.DongTien.ToString())))
            {
                businessObject.DongTien = dataReader.GetBoolean(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.DongTien.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.SoTien_Buoi.ToString())))
            {
                businessObject.SoTien_Buoi = dataReader.GetInt32(dataReader.GetOrdinal(EC_BuoiHoc_HocSinh.BuoiHoc_HocSinh_Fields.SoTien_Buoi.ToString()));
            }
        }

        /// <summary>
        /// Populate business objects from the data reader
        /// </summary>
        /// <param name="dataReader">data reader</param>
        /// <returns>list of SinhVien</returns>
        internal List<EC_BuoiHoc_HocSinh> PopulateObjectsFromReader(IDataReader dataReader)
        {

            List<EC_BuoiHoc_HocSinh> list = new List<EC_BuoiHoc_HocSinh>();

            while (dataReader.Read())
            {
                EC_BuoiHoc_HocSinh businessObject = new EC_BuoiHoc_HocSinh();
                PopulateBusinessObjectFromReader(businessObject, dataReader);
                list.Add(businessObject);
            }
            return list;

        }
    }
}
