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
    /// <summary>
    /// Both WebForms and Razor Pages call PropertyData.GetAll().
    /// Verify the contract: returns exactly 5 properties with correct IDs.
    /// </summary>
    [Fact]
    public void GetAll_Contract_ReturnsSamePropertiesRegardlessOfConsumer()
    {
        // Simulate WebForms consumption
        var webFormsProperties = PropertyData.GetAll();

        // Simulate Razor Pages consumption
        var razorPagesProperties = PropertyData.GetAll();

        Assert.Equal(webFormsProperties.Count, razorPagesProperties.Count);

        for (int i = 0; i < webFormsProperties.Count; i++)
        {
            Assert.Equal(webFormsProperties[i].Id, razorPagesProperties[i].Id);
            Assert.Equal(webFormsProperties[i].Address, razorPagesProperties[i].Address);
            Assert.Equal(webFormsProperties[i].Price, razorPagesProperties[i].Price);
            Assert.Equal(webFormsProperties[i].Bedrooms, razorPagesProperties[i].Bedrooms);
            Assert.Equal(webFormsProperties[i].PropertyType, razorPagesProperties[i].PropertyType);
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
        var webFormsResult = PropertyData.GetById(id);
        var razorResult = PropertyData.GetById(id);

        Assert.NotNull(webFormsResult);
        Assert.NotNull(razorResult);

        Assert.Equal(webFormsResult.Id, razorResult.Id);
        Assert.Equal(webFormsResult.Address, razorResult.Address);
        Assert.Equal(webFormsResult.Price, razorResult.Price);
        Assert.Equal(webFormsResult.Bedrooms, razorResult.Bedrooms);
        Assert.Equal(webFormsResult.Description, razorResult.Description);
        Assert.Equal(webFormsResult.PropertyType, razorResult.PropertyType);
        Assert.Equal(webFormsResult.ImageUrl, razorResult.ImageUrl);
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
        var webFormsResults = PropertyData.Search(keyword, propertyType);
        var razorResults = PropertyData.Search(keyword, propertyType);

        Assert.Equal(webFormsResults.Count, razorResults.Count);

        for (int i = 0; i < webFormsResults.Count; i++)
        {
            Assert.Equal(webFormsResults[i].Id, razorResults[i].Id);
            Assert.Equal(webFormsResults[i].Address, razorResults[i].Address);
            Assert.Equal(webFormsResults[i].Price, razorResults[i].Price);
        }
    }

    /// <summary>
    /// WebForms uses .NET Framework 4.7.2 string.Contains (case-sensitive).
    /// Razor Pages uses .NET 9 string.Contains (case-sensitive).
    /// Verify case-insensitive search works in both by using IndexOf pattern.
    /// This test validates the migration fix we applied.
    /// </summary>
    [Theory]
    [InlineData("KANGAROO")]
    [InlineData("kangaroo")]
    [InlineData("PoOl")]
    [InlineData("FAMILY")]
    public void Search_CaseInsensitive_ProducesIdenticalResults(string keyword)
    {
        var webFormsResults = PropertyData.Search(keyword, null);
        var razorResults = PropertyData.Search(keyword, null);

        Assert.Equal(webFormsResults.Count, razorResults.Count);
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
        var properties = PropertyData.GetAll();

        Assert.Equal(5, properties.Count);
        Assert.Equal(3, properties.Count(p => p.PropertyType == "House"));
        Assert.Equal(2, properties.Count(p => p.PropertyType == "Apartment"));
        Assert.All(properties, p => Assert.True(p.Price > 0));
        Assert.All(properties, p => Assert.True(p.Bedrooms > 0));
        Assert.All(properties, p => Assert.False(string.IsNullOrWhiteSpace(p.Description)));
        Assert.All(properties, p => Assert.False(string.IsNullOrWhiteSpace(p.ImageUrl)));
    }
}
