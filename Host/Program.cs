using System;
using System.Configuration;
using DevExpress.DataAccess.Sql;
using ReportForge.Data;

namespace ReportForge
{
    internal static class Program
    {
        private static void Main()
        {
            var prefix = ConfigurationManager.AppSettings["RenderEndpointPrefix"];
            if (string.IsNullOrWhiteSpace(prefix))
                prefix = "http://localhost:7825/";

            try
            {
                SqlDataSource.DisableCustomQueryValidation = true;
                SqlDataSource.AllowCustomSqlQueries = true;

                Schema.EnsureCreated();

                using (var endpoint = new RenderEndpoint(prefix))
                {
                    endpoint.Start();

                    Console.WriteLine("ReportForge Host");
                    Console.WriteLine("Listening on " + endpoint.Prefix);
                    Console.WriteLine("Endpoints: GET /health, GET /reports, POST /render");
                    Console.WriteLine("Press Enter to stop.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Host failed to start:");
                Console.Error.WriteLine(RenderEndpoint.FlattenException(ex));
                Environment.ExitCode = 1;
            }
        }
    }
}
