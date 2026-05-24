using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ReportForge.Entities;

namespace ReportForge.Data
{
    public static class Sources
    {
        private const string TableName = "reporting.Source";

        /// <summary>Lists source presets. Password is intentionally excluded.</summary>
        public static List<Source> GetAll()
        {
            using (var conn = Db.Open())
            {
                return conn.Query<Source>(
                    "SELECT Id, Name, Server, [Database], Username, " +
                    "       Encrypt, TrustServerCertificate, CreatedAt, UpdatedAt " +
                    "FROM " + TableName + " " +
                    "ORDER BY Id ASC"
                ).ToList();
            }
        }

        /// <summary>Loads one preset, including its password for editing/use.</summary>
        public static Source GetById(int id)
        {
            using (var conn = Db.Open())
            {
                var source = conn.QuerySingleOrDefault<Source>(
                    "SELECT Id, Name, Server, [Database], Username, Password, " +
                    "       Encrypt, TrustServerCertificate, CreatedAt, UpdatedAt " +
                    "FROM " + TableName + " " +
                    "WHERE Id = @id",
                    new { id }
                );

                if (source != null)
                    source.Password = UnprotectPasswordOrPlaintext(source.Password);

                return source;
            }
        }

        public static int Insert(Source source)
        {
            using (var conn = Db.Open())
            {
                return conn.QuerySingle<int>(
                    "INSERT INTO " + TableName + " " +
                    "    (Name, Server, [Database], Username, Password, Encrypt, TrustServerCertificate) " +
                    "OUTPUT INSERTED.Id " +
                    "VALUES " +
                    "    (@Name, @Server, @Database, @Username, @Password, @Encrypt, @TrustServerCertificate)",
                    ToStorage(source)
                );
            }
        }

        public static void Update(Source source)
        {
            using (var conn = Db.Open())
            {
                conn.Execute(
                    "UPDATE " + TableName + " " +
                    "SET Name = @Name, Server = @Server, [Database] = @Database, " +
                    "    Username = @Username, Password = @Password, Encrypt = @Encrypt, " +
                    "    TrustServerCertificate = @TrustServerCertificate, UpdatedAt = SYSUTCDATETIME() " +
                    "WHERE Id = @Id",
                    ToStorage(source)
                );
            }
        }

        public static void Delete(int id)
        {
            using (var conn = Db.Open())
            {
                conn.Execute(
                    "DELETE FROM " + TableName + " WHERE Id = @id",
                    new { id }
                );
            }
        }

        /// <summary>Opens the saved target database once. Throws SqlException when unreachable/invalid.</summary>
        public static void TestConnectivity(Source source)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = source.Server,
                InitialCatalog = source.Database,
                UserID = source.Username,
                Password = source.Password ?? string.Empty,
                Encrypt = source.Encrypt,
                TrustServerCertificate = source.TrustServerCertificate,
                ConnectTimeout = 10
            };

            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
            }
        }

        private static object ToStorage(Source source)
        {
            return new
            {
                source.Id,
                source.Name,
                source.Server,
                source.Database,
                source.Username,
                Password = Secret.Protect(source.Password ?? string.Empty),
                source.Encrypt,
                source.TrustServerCertificate
            };
        }

        private static string UnprotectPasswordOrPlaintext(string storedPassword)
        {
            if (string.IsNullOrEmpty(storedPassword)) return string.Empty;
            return Secret.TryUnprotect(storedPassword, out var plaintext)
                ? plaintext
                : storedPassword;
        }
    }
}
