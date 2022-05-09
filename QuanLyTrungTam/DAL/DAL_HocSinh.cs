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
    public class DAL_HocSinh
    {
        KetNoi_DB con = new KetNoi_DB();
        //Hàm thêm dữ liệu
        public void ThemDuLieu(EC_HocSinh ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[HocSinh_Insert]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_HocSinh", ec.Ma_HocSinh);
                cmd.Parameters.AddWithValue("@Ten_HocSinh", ec.Ten_HocSinh);
                cmd.Parameters.AddWithValue("@NgaySinh", ec.NgaySinh);
                int GioiTinh = ec.GioiTinh == true ? 1 : 0;
                cmd.Parameters.AddWithValue("@GioiTinh", GioiTinh);
                cmd.Parameters.AddWithValue("@DiaChi", ec.DiaChi);
                cmd.Parameters.AddWithValue("@SDT", ec.SDT);
                cmd.Parameters.AddWithValue("@Email", ec.Email);
                cmd.Parameters.AddWithValue("@Lop", ec.Lop);
                cmd.Parameters.AddWithValue("@ID", ec.ID);
                cmd.Parameters.AddWithValue("@Anh", ec.Anh);

                cmd.ExecuteNonQuery();
                return;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi thêm hoc sinh");
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }

        }
        //Hàm sửa
        public void SuaDuLieu(EC_HocSinh ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[HocSinh_Update]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_HocSinh", ec.Ma_HocSinh);
                cmd.Parameters.AddWithValue("@Ten_HocSinh", ec.Ten_HocSinh);
                cmd.Parameters.AddWithValue("@NgaySinh", ec.NgaySinh);
                int GioiTinh = ec.GioiTinh == true ? 1 : 0;
                cmd.Parameters.AddWithValue("@GioiTinh", GioiTinh);
                cmd.Parameters.AddWithValue("@DiaChi", ec.DiaChi);
                cmd.Parameters.AddWithValue("@SDT", ec.SDT);
                cmd.Parameters.AddWithValue("@Email", ec.Email);
                cmd.Parameters.AddWithValue("@Lop", ec.Lop);
                cmd.Parameters.AddWithValue("@ID", ec.ID);
                cmd.Parameters.Add(new SqlParameter("@Anh", SqlDbType.Image)).Value = ec.Anh;

                cmd.ExecuteNonQuery();
                return;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi sửa giao vien: " + e.ToString());
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }
        }
        //Hàm xóa
        public void XoaDuLieu(EC_HocSinh ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[HocSinh_Delete]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_GiaoVien", ec.Ma_HocSinh);

                cmd.ExecuteNonQuery();
                return;
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

        public List<EC_HocSinh> SelectAll()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[HocSinh_Select_All]";
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

        public EC_HocSinh Select_ByPrimaryKey(string Ma_HocSinh)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[HocSinh_Select_By_Ma]";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {

                sqlCommand.Parameters.AddWithValue("@Ma_HocSinh", Ma_HocSinh);
                con.MoKetNoi();
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    EC_HocSinh businessObject = new EC_HocSinh();

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
        public List<EC_HocSinh> SelectByFields(string filedname, object value)
        {
            try
            {
                con.MoKetNoi();
                string query = "exec HocSinh_Select_By_Field '" + filedname + "', N'" + value + "'";
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
        internal void PopulateBusinessObjectFromReader(EC_HocSinh businessObject, IDataReader dataReader)
        {


            businessObject.Ma_HocSinh = dataReader.GetString(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.Ma_HocSinh.ToString()));

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.Ten_HocSinh.ToString())))
            {
                businessObject.Ten_HocSinh = dataReader.GetString(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.Ten_HocSinh.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.NgaySinh.ToString())))
            {
                businessObject.NgaySinh = dataReader.GetDateTime(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.NgaySinh.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.GioiTinh.ToString())))
            {
                businessObject.GioiTinh = dataReader.GetBoolean(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.GioiTinh.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.DiaChi.ToString())))
            {
                businessObject.DiaChi = dataReader.GetString(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.DiaChi.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.SDT.ToString())))
            {
                businessObject.SDT = dataReader.GetString(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.SDT.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.Email.ToString())))
            {
                businessObject.Email = dataReader.GetString(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.Email.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.Lop.ToString())))
            {
                businessObject.Lop = dataReader.GetInt32(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.Lop.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.ID.ToString())))
            {
                businessObject.ID = dataReader.GetString(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.ID.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.Anh.ToString())))
            {

                int length = (int)dataReader.GetBytes(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.Anh.ToString()), 0, null, 0, 0);
                businessObject.Anh = new byte[length];
                dataReader.GetBytes(dataReader.GetOrdinal(EC_HocSinh.HocSinhFields.Anh.ToString()), 0, businessObject.Anh, 0, length);
            }
        }

        /// <summary>
        /// Populate business objects from the data reader
        /// </summary>
        /// <param name="dataReader">data reader</param>
        /// <returns>list of SinhVien</returns>
        internal List<EC_HocSinh> PopulateObjectsFromReader(IDataReader dataReader)
        {

            List<EC_HocSinh> list = new List<EC_HocSinh>();

            while (dataReader.Read())
            {
                EC_HocSinh businessObject = new EC_HocSinh();
                PopulateBusinessObjectFromReader(businessObject, dataReader);
                list.Add(businessObject);
            }
            return list;

        }
        public int ThemID(string Ma_HocSinh, string ID)
        {
            return con.ThucThiCauLenhSQL("UPDATE HocSinh SET ID='" + ID + "' WHERE Ma_HocSinh='" + Ma_HocSinh + "'");
        }

    }
}
