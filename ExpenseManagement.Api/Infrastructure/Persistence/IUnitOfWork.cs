using System.Data;

namespace ExpenseManagement.Api.Infrastructure.Persistence;

public interface IUnitOfWork
{
    IDbTransaction? Transaction { get; }
    void BeginTransaction();
    void Commit();
    void Rollback();
}