using Database;

namespace Services.DbConnection
{
    public interface IShopConnection
    {
        ShopContext Context { get; set; }
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
