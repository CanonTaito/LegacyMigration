# LegacyMigration

A portfolio project demonstrating the migration of a legacy ASP.NET WebForms application to ASP.NET Core Razor Pages.

## Purpose

Built to showcase modernisation patterns for legacy .NET applications — a common challenge in enterprise environments. Documents the step-by-step transformation from WebForms to Razor Pages, including AI-assisted development workflows.

## Project Structure

```
src/
  PropertyList.WebForms/       # Legacy ASP.NET WebForms app (.NET Framework 4.7.2)
  PropertyListing.RazorPages/  # Modern ASP.NET Core Razor Pages (.NET 9)
  PropertyListing.Tests/       # xUnit tests (unit + functional equivalence)
docs/
  MIGRATION-PATTERNS.md        # 12 documented transformation patterns
  AI-WORKFLOW.md               # How AI tools were used in the migration
  CASE-STUDY.md                # Interview narrative and lessons learned
  copilot-modernization-output/ # Microsoft Copilot Modernization assessment
```

## WebForms Application

A property listing website with:
- **Home** — GridView displaying mock properties with data binding
- **Property Detail** — Detail view with QueryString routing
- **Search** — Postback form with keyword and type filtering
- **Contact** — Form with server-side validators

## Razor Pages Migration

The same application rebuilt in ASP.NET Core Razor Pages, demonstrating 12 migration patterns:

| Pattern | WebForms | Razor Pages |
|---|---|---|
| Master Page → Layout | `Site.Master` | `_Layout.cshtml` |
| GridView → Foreach | `asp:GridView` | `@foreach` + HTML table |
| QueryString → Route | `Request.QueryString["id"]` | `@page "{id:int}"` |
| Server Controls → Tag Helpers | `asp:TextBox` | `<input asp-for="...">` |
| PostBack → Form Submit | `OnClick` event | `<form method="post">` + `OnPost()` |
| Validators → Data Annotations | `asp:RequiredFieldValidator` | `[Required]` |
| ViewState → Model Binding | Automatic via ViewState | `asp-for` tag helpers |
| Page Lifecycle → Handlers | `Page_Load` | `OnGet()` / `OnPost()` |
| Response.Redirect → RedirectToPage | `Response.Redirect()` | `RedirectToPage()` |

## Technologies

### WebForms
- ASP.NET WebForms (.NET Framework 4.7.2)
- Bootstrap 5.3 (CDN)
- IIS Express

### Razor Pages
- ASP.NET Core Razor Pages (.NET 9)
- Tag Helpers
- Model Binding
- Data Annotations
- Bootstrap 5.3 (local)

## Development

### Prerequisites
- Visual Studio 2022+
- .NET 9 SDK
- .NET Framework 4.7.2 targeting pack (for WebForms)

### Build
Open `LegacyMigration.slnx` in Visual Studio. Both projects are included.

### Run WebForms
Set `PropertyList.WebForms` as startup project → Ctrl+Shift+B → F5 (IIS Express)

### Run Razor Pages
Set `PropertyListing.RazorPages` as startup project → F5 (Kestrel, http://localhost:5160)

### Run Tests
```
dotnet test src/PropertyListing.Tests
```

## Documentation

- [Migration Patterns](docs/MIGRATION-PATTERNS.md) — 12 documented transformation patterns with before/after code
- [AI Workflow](docs/AI-WORKFLOW.md) — How AI tools were used, what worked, what didn't
- [Case Study](docs/CASE-STUDY.md) — Interview narrative and lessons learned
- [Copilot Modernization Output](docs/copilot-modernization-output/) — Microsoft's automated assessment
