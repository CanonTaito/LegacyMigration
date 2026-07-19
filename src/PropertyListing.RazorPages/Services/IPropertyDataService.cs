using PropertyListing.RazorPages.Models;

namespace PropertyListing.RazorPages.Services;

public interface IPropertyDataService
{
    List<Property> GetAll();
    Property GetById(int id);
    Property GetProperty(int id, bool includeDeleted = false);
    List<Property> Search(string? keyword, string? propertyType);
    void AddProperty(Property property);
    void UpdateProperty(Property property);
    void DeleteProperty(int id);
}
