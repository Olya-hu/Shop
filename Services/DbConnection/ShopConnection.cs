using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Services.DbConnection
{
    public class ShopConnection : IShopConnection
    {
        public ShopContext Context { get; set; }
        private IDbContextTransaction _transaction;
        private bool _connectionIsOpen;

        public ShopConnection(ShopContext dbContext)
        {
            Context = dbContext;
        }

        private void OpenConnection()
        {
            if (_connectionIsOpen) return;
            Context.Database.OpenConnection();
            _connectionIsOpen = true;
        }

        private void CloseConnection()
        {
            Context.Database.CloseConnection();
        }

        public void BeginTransaction()
        {
            OpenConnection();
            _transaction = Context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (!_connectionIsOpen) return;
            _transaction.Commit();
            CloseConnection();
        }

        public void RollbackTransaction()
        {
            if (!_connectionIsOpen) return;
            _transaction.Rollback();
            CloseConnection();
        }
    }
}