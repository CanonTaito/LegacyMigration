using PropertyListing.RazorPages.Services;

namespace PropertyListing.Tests;

public class PropertyDataTests
{
    [Fact]
    public void GetAll_ReturnsFiveProperties()
    {
        var properties = PropertyData.GetAll();

        Assert.Equal(5, properties.Count);
    }

    [Fact]
    public void GetAll_ReturnsNonEmptyAddresses()
    {
        var properties = PropertyData.GetAll();

        Assert.All(properties, p => Assert.False(string.IsNullOrWhiteSpace(p.Address)));
    }

    [Fact]
    public void GetById_ExistingId_ReturnsProperty()
    {
        var property = PropertyData.GetById(1);

        Assert.NotNull(property);
        Assert.Equal(1, property.Id);
        Assert.Equal("12 Kangaroo St, Bulimba QLD 4171", property.Address);
    }

    [Fact]
    public void GetById_NonExistingId_ReturnsNull()
    {
        var property = PropertyData.GetById(999);

        Assert.Null(property);
    }

    [Fact]
    public void GetById_ReturnsCorrectPropertyForEachId()
    {
        for (int id = 1; id <= 5; id++)
        {
            var property = PropertyData.GetById(id);
            Assert.NotNull(property);
            Assert.Equal(id, property.Id);
        }
    }

    [Fact]
    public void Search_ByKeyword_FiltersByAddress()
    {
        var results = PropertyData.Search("Kangaroo", null);

        Assert.Single(results);
        Assert.Contains("Kangaroo", results[0].Address);
    }

    [Fact]
    public void Search_ByKeyword_FiltersByDescription()
    {
        var results = PropertyData.Search("pool", null);

        Assert.True(results.Count >= 2);
        Assert.All(results, r =>
            Assert.True(
                r.Description.Contains("pool", StringComparison.OrdinalIgnoreCase) ||
                r.Address.Contains("pool", StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void Search_ByType_FiltersByPropertyType()
    {
        var houses = PropertyData.Search(null, "House");
        var apartments = PropertyData.Search(null, "Apartment");

        Assert.Equal(3, houses.Count);
        Assert.Equal(2, apartments.Count);
        Assert.All(houses, h => Assert.Equal("House", h.PropertyType));
        Assert.All(apartments, a => Assert.Equal("Apartment", a.PropertyType));
    }

    [Fact]
    public void Search_CombinedFilters_ReturnsIntersection()
    {
        var results = PropertyData.Search("pool", "House");

        Assert.True(results.Count >= 1);
        Assert.All(results, r =>
        {
            Assert.Equal("House", r.PropertyType);
            Assert.Contains("pool", r.Description, StringComparison.OrdinalIgnoreCase);
        });
    }

    [Fact]
    public void Search_EmptyKeyword_ReturnsAllProperties()
    {
        var results = PropertyData.Search("", null);

        Assert.Equal(5, results.Count);
    }

    [Fact]
    public void Search_NoMatch_ReturnsEmpty()
    {
        var results = PropertyData.Search("xyznonexistent", null);

        Assert.Empty(results);
    }

    [Fact]
    public void Search_IsCaseInsensitive()
    {
        var lower = PropertyData.Search("kangaroo", null);
        var upper = PropertyData.Search("KANGAROO", null);
        var mixed = PropertyData.Search("KaNgArOo", null);

        Assert.Single(lower);
        Assert.Single(upper);
        Assert.Single(mixed);
        Assert.Equal(lower[0].Id, upper[0].Id);
        Assert.Equal(upper[0].Id, mixed[0].Id);
    }
}
