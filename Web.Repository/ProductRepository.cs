using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
 
using System.Data.SqlClient;

namespace Web.Repository
{
    public class ProductRepository
    {
        public DataTable RetriveTopProduct()
        {
            string query = "select *from Products p join ProductGroups pg on p.ProductGroupID=pg.ProductGroupId where pg.IsDefault=1";
            Cconnect connect = new Cconnect();

            SqlCommand command = new SqlCommand(query, connect.GetConnection());
            SqlDataAdapter adt = new SqlDataAdapter(command);
            DataTable tb = new DataTable();
            adt.Fill(tb);
            connect.CloseConnection();
            return tb;
        }
    }
}
