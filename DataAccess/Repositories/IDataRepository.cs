using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public interface IDataRepository
{
    public IDataSet<T> Set<T>() where T : class;
    public Task Save(CancellationToken ct);
}


