using Microsoft.AspNetCore.Mvc.RazorPages;
using PropertyListing.RazorPages.Models;
using PropertyListing.RazorPages.Services;

namespace PropertyListing.RazorPages.Pages;

public class IndexModel : PageModel
{
    private readonly IPropertyDataService _propertyDataService;

    public IndexModel(IPropertyDataService propertyDataService)
    {
        _propertyDataService = propertyDataService;
    }

    public List<Property> Properties { get; set; } = [];

    public void OnGet()
    {
        Properties = _propertyDataService.GetAll();
    }
}
