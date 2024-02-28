using Domain.Entities;

namespace Domain.Abstractions;

public interface IPersonRegistrationQueue
{
    Task EnqueueAsync(Person person, CancellationToken cancellationToken);
    Task<Person?> DequeueAsync(CancellationToken cancellationToken);
}