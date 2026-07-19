# Migration Patterns: WebForms to Razor Pages

Documented patterns for migrating ASP.NET WebForms applications to ASP.NET Core Razor Pages. Each pattern includes the WebForms original, Razor Pages equivalent, when to apply, and known gotchas.

## Pattern 1: Master Page to Layout

**When to apply:** Every WebForms app with a Site.Master or Site.Master.cs.

**WebForms:**
```aspx
<%@ Master Language="C#" CodeBehind="Site.master.cs" %>
<html>
<head>
    <asp:ContentPlaceHolder ID="head" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ContentPlaceHolder ID="ContentPlaceHolder1" runat="server" />
    </form>
</body>
</html>
```

**Razor Pages:**
```html
<html>
<head>
    @await RenderSectionAsync("Head", required: false)
</head>
<body>
    <main>
        @RenderBody()
    </main>
</body>
</html>
```

**Gotchas:**
- `ContentPlaceHolder1` becomes `@RenderBody()` — there can only be one `@RenderBody()` per layout
- Additional content placeholders become `@await RenderSectionAsync("Name", required: false)`
- `<form runat="server">` is removed entirely — Razor Pages handles forms differently
- `~/` path prefix resolves in both, but use `asp-page` tag helpers instead of hardcoded URLs

---

## Pattern 2: GridView to Foreach

**When to apply:** Any `<asp:GridView>` displaying a list of items.

**WebForms:**
```aspx
<asp:GridView ID="PropertyGrid" runat="server" AutoGenerateColumns="False"
    DataKeyNames="Id" OnRowCommand="PropertyGrid_RowCommand">
    <Columns>
        <asp:BoundField DataField="Address" HeaderText="Address" />
        <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
        <asp:ButtonField CommandName="ViewDetail" Text="View" />
    </Columns>
</asp:GridView>
```

**Razor Pages:**
```html
<table>
    <thead>
        <tr><th>Address</th><th>Price</th><th></th></tr>
    </thead>
    <tbody>
        @foreach (var p in Model.Properties)
        {
            <tr>
                <td>@p.Address</td>
                <td>@p.Price.ToString("C")</td>
                <td><a asp-page="/Detail" asp-route-id="@p.Id">View</a></td>
            </tr>
        }
    </tbody>
</table>
```

**Gotchas:**
- `DataKeyNames` is no longer needed — use `asp-route-id` to pass the ID directly
- `RowCommand` event handler collapses into a simple `<a>` link — no server event needed
- `DataFormatString="{0:C}"` becomes `@p.Price.ToString("C")`
- `EmptyDataText` becomes `@if (!Model.Properties.Any()) { <p>No items</p> }`
- GridView's automatic paging/sorting requires manual implementation in Razor Pages

---

## Pattern 3: QueryString to Route Template

**When to apply:** Any page that reads `Request.QueryString["key"]`.

**WebForms:**
```csharp
// URL: PropertyDetail.aspx?id=5
string idStr = Request.QueryString["id"];
if (int.TryParse(idStr, out int id))
{
    var property = PropertyData.GetById(id);
    // ...
}
```

**Razor Pages:**
```html
@page "{id:int}"
```
```csharp
public IActionResult OnGet(int id)
{
    var property = PropertyData.GetById(id);
    if (property is null) return RedirectToPage("/Index");
    return Page();
}
```

**Gotchas:**
- Route parameters are type-safe — `int id` means the route won't match non-integer values
- `int.TryParse` is no longer needed — model binding handles validation
- `[BindProperty]` is for form fields, not route parameters — use method parameters instead
- Multiple route templates are supported: `@page "{id:int}"` and `@page "{id?}"` for optional params

---

## Pattern 4: Server Controls to Tag Helpers

**When to apply:** Any `<asp:TextBox>`, `<asp:DropDownList>`, `<asp:Button>`.

**WebForms:**
```aspx
<asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control" />
<asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
    <asp:ListItem Text="All Types" Value="" />
</asp:DropDownList>
<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
```

**Razor Pages:**
```html
<input asp-for="Keyword" class="form-control" />
<select asp-for="PropertyType" asp-items="Model.TypeOptions" class="form-control"></select>
<button type="submit">Search</button>
```

**Gotchas:**
- `asp-for` binds to a `[BindProperty]` property on the page model — name must match exactly
- `asp-items` requires a `List<SelectListItem>` property on the page model
- `<asp:Button OnClick="handler">` becomes `<form method="post">` + `OnPost()` method
- Tag helpers render as standard HTML — no `runat="server"` or View State overhead
- Validation tag helpers: `<span asp-validation-for="Name" class="text-danger"></span>`

---

## Pattern 5: PostBack to Form Submit

**When to apply:** Any `<asp:Button OnClick="handler">` that triggers a postback.

**WebForms:**
```csharp
protected void btnSearch_Click(object sender, EventArgs e)
{
    string keyword = txtKeyword.Text;
    string type = ddlType.SelectedValue;
    var results = PropertyData.Search(keyword, type);
    SearchResults.DataSource = results;
    SearchResults.DataBind();
}
```

**Razor Pages:**
```csharp
[BindProperty]
public string Keyword { get; set; }

public void OnPost()
{
    Results = PropertyData.Search(Keyword, PropertyType);
}
```

**Gotchas:**
- ViewState is gone — form values are bound via `[BindProperty]` on postback
- `Page_Load` with `!IsPostBack` becomes `OnGet()` — called only on initial GET
- Multiple postback handlers: use `<button asp-page-handler="Save">` + `OnPostSave()`
- `ClientIDMode` changes — WebForms generates IDs like `ContentPlaceHolder1_txtKeyword`, Razor Pages uses `Keyword`

---

## Pattern 6: Validators to Data Annotations

**When to apply:** Any `<asp:RequiredFieldValidator>`, `<asp:RegularExpressionValidator>`.

**WebForms:**
```aspx
<asp:TextBox ID="txtName" runat="server" />
<asp:RequiredFieldValidator ID="rfvName" runat="server"
    ControlToValidate="txtName" ErrorMessage="Name is required."
    CssClass="text-danger" Display="Dynamic" />
```

**Razor Pages:**
```csharp
[Required(ErrorMessage = "Name is required.")]
[BindProperty]
public string Name { get; set; }
```
```html
<input asp-for="Name" />
<span asp-validation-for="Name" class="text-danger"></span>
```

**Gotchas:**
- `Page.IsValid` becomes `ModelState.IsValid`
- Client-side validation requires `_ValidationScriptsPartial` (jQuery Validation)
- `[EmailAddress]` replaces `<asp:RegularExpressionValidator>` for email validation
- `Display="Dynamic"` is automatic in Razor Pages — validation messages appear inline
- `ValidationSummary` becomes `<div asp-validation-summary="ModelOnly"></div>`

---

## Pattern 7: ViewState to Model Binding

**When to apply:** Implicit — ViewState is gone entirely in Razor Pages.

**WebForms:**
```csharp
// ViewState automatically preserves form values between postbacks
// No code needed — controls remember their values
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        // First load — set initial values
    }
    // txtName.Text is preserved automatically after postback
}
```

**Razor Pages:**
```csharp
public class SearchModel : PageModel
{
    [BindProperty]
    public string Keyword { get; set; }  // Preserved via model binding

    public void OnGet() { }

    public void OnPost()
    {
        // Keyword is automatically bound from form data
    }
}
```

**Gotchas:**
- `[BindProperty]` is required for each form field you want preserved
- `TempData` becomes `TempData` (still available in Razor Pages)
- Session state requires `builder.Services.AddSession()` in Program.cs
- Large ViewState payloads are eliminated — a major performance win

---

## Pattern 8: Page Lifecycle to Handlers

**When to apply:** Any `Page_Load`, `Page_PreRender`, or event handler in code-behind.

**WebForms:**
```csharp
public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    protected void PropertyGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetail") { /* ... */ }
    }
}
```

**Razor Pages:**
```csharp
public class IndexModel : PageModel
{
    public List<Property> Properties { get; set; } = [];

    public void OnGet()
    {
        Properties = PropertyData.GetAll();
    }

    // RowCommand is replaced by asp-route-id link — no handler needed
}
```

**Gotchas:**
- `OnGet()` replaces `Page_Load` — no `IsPostBack` check needed (OnGet is only called on GET)
- `OnPost()` replaces `OnClick` event handlers
- `OnGet(int id)` replaces `Request.QueryString["id"]` with parameter binding
- Multiple handlers: `OnGet()`, `OnPost()`, `OnPostSave()`, `OnPostDelete()`
- No `Page_Init`, `Page_PreRender` equivalents — use constructor or `OnGet()`/`OnPost()`

---

## Pattern 9: Response.Redirect to RedirectToPage

**When to apply:** Any `Response.Redirect()` or `Server.Transfer()`.

**WebForms:**
```csharp
Response.Redirect("~/Default.aspx");
// or
Response.Redirect($"PropertyDetail.aspx?id={id}");
```

**Razor Pages:**
```csharp
return RedirectToPage("/Index");
// or
return RedirectToPage("/PropertyDetail", new { id = id });
```

**Gotchas:**
- `RedirectToPage` is a method that returns `IActionResult` — must return it from the handler
- `~/` prefix is not needed — use `/` for absolute paths within the app
- `RedirectToPagePermanent()` sends a 301 instead of 302
- Route parameters are passed as an anonymous object: `new { id = 5 }`

---

## Summary Table

| Pattern | WebForms | Razor Pages | Effort |
|---|---|---|---|
| Master Page to Layout | Site.Master | _Layout.cshtml | Low |
| GridView to Foreach | asp:GridView | @foreach + table | Medium |
| QueryString to Route | Request.QueryString | @page route | Low |
| Server Controls to Tags | asp:TextBox, etc. | asp-for tag helpers | Medium |
| PostBack to Form Submit | OnClick event | OnPost() method | Medium |
| Validators to Annotations | asp:Validator | [Required], etc. | Low |
| ViewState to Model Binding | Automatic | [BindProperty] | Medium |
| Page Lifecycle to Handlers | Page_Load | OnGet/OnPost | Medium |
| Redirect to RedirectToPage | Response.Redirect | RedirectToPage() | Low |

**Total estimated effort for this app:** 4-6 hours for a developer familiar with both frameworks.
