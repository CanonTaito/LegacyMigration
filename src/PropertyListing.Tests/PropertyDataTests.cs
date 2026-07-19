using PropertyListing.RazorPages.Services;

namespace PropertyListing.Tests;

public class PropertyDataTests
{
    private readonly IPropertyDataService _service = new PropertyDataService();

    [Fact]
    public void GetAll_ReturnsFiveProperties()
    {
        var properties = _service.GetAll();

        Assert.Equal(5, properties.Count);
    }

    [Fact]
    public void GetAll_ReturnsNonEmptyAddresses()
    {
        var properties = _service.GetAll();

        Assert.All(properties, p => Assert.False(string.IsNullOrWhiteSpace(p.Address)));
    }

    [Fact]
    public void GetById_ExistingId_ReturnsProperty()
    {
        var property = _service.GetById(1);

        Assert.NotNull(property);
        Assert.Equal(1, property.Id);
        Assert.Equal("12 Kangaroo St, Bulimba QLD 4171", property.Address);
    }

    [Fact]
    public void GetById_NonExistingId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _service.GetById(999));
    }

    [Fact]
    public void GetById_ReturnsCorrectPropertyForEachId()
    {
        for (int id = 1; id <= 5; id++)
        {
            var property = _service.GetById(id);
            Assert.NotNull(property);
            Assert.Equal(id, property.Id);
        }
    }

    [Fact]
    public void Search_ByKeyword_FiltersByAddress()
    {
        var results = _service.Search("Kangaroo", null);

        Assert.Single(results);
        Assert.Contains("Kangaroo", results[0].Address);
    }

    [Fact]
    public void Search_ByKeyword_FiltersByDescription()
    {
        var results = _service.Search("pool", null);

        Assert.True(results.Count >= 2);
        Assert.All(results, r =>
            Assert.True(
                r.Description.Contains("pool", StringComparison.OrdinalIgnoreCase) ||
                r.Address.Contains("pool", StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void Search_ByType_FiltersByPropertyType()
    {
        var houses = _service.Search(null, "House");
        var apartments = _service.Search(null, "Apartment");

        Assert.Equal(3, houses.Count);
        Assert.Equal(2, apartments.Count);
        Assert.All(houses, h => Assert.Equal("House", h.PropertyType));
        Assert.All(apartments, a => Assert.Equal("Apartment", a.PropertyType));
    }

    [Fact]
    public void Search_CombinedFilters_ReturnsIntersection()
    {
        var results = _service.Search("pool", "House");

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
        var results = _service.Search("", null);

        Assert.Equal(5, results.Count);
    }

    [Fact]
    public void Search_NoMatch_ReturnsEmpty()
    {
        var results = _service.Search("xyznonexistent", null);

        Assert.Empty(results);
    }

    [Fact]
    public void Search_IsCaseInsensitive()
    {
        var lower = _service.Search("kangaroo", null);
        var upper = _service.Search("KANGAROO", null);
        var mixed = _service.Search("KaNgArOo", null);

        Assert.Single(lower);
        Assert.Single(upper);
        Assert.Single(mixed);
        Assert.Equal(lower[0].Id, upper[0].Id);
        Assert.Equal(upper[0].Id, mixed[0].Id);
    }
}
