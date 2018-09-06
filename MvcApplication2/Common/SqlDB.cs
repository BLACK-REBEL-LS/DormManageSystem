using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace Common
{
    public class SqlDB
    {
        private static SqlConnection sqlConn;
        private static SqlCommand sqlcom;
        private static SqlDataAdapter dataAdapter;
        private static string connectionString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString.ToString();
        
        public SqlDB()
        {
            //connectionString = constr;
            sqlConn = new SqlConnection(connectionString);
            sqlcom = new SqlCommand();
            sqlcom.Connection = sqlConn;
        }

        ///<summary>
        ///对连接执行T-SQl语句，并返回受影响的行数，错误返回-1
        ///</summary>
        public static int executeSql(string sqlstr, SqlParameter[] para = null)
        {
            int i = -1;
            using(SqlConnection con=new SqlConnection(connectionString))
            {
                using (SqlCommand cmd=new SqlCommand(sqlstr,con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (para != null)
                    {
                        cmd.Parameters.AddRange(para);
                    }
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
            }
            return i;
        }

        ///<summary>
        ///根据查询语句，返回DataTable，如果执行的是各种更新语句则没用
        ///</summary>
        public static DataTable FillDt(string sqlstr, SqlParameter[] para=null)
        {
            DataTable dt = new DataTable();
            using(SqlConnection conn=new SqlConnection(connectionString))
            {
                using(SqlDataAdapter da=new SqlDataAdapter(sqlstr,conn))
                {
                    da.SelectCommand.CommandType = CommandType.Text;
                    if (para != null)
                    {
                        foreach (SqlParameter item in para)
                        {
                            da.SelectCommand.Parameters.Add(item);
                        }
                    }
                    
                    da.Fill(dt);
                }
            }
            return dt;
        }

    }
}
