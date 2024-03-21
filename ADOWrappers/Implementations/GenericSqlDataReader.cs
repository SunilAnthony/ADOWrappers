using System.Data;
using System.Data.SqlClient;
using ADOWrappers.Contacts;
using ADOWrappers.Extensions;

namespace ADOWrappers.Implementations
{
    public sealed class GenericSqlDataReader : IGenericSqlDataReader
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
                    using (var dataReader = await sqlCommand.ExecuteReaderAsync(CommandBehavior.Default))
                    {
                        if (dataReader.HasRows)
                        {
                            while (await dataReader.ReadAsync())
                            {
                                var newObject = new T();
                                dataReader.MapDataToObject(newObject);
                                newListObject.Add(newObject);
                            }
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
                    using (var dataReader = sqlCommand.ExecuteReader(CommandBehavior.Default))
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                var newObject = new T();
                                dataReader.MapDataToObject(newObject);
                                newListObject.Add(newObject);
                            }
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
