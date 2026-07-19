# AI-Assisted Migration Workflow

Documents how AI tools were used during the WebForms to Razor Pages migration, including what worked, what didn't, and responsible AI practices applied.

## Tools Used

### Primary: OpenCode (Open-Source Terminal Agent)

**What it is:** A free, open-source CLI tool that acts as an AI coding assistant. It can read/write files, run commands, search codebases, and execute multi-step tasks.

**How it was used:**
- Scaffolded the .NET 10 Razor Pages project
- Created all page migrations (Index, PropertyDetail, Search, Contact)
- Generated unit tests and functional equivalence tests
- Created documentation files
- Managed git commits and GitHub issues

**Model:** big-pickle (opencode/big-pickle)

### Secondary: GitHub Copilot Modernization

**What it is:** Microsoft's official AI agent for .NET migration. Assesses projects and generates migration plans.

**How it was used:**
- Ran assessment on the WebForms project
- Generated migration plan targeting Blazor (not Razor Pages)
- Output stored in `docs/copilot-modernization-output/`

## Prompts That Worked Well

### Specific, Contextual Prompts
```
"Create a Razor Pages Index.cshtml that replaces the WebForms Default.aspx
GridView with a @foreach loop. The WebForms version uses DataKeyNames="Id"
and RowCommand to navigate to PropertyDetail.aspx?id={id}."
```
**Why it worked:** Specific about source, target, and the exact transformation needed.

### Pattern-Based Prompts
```
"Apply the PostBack to Form Submit pattern to the Search page.
WebForms uses asp:Button OnClick="btnSearch_Click" with asp:TextBox
and asp:DropDownList. Razor Pages should use <form method="post">
with OnPost() and [BindProperty] attributes."
```
**Why it worked:** Referenced an established pattern, giving the AI a clear template.

### Iterative Refinement
```
"The Contact page validators aren't working. The WebForms version uses
asp:RequiredFieldValidator and Page.IsValid. What's the Razor Pages equivalent?"
```
**Why it worked:** Described the problem and current state, allowing targeted fix.

## Prompts That Didn't Work

### Too Vague
```
"Convert this WebForms app to Razor Pages"
```
**Why it failed:** Too broad — produced generic output that didn't match the specific app structure.

### Assuming Framework Knowledge
```
"Use the same approach as the WebForms version"
```
**Why it failed:** The AI doesn't automatically know what "the WebForms version" does — you need to describe the specific behavior.

### Missing Constraints
```
"Add validation to the Contact form"
```
**Why it failed (initially):** Didn't specify which validation framework, which fields, or what error messages. Produced incorrect output that needed manual correction.

## What AI Handled Well

| Task | Quality | Notes |
|---|---|---|
| Project scaffolding | Excellent | `dotnet new webapp` with correct framework |
| Model/Service creation | Excellent | Clean C# with modern syntax |
| Layout migration | Excellent | Direct mapping from ContentPlaceHolder to RenderBody |
| GridView to Foreach | Excellent | Correct @foreach with tag helpers |
| QueryString to Route | Excellent | @page directive + parameter binding |
| Form migration | Good | Needed minor tweaks for [BindProperty] |
| Validator migration | Good | Data annotations straightforward |
| Test generation | Excellent | Comprehensive coverage with [Theory] parameterized tests |
| Documentation | Excellent | Structured, interview-ready content |

## What AI Needed Help With

| Task | Issue | Resolution |
|---|---|---|
| .NET 10 targeting | VS 2026 doesn't support .NET 10 | Manual downgrade to .NET 9 |
| WebForms .csproj in `dotnet sln` | Old-style .csproj incompatible | Manual .slnx editing |
| `string.Contains` API difference | .NET Framework vs .NET Core | Manual fix (IndexOf pattern) |
| WebForms UnobtrusiveValidation | Requires jQuery registration | Manual Web.config fix |
| Copilot Modernization output | Targets Blazor, not Razor Pages | Manual comparison and documentation |

## Responsible AI Practices

### Code Review
- All AI-generated code was reviewed before committing
- Manual corrections applied where AI output didn't match requirements
- Tests verified that AI-generated code produced correct output

### Validation Strategy
- Unit tests for model/service layer (PropertyDataTests)
- Functional equivalence tests proving both apps produce identical results
- Manual visual verification in browser for each page

### Transparency
- All AI-generated code is in the repo with clear commit messages
- AI workflow documented in this file
- Copilot Modernization output preserved for reference

### Limitations Acknowledged
- AI couldn't handle framework incompatibilities (.NET 10 vs VS 2026)
- AI couldn't run the application — only generated code
- AI's assessment (Copilot Modernization) targeted Blazor, not our actual target
- Manual intervention was required for ~20% of the migration

## Key Takeaway

AI tools are most effective when:
1. **You understand the target architecture** — AI generates code, you validate it
2. **You provide specific context** — Vague prompts produce vague output
3. **You have a validation strategy** — Tests prove the AI output is correct
4. **You iterate** — First output is rarely final; refinement is expected

The combination of AI-assisted code generation + manual validation + automated testing produced a higher quality result than either approach alone.
