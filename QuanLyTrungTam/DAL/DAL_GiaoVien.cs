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
    public class DAL_GiaoVien
    {
        KetNoi_DB con = new KetNoi_DB();
        //Hàm thêm dữ liệu
        public void ThemDuLieu(EC_GiaoVien ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[GiaoVien_Insert]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_GiaoVien", ec.Ma_GiaoVien);
                cmd.Parameters.AddWithValue("@Ten_GiaoVien", ec.Ten_GiaoVien);
                cmd.Parameters.AddWithValue("@NgaySinh", ec.NgaySinh);
                int GioiTinh = ec.GioiTinh == true ? 1 : 0;
                cmd.Parameters.AddWithValue("@GioiTinh", GioiTinh);
                cmd.Parameters.AddWithValue("@SDT", ec.SDT);
                cmd.Parameters.AddWithValue("@DiaChi", ec.DiaChi);
                cmd.Parameters.AddWithValue("@Email", ec.Email);
                cmd.Parameters.AddWithValue("@TrinhDo", ec.TrinhDo);
                cmd.Parameters.AddWithValue("@ID", ec.ID);
                cmd.Parameters.Add(new SqlParameter("@Anh", SqlDbType.Image)).Value = ec.Anh;

                cmd.ExecuteNonQuery();
                return;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi thêm giao vien");
            }
            finally
            {
                con.DongKetNoi();
                cmd.Dispose();
            }

        }
        //Hàm sửa
        public void SuaDuLieu(EC_GiaoVien ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[GiaoVien_Update]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_GiaoVien", ec.Ma_GiaoVien);
                cmd.Parameters.AddWithValue("@Ten_GiaoVien", ec.Ten_GiaoVien);
                cmd.Parameters.AddWithValue("@NgaySinh", ec.NgaySinh);
                int GioiTinh = ec.GioiTinh == true ? 1 : 0;
                cmd.Parameters.AddWithValue("@GioiTinh", GioiTinh);
                cmd.Parameters.AddWithValue("@DiaChi", ec.DiaChi);
                cmd.Parameters.AddWithValue("@SDT", ec.SDT);
                cmd.Parameters.AddWithValue("@Email", ec.Email);
                cmd.Parameters.AddWithValue("@TrinhDo", ec.TrinhDo);
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
        public void XoaDuLieu(EC_GiaoVien ec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.[GiaoVien_Delete]";
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.MoKetNoi();
                // Use connection object of base class
                cmd.Connection = con.connect;

                cmd.Parameters.AddWithValue("@Ma_GiaoVien", ec.Ma_GiaoVien);

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

        public List<EC_GiaoVien> SelectAll()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[GiaoVien_Select_All]";
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
                throw new Exception("Lỗi khi lấy tất cả giao vien", ex);
            }
            finally
            {
                con.DongKetNoi();
                sqlCommand.Dispose();
            }
        }

        public EC_GiaoVien Select_ByPrimaryKey(string Ma_GiaoVien)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "dbo.[GiaoVien_Select_By_Ma]";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {

                sqlCommand.Parameters.AddWithValue("@Ma_GiaoVien", Ma_GiaoVien);

                con.MoKetNoi();
                // Use connection object of base class
                sqlCommand.Connection = con.connect;

                IDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.Read())
                {
                    EC_GiaoVien businessObject = new EC_GiaoVien();

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
        public List<EC_GiaoVien> SelectByFields(string filedname, object value)
        {
            try
            {
                con.MoKetNoi();
                string query = "exec GiaoVien_Select_By_Field '" + filedname + "', N'" + value + "'";
                SqlCommand sqlCommand = new SqlCommand(query, con.connect);
                // Use connection object of base class

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
        internal void PopulateBusinessObjectFromReader(EC_GiaoVien businessObject, IDataReader dataReader)
        {


            businessObject.Ma_GiaoVien = dataReader.GetString(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.Ma_GiaoVien.ToString()));

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.Ten_GiaoVien.ToString())))
            {
                businessObject.Ten_GiaoVien = dataReader.GetString(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.Ten_GiaoVien.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.NgaySinh.ToString())))
            {
                businessObject.NgaySinh = dataReader.GetDateTime(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.NgaySinh.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.GioiTinh.ToString())))
            {
                businessObject.GioiTinh = dataReader.GetBoolean(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.GioiTinh.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.DiaChi.ToString())))
            {
                businessObject.DiaChi = dataReader.GetString(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.DiaChi.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.SDT.ToString())))
            {
                businessObject.SDT = dataReader.GetString(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.SDT.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.Email.ToString())))
            {
                businessObject.Email = dataReader.GetString(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.Email.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.TrinhDo.ToString())))
            {
                businessObject.TrinhDo = dataReader.GetString(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.TrinhDo.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.ID.ToString())))
            {
                businessObject.ID = dataReader.GetString(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.ID.ToString()));
            }

            if (!dataReader.IsDBNull(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.Anh.ToString())))
            {

                int length = (int)dataReader.GetBytes(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.Anh.ToString()), 0, null, 0, 0);
                businessObject.Anh = new byte[length];
                dataReader.GetBytes(dataReader.GetOrdinal(EC_GiaoVien.GiaoVienFields.Anh.ToString()), 0, businessObject.Anh, 0, length);
            }
        }

        /// <summary>
        /// Populate business objects from the data reader
        /// </summary>
        /// <param name="dataReader">data reader</param>
        /// <returns>list of SinhVien</returns>
        internal List<EC_GiaoVien> PopulateObjectsFromReader(IDataReader dataReader)
        {

            List<EC_GiaoVien> list = new List<EC_GiaoVien>();

            while (dataReader.Read())
            {
                EC_GiaoVien businessObject = new EC_GiaoVien();
                PopulateBusinessObjectFromReader(businessObject, dataReader);
                list.Add(businessObject);
            }
            return list;

        }

        //Lay lich day trong 1 ngay cua giao vien
        public DataTable LayLichDay_Ngay(string Ma_GiaoVien, DateTime date)
        {
            string ngayhoc = string.Format("{0:yyyy-MM-dd hh:mm:ss}", date);
            DataTable result = con.GetData(@"SELECT KipHoc, LopHoc.Ma_LopHoc, Ten_MonHoc from LopHoc, MonHoc, LichHoc 
                                            where MonHoc.Ma_MonHoc=LopHoc.Ma_MonHoc and LichHoc.Ma_LopHoc=LopHoc.Ma_LopHoc
                                            and LopHoc.Ma_GiaoVien='" + Ma_GiaoVien + "' and LichHoc.NgayHoc='" + ngayhoc + "'");
            return result;
        }

        public string getMa_GiaoVien(string ID)
        {
            DataTable table = con.GetData("Select Ma_GiaoVien from GiaoVien where ID='" + ID + "'");
            if (table.Rows.Count == 0)
            {
                return null;
            }
            string result = table.Rows[0]["Ma_GiaoVien"].ToString();
            return result;
        }

        public int ThemID(string Ma_GiaoVien, string ID)
        {
            return con.ThucThiCauLenhSQL("UPDATE GiaoVien SET ID='" + ID + "' WHERE Ma_GiaoVien='" + Ma_GiaoVien + "'");
        }
    }
}
