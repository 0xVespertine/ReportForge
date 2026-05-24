# ReportForge

ReportForge is a personal, single-operator WinForms tool for managing
DevExpress reports and rendering them on demand.

## Current Shape

- **Core** contains SQL Server access, schema creation, report/source storage,
  source password encryption, parameter validation, and DevExpress rendering.
- **Studio** is the desktop UI for managing sources, creating reports, and
  designing/saving `.repx` layouts.
- **Host** is a small local console process exposing report rendering over HTTP.
- **SQL Server** stores the application tables in the `reporting` schema.

## Scope

This is intentionally small:

- One operator.
- Personal desktop workflow.
- WinForms, DevExpress Reports, Dapper, and SQL Server.
- Report management and rendering only.
- Local/trusted Host endpoint only.

Keep future changes sized for that reality.

## Core Nouns

- **Report** - a reusable DevExpress report template. It stores the report
  name, description, saved layout XML, and declared parameter schema.
- **Source** - a saved SQL Server connection used by reports. The password is
  stored encrypted at rest using the local secret key.

Everything else should be treated as mechanism unless it clearly earns a place
in the model.

## Database

`Core.Data.Schema.EnsureCreated()` creates:

- `reporting.Report`
- `reporting.Source`

Database connection values are read from `mssql.env`.

## Host API

The Host reads `RenderEndpointPrefix` from `Host/App.config`.

Default:

```text
http://localhost:8787/
```

Endpoints:

- `GET /health` returns a simple status payload.
- `GET /reports` lists renderable reports. A report is renderable only after a
  layout has been saved.
- `POST /render` renders one report and returns a base64 file payload.

Example render request:

```json
{
  "reportId": 1,
  "exportType": "Pdf",
  "params": {
    "pFrom": "2026-01-01",
    "pTo": "2026-01-31"
  }
}
```

Allowed export formats:

- `Pdf`
- `Excel`
- `Image`
- `Html`

## Workflow

1. Configure `mssql.env`.
2. Run Studio.
3. Create one or more sources.
4. Create a report.
5. Open the designer, build the `.repx` layout, and save it.
6. Run Host.
7. Call `/reports` to inspect available reports and parameters.
8. Call `/render` to generate the requested file.

## Rendering Rules

Rendering should fail loudly when something is wrong:

- Missing report.
- Missing saved layout.
- Unknown parameter.
- Missing required parameter.
- Invalid parameter type.
- Invalid period range.
- Template/schema mismatch.
- Unsupported export format.

Never silently render an unfiltered or mismatched report.

## Design Principles

- Prefer the existing Core/Studio/Host split.
- Keep database changes narrow and explicit.
- Keep the endpoint small and deterministic.
- Avoid adding unrelated workflow domains unless explicitly requested.
- Encrypt source passwords at rest, but do not overbuild security for this
  personal local app.
