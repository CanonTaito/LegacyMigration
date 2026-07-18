# LegacyMigration

A portfolio project demonstrating the migration of a legacy ASP.NET WebForms application to ASP.NET Core Razor Pages.

## Purpose

Built to showcase modernisation patterns for legacy .NET applications — a common challenge in enterprise environments. Documents the step-by-step transformation from WebForms to Razor Pages, including AI-assisted development workflows.

## Project Structure

```
src/
  PropertyList.WebForms/    # Legacy ASP.NET WebForms app (.NET Framework 4.7.2)
```

## WebForms Application

A property listing website with:
- **Home** — GridView displaying mock properties with data binding
- **Property Detail** — Detail view with QueryString routing
- **Search** — Postback form with keyword and type filtering
- **Contact** — Form with server-side validators

### Technologies
- ASP.NET WebForms (.NET Framework 4.7.2)
- Bootstrap 5.3
- IIS Express

## Planned: Razor Pages Migration

- Razor Pages on .NET 8+
- Tag helpers replacing server controls
- Dependency injection
- Model binding replacing ViewState
- Client-side validation
- Clean separation of concerns

## Development

### Prerequisites
- Visual Studio 2022
- .NET Framework 4.7.2 targeting pack
- IIS Express

### Build
Open `src/PropertyList.WebForms/PropertyList.WebForms.sln` in Visual Studio and build (Ctrl+Shift+B).
