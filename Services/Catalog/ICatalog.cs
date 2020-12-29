using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Services.Catalog.Requests;

namespace Services.Catalog
{
    public interface ICatalog
    {
        Task<List<Product>> GetWithFilters(CatalogFilters filters);
        Task<Product> GetProduct(int productId);
        Task AddProduct(AddItem request, byte[] image);
        Task AddToBag(int productId, string size, int userId);
        Task RemoveFromBag(int productId, string size, int userId);
    }
}