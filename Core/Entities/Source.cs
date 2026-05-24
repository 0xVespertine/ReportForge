using System;
using System.ComponentModel;

namespace ReportForge.Entities
{
    /// <summary>
    /// Saved SQL Server source preset. Reports do not depend on these rows; the
    /// report designer can use them as starting points when building a
    /// DevExpress datasource.
    /// </summary>
    public class Source
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }

        [Browsable(false)]
        public string Password { get; set; }

        public bool Encrypt { get; set; } = true;
        public bool TrustServerCertificate { get; set; } = true;

        [Browsable(false)]
        public DateTime CreatedAt { get; set; }

        [Browsable(false)]
        public DateTime UpdatedAt { get; set; }
    }
}
