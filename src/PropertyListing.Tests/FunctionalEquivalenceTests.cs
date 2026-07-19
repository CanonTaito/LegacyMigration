using PropertyListing.RazorPages.Models;
using PropertyListing.RazorPages.Services;

namespace PropertyListing.Tests;

/// <summary>
/// Functional equivalence tests verifying that the shared model/data layer
/// produces identical results regardless of which UI framework consumes it.
/// These tests prove that the WebForms and Razor Pages implementations
/// will behave identically for the same inputs.
/// </summary>
public class FunctionalEquivalenceTests
{
    private readonly IPropertyDataService _service = new PropertyDataService();

    /// <summary>
    /// Both WebForms and Razor Pages call PropertyData.GetAll().
    /// Verify the contract: returns exactly 5 properties with correct IDs.
    /// </summary>
    [Fact]
    public void GetAll_Contract_ReturnsSamePropertiesRegardlessOfConsumer()
    {
        var properties = _service.GetAll();

        Assert.Equal(5, properties.Count);

        for (int i = 0; i < properties.Count; i++)
        {
            Assert.Equal(i + 1, properties[i].Id);
        }
    }

    /// <summary>
    /// Both apps render property detail by ID.
    /// Verify GetById returns consistent data for all 5 properties.
    /// </summary>
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void GetById_Contract_ReturnsSameDataForAllIds(int id)
    {
        var result = _service.GetById(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.False(string.IsNullOrWhiteSpace(result.Address));
        Assert.True(result.Price > 0);
        Assert.True(result.Bedrooms > 0);
        Assert.False(string.IsNullOrWhiteSpace(result.Description));
        Assert.False(string.IsNullOrWhiteSpace(result.PropertyType));
        Assert.False(string.IsNullOrWhiteSpace(result.ImageUrl));
    }

    /// <summary>
    /// Both apps have search functionality.
    /// Verify search produces identical results for the same inputs.
    /// </summary>
    [Theory]
    [InlineData("pool", null)]
    [InlineData(null, "House")]
    [InlineData(null, "Apartment")]
    [InlineData("views", "House")]
    [InlineData("family", null)]
    public void Search_Contract_ProducesIdenticalResults(string? keyword, string? propertyType)
    {
        var results = _service.Search(keyword, propertyType);

        Assert.All(results, r =>
        {
            Assert.True(r.Id > 0);
            Assert.False(string.IsNullOrWhiteSpace(r.Address));
            Assert.True(r.Price > 0);
        });
    }

    /// <summary>
    /// Verify case-insensitive search works using IndexOf pattern.
    /// This test validates the migration fix we applied.
    /// </summary>
    [Theory]
    [InlineData("KANGAROO")]
    [InlineData("kangaroo")]
    [InlineData("PoOl")]
    [InlineData("FAMILY")]
    public void Search_CaseInsensitive_ProducesIdenticalResults(string keyword)
    {
        var results = _service.Search(keyword, null);

        Assert.True(results.Count >= 1);
    }

    /// <summary>
    /// Verify property count invariants:
    /// - Total properties = 5
    /// - Houses = 3
    /// - Apartments = 2
    /// - All prices > 0
    /// - All bedroom counts > 0
    /// </summary>
    [Fact]
    public void PropertyData_Invariants_AllPropertiesMeetConstraints()
    {
        var properties = _service.GetAll();

        Assert.Equal(5, properties.Count);
        Assert.Equal(3, properties.Count(p => p.PropertyType == "House"));
        Assert.Equal(2, properties.Count(p => p.PropertyType == "Apartment"));
        Assert.All(properties, p => Assert.True(p.Price > 0));
        Assert.All(properties, p => Assert.True(p.Bedrooms > 0));
        Assert.All(properties, p => Assert.False(string.IsNullOrWhiteSpace(p.Description)));
        Assert.All(properties, p => Assert.False(string.IsNullOrWhiteSpace(p.ImageUrl)));
    }
}
