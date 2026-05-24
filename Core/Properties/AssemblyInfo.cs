using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Core")]
[assembly: AssemblyDescription("ReportForge shared report catalog, data access and rendering pipeline.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ReportForge")]
[assembly: AssemblyCopyright("Copyright (c) 2026")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]
[assembly: Guid("b1e5c3a2-7d4f-4a6b-9c2e-1f0a8d7b6c54")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Current desktop and endpoint hosts can still consume internal helpers.
[assembly: InternalsVisibleTo("Studio")]
[assembly: InternalsVisibleTo("Host")]
