using Domain.Entities;

namespace Domain.Abstractions;

public interface IPersonRepository : IBaseRepository<Person>
{
    Task<Person?> GetByEmail(string email, CancellationToken cancellationToken);
}