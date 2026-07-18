using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PropertyListing.RazorPages.Models;
using PropertyListing.RazorPages.Services;

namespace PropertyListing.RazorPages.Pages;

public class SearchModel : PageModel
{
    [BindProperty]
    public string Keyword { get; set; } = string.Empty;

    [BindProperty]
    public string PropertyType { get; set; } = string.Empty;

    public List<Property> Results { get; set; } = [];
    public bool HasSearched { get; set; }
    public List<SelectListItem> PropertyTypeOptions { get; set; } =
    [
        new("All Types", ""),
        new("House", "House"),
        new("Apartment", "Apartment")
    ];

    public void OnGet()
    {
    }

    public void OnPost()
    {
        HasSearched = true;
        Results = PropertyData.Search(
            string.IsNullOrWhiteSpace(Keyword) ? null : Keyword,
            string.IsNullOrWhiteSpace(PropertyType) ? null : PropertyType);
    }
}
