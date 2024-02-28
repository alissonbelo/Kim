using Domain.Abstractions;
using Domain.Entities;

namespace Infrastructure.Queues;

public class PersonRegistrationQueue : IPersonRegistrationQueue
{
    private readonly Queue<Person> _queue = new Queue<Person>();
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public async Task EnqueueAsync(Person person, CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            _queue.Enqueue(person);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Person?> DequeueAsync(CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return _queue.Count > 0 ? _queue.Dequeue() : null;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}