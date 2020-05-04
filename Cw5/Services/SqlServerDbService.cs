using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Cw5.Models;

namespace Cw5.Services
{
    public class SqlServerDbService : IStudentsDbService
    {
        private string conString = "Data Source=db-mssql;Initial Catalog=s16859;Integrated Security=True";

        private SqlConnection con;

        public SqlServerDbService()
        {
            con = new SqlConnection(conString);
            con.Open();
        }

        public List<object[]> ExecuteSelect(SqlCommand command)
        {

            List<object[]> resultList = new List<object[]>();

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = command)
            {
                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    com.Connection = con;
                    com.Transaction = tran;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        object[] temp = new object[reader.FieldCount];

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            temp[i] = reader[i];
                        }

                        resultList.Add(temp);
                    }

                    reader.Close();
                }
                catch (SqlException e)
                {
                    tran.Rollback();
                }

                //tran.Commit();
                con.Close();
            }
            return resultList;
        }
        public void ExecuteInsert(SqlCommand command)
        {
            System.Diagnostics.Debug.WriteLine("test1");

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = command)
            {
                con.Open();
                var tran = con.BeginTransaction();

                try
                {

                    com.Connection = con;
                    com.Transaction = tran;

                    com.ExecuteScalar();
                    System.Diagnostics.Debug.WriteLine("test2");
                    //tran.Commit();
                   

                }
                catch (SqlException e)
                {
                    tran.Rollback();
                }

            }
        }

        public SqlConnection GetConnection()
        {
            return con;
        }
    }
}
