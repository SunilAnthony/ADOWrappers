using System.Data;
using System.Data.SqlClient;

namespace ADOWrappers.Contacts
{
    public interface IGenericSqlDataReader
    {
        /// <summary>
        /// Return a list of T from an ADO.net SQL Data Reader Async
        /// </summary>
        /// <typeparam name="T">Return a list of T: where T is a class</typeparam>
        /// <param name="connectionString"></param>
        /// <param name="executeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        Task<List<T>> ExecuteReaderAsync<T>(string connectionString, string executeCommand, CommandType commandType, SqlParameter[]? sqlParameters = null) where T : class, new();
        /// <summary>
        /// Return a list of T from an ADO.net SQL Data Reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="executeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        List<T> ExecuteReader<T>(string connectionString, string executeCommand, CommandType commandType, SqlParameter[]? sqlParameters = null) where T : class, new();
    }
}