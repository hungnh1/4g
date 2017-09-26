using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Repository
{
   
    public class ClearWordRepository
    {
        private static readonly string[] VietnameseSigns = new string[]
                                                               {
                                                                   "aAeEoOuUiIdDyY",
 
        "áàạảãâấầậẩẫăắằặẳẵ",
 
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
 
        "éèẹẻẽêếềệểễ",
 
        "ÉÈẸẺẼÊẾỀỆỂỄ",
 
        "óòọỏõôốồộổỗơớờợởỡ",
 
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
 
        "úùụủũưứừựửữ",
 
        "ÚÙỤỦŨƯỨỪỰỬỮ",
 
        "íìịỉĩ",
 
        "ÍÌỊỈĨ",
 
        "đ",
 
        "Đ",
 
        "ýỳỵỷỹ",
 
        "ÝỲỴỶỸ",

                                                               };
        /// <summary>
        /// Chuyển chuỗi tiếng Việt có dấu thành chuỗi không dấu và kô có chữ hoa
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NoVNeseLower(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            str = str.Replace("?", "");
            str = str.Replace("'", "");
            str = str.Replace("-", "");
            str = str.Replace("/", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace("’", "");
            str = str.Replace(",", "");
            str = str.Replace(",", "");
            str = str.Replace(":", "");
            str = str.Replace(";", "");
            str = str.Replace("!", "");
            str = str.Replace(".", "");
            str = str.Replace("~", "");
            str = str.Replace("`", "");
            str = str.Replace("#", "");
            str = str.Replace("@", "");
            str = str.Replace("$", "");
            str = str.Replace("%", "");
            str = str.Replace("^", "");
            str = str.Replace("&", "");
            str = str.Replace("*", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("=", "");
            str = str.Replace("+", "");
            str = str.Replace("®", "");
            str = str.Replace("  ", " ");

            str = str.Replace("---", "-");
            str = str.Replace("--", "-");
            str = str.Replace("–", "");
            str = str.Replace(" ", "-");
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str.ToLower().Trim();
        }
    }
}
