using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOWrappers.Extensions;

namespace ADOWrappers
{
    public sealed class GenericSqlDataTable : IGenericSqlDataTable
    {
        /// <summary>
        /// Generic Function that can take an sql query or store procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="executeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public async Task<List<T>> ExecuteReaderAsync<T>(string connectionString, string executeCommand, CommandType commandType, SqlParameter[]? sqlParameters = null) where T : class, new()
        {
            var newListObject = new List<T>();

            using (var conn = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = GetSqlCommand(conn, executeCommand, commandType, sqlParameters))
                {
                    await conn.OpenAsync();
                    using (var dataTable = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable dt = new();
                        await Task.Run(() => dataTable.Fill(dt));
                        if (dt.Rows.Count > 0)
                        {
                            newListObject = dt.ConvertDataTable<T>();
                        }
                    }
                }
            }

            return newListObject;
        }
        /// <summary>
        /// Generic Function that can take an sql query or store procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="executeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public List<T> ExecuteReader<T>(string connectionString, string executeCommand, CommandType commandType, SqlParameter[]? sqlParameters = null) where T : class, new()
        {
            var newListObject = new List<T>();

            using (var conn = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = GetSqlCommand(conn, executeCommand, commandType, sqlParameters))
                {

                    conn.Open();
                    using (var dataTable = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable dt = new();
                        dataTable.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            newListObject = dt.ConvertDataTable<T>();
                        }
                    }
                }
            }

            return newListObject;
        }

        private SqlCommand GetSqlCommand(SqlConnection connection, string sqlQuery, CommandType commandType, SqlParameter[]? parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);
            sqlCommand.CommandType = commandType;

            if (parameters != null)
            {
                sqlCommand.Parameters.AddRange(parameters);
            }

            return sqlCommand;
        }
    }
}
