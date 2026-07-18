using Microsoft.AspNetCore.Mvc.RazorPages;
using PropertyListing.RazorPages.Models;
using PropertyListing.RazorPages.Services;

namespace PropertyListing.RazorPages.Pages;

public class IndexModel : PageModel
{
    public List<Property> Properties { get; set; } = [];

    public void OnGet()
    {
        Properties = PropertyData.GetAll();
    }
}
