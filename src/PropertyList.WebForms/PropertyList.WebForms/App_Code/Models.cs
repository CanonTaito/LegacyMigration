using System;
using System.Collections.Generic;
using System.Linq;

namespace PropertyList.WebForms
{
    public class Property
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public string Description { get; set; }
        public string PropertyType { get; set; }
        public string ImageUrl { get; set; }
    }

    public static class PropertyData
    {
        private static readonly List<Property> _properties = new List<Property>
        {
            new Property
            {
                Id = 1,
                Address = "12 Kangaroo St, Bulimba QLD 4171",
                Price = 850000,
                Bedrooms = 3,
                Description = "Charming post-war home with modern renovation. Open-plan living, air conditioning, and a covered alfresco area perfect for weekend BBQs.",
                PropertyType = "House",
                ImageUrl = "https://placehold.co/400x300?text=House+1"
            },
            new Property
            {
                Id = 2,
                Address = "4/25 James St, Fortitude Valley QLD 4006",
                Price = 520000,
                Bedrooms = 2,
                Description = "Modern apartment in the heart of the Valley. Floor-to-ceiling windows, secure parking, and rooftop pool access.",
                PropertyType = "Apartment",
                ImageUrl = "https://placehold.co/400x300?text=Apartment+2"
            },
            new Property
            {
                Id = 3,
                Address = "7 Moggill Rd, Indooroopilly QLD 4068",
                Price = 1100000,
                Bedrooms = 4,
                Description = "Spacious family home on a large block. Pool, double garage, and within walking distance to Indooroopilly Shopping Centre.",
                PropertyType = "House",
                ImageUrl = "https://placehold.co/400x300?text=House+3"
            },
            new Property
            {
                Id = 4,
                Address = "12/88 Water St, West End QLD 4101",
                Price = 475000,
                Bedrooms = 1,
                Description = "Sleek inner-city apartment with city views. Walking distance to South Bank and the CBD. Low body corporate fees.",
                PropertyType = "Apartment",
                ImageUrl = "https://placehold.co/400x300?text=Apartment+4"
            },
            new Property
            {
                Id = 5,
                Address = "33 Koala Ave, Pullenvale QLD 4069",
                Price = 1450000,
                Bedrooms = 5,
                Description = "Executive acreage property with pool, tennis court, and 3-car garage. Quiet cul-de-sac with panoramic valley views.",
                PropertyType = "House",
                ImageUrl = "https://placehold.co/400x300?text=House+5"
            }
        };

        public static List<Property> GetAll()
        {
            return _properties;
        }

        public static Property GetById(int id)
        {
            return _properties.FirstOrDefault(p => p.Id == id);
        }

        public static List<Property> Search(string keyword, string propertyType)
        {
            var results = _properties.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                results = results.Where(p =>
                    p.Address.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.Description.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(propertyType))
            {
                results = results.Where(p => p.PropertyType.Equals(propertyType, StringComparison.OrdinalIgnoreCase));
            }

            return results.ToList();
        }
    }
}