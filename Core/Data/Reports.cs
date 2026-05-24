using System.Collections.Generic;
using System.Linq;
using Dapper;
using ReportForge.Entities;

namespace ReportForge.Data
{
    public static class Reports
    {
        private const string TableName = "reporting.Report";

        /// <summary>Lists report templates. Blob/schema columns are excluded to keep the result light.</summary>
        public static List<Report> GetAll()
        {
            using (var conn = Db.Open())
            {
                return conn.Query<Report>(
                    "SELECT Id, Name, Description " +
                    "FROM " + TableName + " ORDER BY Id ASC"
                ).ToList();
            }
        }

        /// <summary>
        /// Lists report templates that already have a saved layout, including
        /// their declared-parameters schema for endpoint catalogs.
        /// </summary>
        public static List<Report> GetRenderable()
        {
            using (var conn = Db.Open())
            {
                return conn.Query<Report>(
                    "SELECT Id, Name, Description, ParametersSchemaJson " +
                    "FROM " + TableName + " " +
                    "WHERE DATALENGTH(TemplateXml) > 0 " +
                    "ORDER BY Id ASC"
                ).ToList();
            }
        }

        /// <summary>Fetches only the TemplateXml blob for a report.</summary>
        public static byte[] GetTemplateXml(int id)
        {
            using (var conn = Db.Open())
            {
                return conn.QuerySingleOrDefault<byte[]>(
                    "SELECT TemplateXml FROM " + TableName + " WHERE Id = @id",
                    new { id }
                );
            }
        }

        public static string GetParametersSchemaJson(int id)
        {
            using (var conn = Db.Open())
            {
                return conn.QuerySingleOrDefault<string>(
                    "SELECT ParametersSchemaJson FROM " + TableName + " WHERE Id = @id",
                    new { id }
                );
            }
        }

        /// <summary>Loads a single report without its TemplateXml blob.</summary>
        public static Report GetById(int id)
        {
            using (var conn = Db.Open())
            {
                return conn.QuerySingleOrDefault<Report>(
                    "SELECT Id, Name, Description, ParametersSchemaJson " +
                    "FROM " + TableName + " WHERE Id = @id",
                    new { id }
                );
            }
        }

        public static int Insert(Report report)
        {
            using (var conn = Db.Open())
            {
                return conn.QuerySingle<int>(
                    "INSERT INTO " + TableName + " (Name, Description) " +
                    "OUTPUT INSERTED.Id " +
                    "VALUES (@Name, @Description)",
                    report
                );
            }
        }

        /// <summary>Updates metadata only. TemplateXml / ParametersSchemaJson are left untouched.</summary>
        public static void Update(Report report)
        {
            using (var conn = Db.Open())
            {
                conn.Execute(
                    "UPDATE " + TableName + " " +
                    "SET Name = @Name, Description = @Description, UpdatedAt = SYSUTCDATETIME() " +
                    "WHERE Id = @Id",
                    report
                );
            }
        }

        public static int Duplicate(int sourceId)
        {
            var source = GetById(sourceId);
            if (source == null) return 0;

            var copy = new Report
            {
                Name = source.Name + " (copy)",
                Description = source.Description
            };

            var newId = Insert(copy);

            var templateBytes = GetTemplateXml(sourceId);
            if (templateBytes != null && templateBytes.Length > 0)
                SaveTemplateXml(newId, templateBytes, source.ParametersSchemaJson);

            return newId;
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

        public static void SaveTemplateXml(int id, byte[] bytes, string parametersSchemaJson)
        {
            using (var conn = Db.Open())
            {
                conn.Execute(
                    "UPDATE " + TableName + " " +
                    "SET TemplateXml = @bytes, ParametersSchemaJson = @parametersSchemaJson, UpdatedAt = SYSUTCDATETIME() " +
                    "WHERE Id = @id",
                    new { id, bytes, parametersSchemaJson }
                );
            }
        }
    }
}
