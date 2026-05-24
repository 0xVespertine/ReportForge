using System.Data;
using System.Data.SqlClient;

namespace ReportForge.Data
{
    /// <summary>
    /// Connection factory. Reads values from mssql.env for now.
    /// </summary>
    internal static class Db
    {
        public static IDbConnection Open()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = EnvFile.Get("MSSQL_HOST"),
                InitialCatalog = EnvFile.Get("MSSQL_DB"),
                UserID = EnvFile.Get("MSSQL_USER"),
                Password = EnvFile.Get("MSSQL_PASSWORD"),
                Encrypt = EnvFile.Get("MSSQL_ENCRYPT").Equals("yes", System.StringComparison.OrdinalIgnoreCase),
                TrustServerCertificate = EnvFile.Get("MSSQL_TRUST_CERT").Equals("yes", System.StringComparison.OrdinalIgnoreCase),
                ConnectTimeout = 10
            };

            var conn = new SqlConnection(builder.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
