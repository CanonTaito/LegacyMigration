using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PropertyListing.RazorPages.Services;

namespace PropertyListing.RazorPages.Pages;

public class ContactModel : PageModel
{
    private readonly IPropertyDataService _propertyDataService;

    public ContactModel(IPropertyDataService propertyDataService)
    {
        _propertyDataService = propertyDataService;
    }

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
    public string? BusinessValidationErrors { get; set; }
    public List<SelectListItem> PropertyRefOptions { get; set; } = [];

    public void OnGet()
    {
        LoadPropertyReferenceData();
    }

    public IActionResult OnPost()
    {
        LoadPropertyReferenceData();

        var businessErrors = PerformBusinessValidation();
        if (!string.IsNullOrEmpty(businessErrors))
        {
            BusinessValidationErrors = businessErrors;
            return Page();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        ShowSuccess = true;
        return Page();
    }

    private void LoadPropertyReferenceData()
    {
        PropertyRefOptions =
        [
            new("-- Select a property --", ""),
            .. _propertyDataService.GetAll().Select(p => new SelectListItem(p.Address, p.Id.ToString()))
        ];
    }

    private string PerformBusinessValidation()
    {
        var errors = new List<string>();

        if (!ValidateContactName(Name))
            errors.Add("Name must be at least 2 characters and contain only letters and spaces.");

        if (!ValidateContactEmail(Email))
            errors.Add("Please provide a valid email address.");

        if (!ValidatePropertySelection(PropertyRef))
            errors.Add("Please select a property you're interested in.");

        if (!ValidateContactMessage(Message))
            errors.Add("Message must be at least 10 characters describing your inquiry.");

        return errors.Count > 0 ? string.Join("<br/>", errors) : string.Empty;
    }

    private bool ValidateContactName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) &&
               name.Length >= 2 &&
               name.Length <= 100 &&
               name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
    }

    private bool ValidateContactEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) &&
               email.Contains("@") &&
               email.Contains(".") &&
               email.Length <= 320;
    }

    private bool ValidatePropertySelection(string selectedValue)
    {
        return !string.IsNullOrEmpty(selectedValue) &&
               int.TryParse(selectedValue, out int id) &&
               id > 0;
    }

    private bool ValidateContactMessage(string message)
    {
        return !string.IsNullOrWhiteSpace(message) &&
               message.Length >= 10 &&
               message.Length <= 2000;
    }
}
