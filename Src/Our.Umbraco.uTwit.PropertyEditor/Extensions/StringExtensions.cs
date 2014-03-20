using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Our.Umbraco.uTwit.Extensions
{
    internal static class StringExtensions
    {
        internal static string Decrypt(this string input)
        {
            input = input.Replace(" ", "+");

            var key = "!$uTwit$!";
            var byKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            var IV = new byte[] { 18, 52, 86, 120, 144, 171, 205, 239 };
            var inputByteArray = new byte[input.Length];

            var des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(input);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}