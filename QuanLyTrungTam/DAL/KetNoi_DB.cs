using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.DAL
{
    class KetNoi_DB
    {
        public SqlConnection connect;
        private static string strConnect = @"Data Source=LAPTOP-3EAD9E6J\SQLEXPRESS;Initial Catalog=QuanLyTrungTam;Integrated Security=True";

        internal void MoKetNoi()
        {
            if (connect == null)
            {
                connect = new SqlConnection(strConnect);
            }
            connect.Open();
        }

        internal void DongKetNoi()
        {
            if (connect != null)
            {
                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
        }

        internal int ThucThiCauLenhSQL(string strSQL)
        {
            int result = 0;
            try
            {
                //MoKetNoi();
                using (SqlConnection con = new SqlConnection(strConnect))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                //DongKetNoi();
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        internal DataTable GetData(string strSQL)
        {
            try
            {
                //MoKetNoi();
                DataTable table = new DataTable();
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(strSQL, conn);
                    adapter.Fill(table);
                    conn.Close();
                }
                // DongKetNoi();
                return table;
            }
            catch
            {
                return null;
            }
        }

        internal string GetValue(string strSQL)
        {
            string temp = null;
            MoKetNoi();
            SqlCommand cmd = new SqlCommand(strSQL, connect);
            SqlDataReader sqldr = cmd.ExecuteReader();
            while (sqldr.Read())
            {
                temp = sqldr[0].ToString();
            }
            DongKetNoi();
            return temp;
        }
    }
}
