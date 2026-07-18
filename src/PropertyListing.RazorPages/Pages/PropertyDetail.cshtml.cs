using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PropertyListing.RazorPages.Models;
using PropertyListing.RazorPages.Services;

namespace PropertyListing.RazorPages.Pages;

public class PropertyDetailModel : PageModel
{
    public Property? Property { get; set; }

    public IActionResult OnGet(int id)
    {
        Property = PropertyData.GetById(id);

        if (Property is null)
        {
            return RedirectToPage("/Index");
        }

        return Page();
    }
}
