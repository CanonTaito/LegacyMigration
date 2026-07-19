using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PropertyListing.RazorPages.Models;
using PropertyListing.RazorPages.Services;

namespace PropertyListing.RazorPages.Pages;

public class SearchModel : PageModel
{
    private readonly IPropertyDataService _propertyDataService;

    public SearchModel(IPropertyDataService propertyDataService)
    {
        _propertyDataService = propertyDataService;
    }

    [BindProperty]
    public string Keyword { get; set; } = string.Empty;

    [BindProperty]
    public string PropertyType { get; set; } = string.Empty;

    public List<Property> Results { get; set; } = [];
    public bool HasSearched { get; set; }
    public string? ValidationMessage { get; set; }
    public string? SearchTips { get; set; }
    public List<SelectListItem> PropertyTypeOptions { get; set; } =
    [
        new("All Types", ""),
        new("House", "House"),
        new("Apartment", "Apartment")
    ];

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        var validationError = ValidateSearchInput();
        if (!string.IsNullOrEmpty(validationError))
        {
            ValidationMessage = validationError;
            return Page();
        }

        HasSearched = true;
        var sanitizedKeyword = SanitizeSearchInput(Keyword);

        Results = _propertyDataService.Search(
            string.IsNullOrWhiteSpace(sanitizedKeyword) ? null : sanitizedKeyword,
            string.IsNullOrWhiteSpace(PropertyType) ? null : PropertyType);

        if (Results.Count > 0)
        {
            SearchTips = "Search tips: Use keywords from property descriptions (e.g., 'pool', 'garden', 'city views')";
        }

        return Page();
    }

    private string ValidateSearchInput()
    {
        if (string.IsNullOrWhiteSpace(Keyword))
            return "Please enter a search keyword.";

        if (Keyword.Length > 100)
            return "Search keyword too long (max 100 characters).";

        if (!IsValidSearchTerm(Keyword))
            return "Invalid search term. Please use letters, numbers, and spaces only.";

        return string.Empty;
    }

    private string SanitizeSearchInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        return input.Trim();
    }

    private bool IsValidSearchTerm(string term)
    {
        if (string.IsNullOrWhiteSpace(term)) return false;

        foreach (char c in term)
        {
            if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c) &&
                c != ',' && c != '.' && c != '-' && c != '!' && c != '?')
            {
                return false;
            }
        }

        return true;
    }
}
