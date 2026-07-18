# AI Workflow Documentation

## 1. Introduction

This document outlines the workflow for using AI (specifically OpenCode) in the LegacyMigration project, which involves migrating an ASP.NET WebForms application to ASP.NET Core Razor Pages. AI was employed to assist with code conversion, reducing manual effort while maintaining accuracy and adherence to modern .NET practices.

## 2. AI Tool Selection

OpenCode was selected as the AI assistant for this migration due to its proficiency in understanding .NET frameworks and its ability to generate context-aware code transformations. The tool was integrated into the development workflow via the Claude Code CLI, which provided secure and auditable interactions.

## 3. AI-Assisted Migration Process

AI assistance was applied to various migration tasks, including:

### 3.1. Model and Service Migration
- **Prompt**: "Convert the following ASP.NET WebForms business logic class to a modern C# service using dependency injection."
- **Example**: Transforming data access methods from direct ADO.NET usage to repository patterns with interfaces.

### 3.2. UI Component Conversion
- **Prompt**: "Convert this ASP.NET WebForms server control (e.g., asp:GridView) to an equivalent Razor Pages implementation using HTML helpers or tag helpers."
- **Example**: Replacing GridView with a `<table>` element populated via `@foreach` loops in Razor syntax.

### 3.3. Event Handler Migration
- **Prompt**: "Transform this ASP.NET WebForms event handler (e.g., Button_Click) into an ASP.NET Core Razor Pages handler method (OnPostAsync)."
- **Example**: Moving logic from code-behind `OnClick` events to `OnPost` handler methods in the page model.

### 3.4. Configuration and Routing
- **Prompt**: "Update the WebForms URL routing (QueryString-based) to ASP.NET Core Razor Pages route parameters."
- **Example**: Changing `Request.QueryString["id"]` to `[FromRoute] int id` in the page model.

### 3.5. Validation Migration
- **Prompt**: "Convert ASP.NET WebForms validation controls (e.g., asp:RequiredFieldValidator) to ASP.NET Core Data Annotations."
- **Example**: Adding `[Required]` and `[StringLength]` attributes to model properties.

## 4. Code Review Process for AI-Generated Code

All AI-generated code underwent a rigorous review process to ensure quality, security, and maintainability:

### 4.1. Automated Checks
- Code was formatted according to project style guidelines using `dotnet format`.
- Static analysis was run via `dotnet analyze` to detect potential issues.
- Unit tests were executed to verify functional correctness.

### 4.2. Peer Review
- A human developer reviewed the AI-generated changes for:
  - Correctness of business logic translation.
  - Adherence to ASP.NET Core conventions.
  - Proper use of dependency injection and configuration.
  - Absence of security vulnerabilities (e.g., open redirects, SQL injection).

### 4.3. Iterative Refinement
- Review feedback was incorporated by prompting the AI with specific correction requests.
- Example follow-up prompt: "The generated method incorrectly handles null values; revise to use null-conditional operators."

### 4.4. Approval Criteria
- Changes were only merged after receiving approval from at least one human reviewer.
- All commits include the trailer `Assisted-by: Claude Code` to disclose AI assistance.

## 5. Validation and Testing

### 5.1. Functional Testing
- Manual testing was performed to verify UI equivalence between WebForms and Razor Pages versions.
- Automated browser tests (using Playwright) were executed where applicable.

### 5.2. Performance Testing
- Basic performance benchmarks were conducted to ensure the migrated pages met response time thresholds.

### 5.3. Regression Testing
- Existing unit tests for business logic were run to ensure no regression was introduced.

## 6. Lessons Learned and Best Practices

### 6.1. Prompt Engineering
- Clear, specific prompts yielded the best results. Including context about the target framework (.NET 9) and desired patterns (e.g., using tag helpers) improved output quality.
- Iterative prompting was necessary for complex transformations.

### 6.2. Human-in-the-Loop
- AI-generated code should never be used without human review. The AI serves as a productivity aid, not a replacement for developer judgment.

### 6.3. Incremental Adoption
- Migrating small, isolated components first allowed the team to refine prompts and review processes before tackling larger modules.

### 6.4. Documentation
- Keeping a log of prompts used and their outcomes helped improve future AI interactions and provided transparency.

### 6.5. Tooling Integration
- Integrating the AI tool into the existing Git workflow (via Claude Code CLI) ensured that all AI-assisted changes were traceable and compliant with project contribution guidelines.

---

*This document was generated as part of the LegacyMigration project to document the AI-assisted workflow. All AI-assisted changes in this repository are disclosed with the `Assisted-by: Claude Code` trailer.*