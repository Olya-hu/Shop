using Database.Enums;

namespace Services.Catalog.Requests
{
    public class CatalogFilters
    {
        public bool SortByPrice { get; set; } = true;
        public string NameContains { get; set; }
        public decimal? PriceHigherThan { get; set; }
        public decimal? PriceLowerThan { get; set; }
        public Gender[] Genders { get; set; }
        public Category[] Categories { get; set; }
        public Brand[] Brands { get; set; }
        public Color[] Colors { get; set; }
        public Size[] Sizes { get; set; }
    }
}