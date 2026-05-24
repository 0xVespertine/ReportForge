using Dapper;

namespace ReportForge.Data
{
    public static class Schema
    {
        public static void EnsureCreated()
        {
            using (var conn = Db.Open())
            {
                conn.Execute(Sql);
            }
        }

        public const string Sql = @"
IF SCHEMA_ID(N'reporting') IS NULL
    EXEC(N'CREATE SCHEMA reporting');

IF OBJECT_ID(N'reporting.Report', N'U') IS NULL
    EXEC(N'CREATE TABLE reporting.Report (
        Id                   INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_reporting_Report PRIMARY KEY,
        Name                 NVARCHAR(200) NOT NULL,
        Description          NVARCHAR(MAX) NULL,
        TemplateXml          VARBINARY(MAX) NULL,
        ParametersSchemaJson NVARCHAR(MAX) NULL,
        CreatedAt            DATETIME2(0) NOT NULL CONSTRAINT DF_reporting_Report_CreatedAt DEFAULT (SYSUTCDATETIME()),
        UpdatedAt            DATETIME2(0) NULL
    )');

IF OBJECT_ID(N'reporting.Source', N'U') IS NULL
    EXEC(N'CREATE TABLE reporting.Source (
        Id                     INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_reporting_Source PRIMARY KEY,
        Name                   NVARCHAR(200) NOT NULL,
        Server                 NVARCHAR(255) NOT NULL,
        [Database]             NVARCHAR(255) NOT NULL,
        Username               NVARCHAR(255) NOT NULL,
        Password               NVARCHAR(MAX) NULL,
        Encrypt                BIT NOT NULL CONSTRAINT DF_reporting_Source_Encrypt DEFAULT (1),
        TrustServerCertificate BIT NOT NULL CONSTRAINT DF_reporting_Source_TrustServerCertificate DEFAULT (1),
        CreatedAt              DATETIME2(0) NOT NULL CONSTRAINT DF_reporting_Source_CreatedAt DEFAULT (SYSUTCDATETIME()),
        UpdatedAt              DATETIME2(0) NULL
    )');

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_reporting_Report_Name'
      AND object_id = OBJECT_ID(N'reporting.Report')
)
    CREATE INDEX IX_reporting_Report_Name ON reporting.Report (Name);

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'UX_reporting_Source_Name'
      AND object_id = OBJECT_ID(N'reporting.Source')
)
    CREATE UNIQUE INDEX UX_reporting_Source_Name ON reporting.Source (Name);
";
    }
}
