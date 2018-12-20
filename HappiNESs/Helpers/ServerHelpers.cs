using System.Data.SqlClient;

namespace HappiNESs
{
    /// <summary>
    /// Helpers to open and manipulate SQL Server
    /// </summary>
    public static class ServerHelpers
    {
        /// <summary>
        /// Starts a new SQL Server connection
        /// </summary>
        /// <param name="server">The server name</param>
        /// <param name="database">The database name</param>
        /// <returns></returns>
        public static SqlConnection CreateConnection(string server, string database)
        {
            // Construct the Connection String
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
                IntegratedSecurity = true,
            };

            return new SqlConnection(builder.ToString());
        }
    }
}
