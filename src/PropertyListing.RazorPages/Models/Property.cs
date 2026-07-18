namespace PropertyListing.RazorPages.Models;

public class Property
{
    public int Id { get; init; }
    public required string Address { get; init; }
    public decimal Price { get; init; }
    public int Bedrooms { get; init; }
    public required string Description { get; init; }
    public required string PropertyType { get; init; }
    public required string ImageUrl { get; init; }
}
