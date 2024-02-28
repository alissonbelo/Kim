using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(AppDbContext context) : base(context)
    { }

    public async Task<Person?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await Context.Persons.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}