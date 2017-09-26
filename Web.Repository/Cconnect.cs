using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Linq;
namespace Web.Repository
{
    public class Cconnect
    {
        private  string strConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionCTm"].ConnectionString;
        /// <summary>
        /// Global SQL server connection
        /// </summary>
        public   SqlConnection connection;

        public   SqlConnection GetConnection()
        {
            if (connection == null) { connection = new SqlConnection(strConStr); }
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            return connection;
        }
        public void CloseConnection()
        {
            connection.Close();
        }

        #region DB Access Functions

        //public static void ExeCuteNonquery(SqlCommand cmd)
        //{
        //    try
        //    {
        //        SqlConnection conn = GetConnection();
        //        cmd.Connection = conn;
        //        cmd.ExecuteNonQuery();
        //    }
        //    finally
        //    {
        //    }
        //}

        //public static void ExeCuteNonquery(string sql)
        //{
        //    try
        //    {
        //        SqlConnection conn = GetConnection();
        //        var cmd = new SqlCommand(sql, conn);
        //        cmd.ExecuteNonQuery();
        //    }
        //    finally
        //    {
        //    }
        //}

        //public SqlDataReader ExecuteReader(string sql)
        //{
        //    try
        //    {
        //        var cmd = new SqlCommand(sql, GetConnection());
        //        return cmd.ExecuteReader();
        //    }
        //    finally
        //    {
        //    }
        //}

        //public string ExecuteScalar(string sql)
        //{
        //    try
        //    {
        //        SqlConnection conn = GetConnection();
        //        var cmd = new SqlCommand(sql, conn);
        //        return cmd.ExecuteScalar().ToString();
        //    }
        //    finally
        //    {
        //    }
        //}

        //public object ExecuteScalar(SqlCommand cmd)
        //{
        //    try
        //    {
        //        SqlConnection conn = GetConnection();
        //        cmd.Connection = conn;
        //        return cmd.ExecuteScalar();
        //    }
        //    finally
        //    {
        //    }
        //}

        //public int DBSize()
        //{
        //    using (var cmd = new SqlCommand("select sum(size) * 8 * 1024 from sysfiles"))
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        return (int)ExecuteScalar(cmd);
        //    }
        //}

        //public bool CheckConnect()
        //{
        //    var cmd = new SqlCommand("select getdate()");
        //    if (GetTable(cmd).Rows.Count > 0)
        //        return true;
        //    return false;
        //}
        #endregion

        #region connect to excel
        public static System.Data.DataSet LoadExcelToDataSet(string sheetname, string sourFile)
        {
            string selectExcel = "select * from [" + sheetname + "$]";
            OleDbConnection oleCon = GetConToExcel(sourFile);
            var oleCom = new OleDbCommand(selectExcel, oleCon);
            var oleDa = new OleDbDataAdapter(oleCom);
            System.Data.DataSet ds = new System.Data.DataSet();
            oleDa.Fill(ds);
            oleCon.Close();
            return ds;
        }

        public static OleDbConnection GetConToExcel(string sourFile)
        {

            string constring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sourFile + @";Extended Properties=""Excel 8.0;HDR=YES;""";
            string connctString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourFile + @";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1""";//Office2007
            var oleCon = new OleDbConnection(constring);
            try
            {

                oleCon.Open();
            }
            catch (Exception)
            {

                oleCon = new OleDbConnection(connctString);
                oleCon.Open();
            }

            return oleCon;
        }
        #endregion
    }
}
