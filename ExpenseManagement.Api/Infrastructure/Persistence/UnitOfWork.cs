using System.Data;

namespace ExpenseManagement.Api.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction? _transaction;

    public IDbTransaction? Transaction => _transaction;
    
    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public void BeginTransaction()
    {
        if(_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        
        if (_transaction == null)
        {
            _transaction = _connection.BeginTransaction();
        }
    }

    public void Commit()
    {
        if (_transaction != null)
        {
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public void Rollback()
    {
        if (_transaction != null)
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }
    }
}
