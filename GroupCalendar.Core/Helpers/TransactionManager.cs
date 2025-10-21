using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace GroupCalendar.Core.Helpers
{
    public class TransactionManager : IAsyncDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private bool disposedValue;

        public TransactionManager(DbConnectionFactory factory)
        {
            _connection = factory.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IDbTransaction Transaction => _transaction;
        public IDbConnection Connection => _connection;

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                await _connection.CloseAsync();
            }

            if (_connection != null)
            {
                await _connection.DisposeAsync();
            }
        }
    }
}
