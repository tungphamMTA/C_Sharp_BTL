using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrungTam.AllForms
{
    static class Hash
    {
        public static string getHashString(string Pass)
        {
            string str_md5 = "";
            byte[] arr = System.Text.Encoding.UTF8.GetBytes(Pass);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            arr = my_md5.ComputeHash(arr);

            foreach (byte i in arr)
            {
                str_md5 += i.ToString("X2");
            }

            return str_md5;
        }
    }
}
