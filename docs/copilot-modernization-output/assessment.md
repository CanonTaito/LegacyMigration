# Assessment — PropertyList.WebForms

Project: src/PropertyList.WebForms/PropertyList.WebForms/PropertyList.WebForms.csproj

Summary:
- Target framework: .NET Framework 4.7.2 (classic non-SDK csproj)
- WebForms artifacts detected: Contact.aspx, Default.aspx, PropertyDetail.aspx, Search.aspx, Site.Master
- Code-behind: .aspx.cs files present (Contact.aspx.cs, Default.aspx.cs, PropertyDetail.aspx.cs, Search.aspx.cs)
- App_Code usage: App_Code\Models.cs
- System.Web references and server controls extensively used (GridView, DropDownList, Validators, asp:Button, etc.)
- NuGet packages: Microsoft.CodeDom.Providers.DotNetCompilerPlatform (only)

Key findings and migration impact:
- This is a traditional ASP.NET Web Forms application that depends on System.Web and server-side controls. Automated conversion is limited — most UI will require manual reimplementation as Blazor components.
- Master pages (Site.Master) will need to be replaced by a Blazor MainLayout/Shared layout.
- Code-behind logic must be reviewed and refactored into component code or injectable services; lifecycle and event model differ between Web Forms and Blazor.
- Web.config settings (compilation, httpRuntime) and any ASP.NET pipeline behavior must be evaluated and ported to appsettings and middleware as appropriate.
- No heavy third-party WebForms controls or obscure NuGet packages were detected in packages.config, which reduces migration friction.
- Project is not SDK-style and will need SDK conversion (or the Blazor project should be created separately and reference shared libraries).

Risks and blockers:
- Dependence on System.Web APIs (HttpContext.Current, server-side controls with automatic view state) requires code refactor.
- App_Code dynamic compilation patterns may not translate directly; move these types into class library projects.
- Any custom HttpModules/HttpHandlers or Global.asax logic (none found in root) would need explicit migration to middleware.
- Build relies on MSBuild WebApplication targets and a packages import; ensure NuGet packages are restored and imports resolved before changes.

Recommended migration strategy (high-level):
1. Target: Blazor Server (recommended by this scenario). Blazor Server gives server-side rendering and a component model suitable for porting WebForms apps.
2. Approach: Side-by-side incremental migration
   - Create a new Blazor Server project in the same solution (or in a new project) targeting a modern TFM (e.g., net8.0 or net10.0 — confirm target with you).
   - Move non-UI code (business logic, models in App_Code) into a .NET Standard / .NET class library that can be referenced by both projects while migrating.
   - Reimplement each page as a Razor component, starting with low-risk pages (Contact, PropertyDetail) and preserving URLs using routing or a reverse proxy during migration.
   - Replace Site.Master with a Blazor layout component and migrate shared CSS/JS assets.
   - Migrate data access if present to EF Core or compatible libraries as needed.
3. Alternative: Full rewrite in-place — more risky and requires converting the web project to SDK-style, updating packages, and rewriting all UI code at once.

Next recommended actions (proposed tasks):
- Inventory task: produce a per-page and per-class lines-of-code and dependency map (helps estimate effort)
- Create a new Blazor Server project scaffold and verify it runs in the solution
- Extract App_Code types into a class library and convert to SDK-style project
- Pick a pilot page to port (e.g., PropertyDetail) and implement end-to-end (UI, navigation, data access)

Estimated effort (very rough):
- Small app (few pages, simple business logic): 1–2 weeks for a single developer to migrate core UX + data access
- Medium complexity (multiple pages, forms, moderate server-side logic): 2–6 weeks
- Large or highly stateful apps: more — requires detailed scoping after inventory

Files examined (partial):
- src/PropertyList.WebForms/PropertyList.WebForms/PropertyList.WebForms.csproj
- src/PropertyList.WebForms/PropertyList.WebForms/Default.aspx(.cs/.designer)
- src/PropertyList.WebForms/PropertyList.WebForms/Contact.aspx(.cs/.designer)
- src/PropertyList.WebForms/PropertyList.WebForms/PropertyDetail.aspx(.cs/.designer)
- src/PropertyList.WebForms/PropertyList.WebForms/Web.config
- src/PropertyList.WebForms/PropertyList.WebForms/packages.config

Recommended next step: confirm migration target (Blazor Server) and TFM (e.g., net8.0 or net10.0). If confirmed, I will generate a migration plan with concrete tasks and start the first task: inventory and per-file impact analysis.
