using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PropertyListing.RazorPages.Models;
using PropertyListing.RazorPages.Services;

namespace PropertyListing.RazorPages.Pages;

public class PropertyDetailModel : PageModel
{
    private readonly IPropertyDataService _propertyDataService;

    public PropertyDetailModel(IPropertyDataService propertyDataService)
    {
        _propertyDataService = propertyDataService;
    }

    public Property? Property { get; set; }
    public string? ErrorMessage { get; set; }

    public IActionResult OnGet(int id)
    {
        if (id <= 0)
        {
            ErrorMessage = "Invalid property ID. Please select a valid property from the list.";
            return Page();
        }

        try
        {
            Property = _propertyDataService.GetById(id);
        }
        catch (ArgumentException)
        {
            ErrorMessage = $"Property with ID {id} was not found. It may have been removed or you may not have permission to view it.";
        }

        return Page();
    }
}
