using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Services.Catalog.Requests;

namespace Services.Catalog
{
    public interface ICatalog
    {
        Task<List<Product>> GetWithFilters(CatalogFilters filters);
        Task<Dictionary<string, short>> GetSizesFor(int productId);
        Task AddProduct(AddItem request, byte[] image);
    }
}