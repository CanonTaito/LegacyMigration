using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PropertyListing.RazorPages.Services;

namespace PropertyListing.RazorPages.Pages;

public class ContactModel : PageModel
{
    [BindProperty]
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email.")]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string PropertyRef { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Message is required.")]
    public string Message { get; set; } = string.Empty;

    public bool ShowSuccess { get; set; }
    public List<SelectListItem> PropertyRefOptions { get; set; } = [];

    public void OnGet()
    {
        PropertyRefOptions =
        [
            new("-- Select a property --", ""),
            .. PropertyData.GetAll().Select(p => new SelectListItem(p.Address, p.Id.ToString()))
        ];
    }

    public IActionResult OnPost()
    {
        PropertyRefOptions =
        [
            new("-- Select a property --", ""),
            .. PropertyData.GetAll().Select(p => new SelectListItem(p.Address, p.Id.ToString()))
        ];

        if (!ModelState.IsValid)
        {
            return Page();
        }

        ShowSuccess = true;
        return Page();
    }
}
