using Application.Persons.Queries.GetAllPerson;
using Application.Persons.Queries.GetPerson;
using Domain.Abstractions;
using Moq;

namespace Application.UnitTest.Person.Queries;

public class PersonQueryHandlerTests
{
    [Fact]
    public async Task GetAllPersonQueryHandler_ShouldReturnAllPersons()
    {
        var personRepositoryMock = new Mock<IPersonRepository>();

        var persons = new List<Domain.Entities.Person>
        {
            new Domain.Entities.Person { Id = Guid.NewGuid(), Name = "John Doe" },
            new Domain.Entities.Person { Id = Guid.NewGuid(), Name = "Jane Smith" }
        };

        personRepositoryMock.Setup(repo => repo.GetAll(CancellationToken.None))
            .ReturnsAsync(persons);

        var handler = new GetAllPersonQuery.GetAllPeronQueryHandler(personRepositoryMock.Object);

        var request = new GetAllPersonQuery();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Domain.Entities.Person>>(result);
        Assert.Equal(persons, result);
    }

    [Fact]
    public async Task GetPersonQueryHandler_ShouldReturnPersonById()
    {
        var personRepositoryMock = new Mock<IPersonRepository>();

        var personId = Guid.NewGuid();
        var person = new Domain.Entities.Person { Id = personId, Name = "John Doe" };

        personRepositoryMock.Setup(repo => repo.Get(personId, CancellationToken.None))
            .ReturnsAsync(person);

        var handler = new GetPersonQueryHandler(personRepositoryMock.Object);

        var request = new GetPersonQuery { Id = personId };
        
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.IsType<Domain.Entities.Person>(result);
        Assert.Equal(person, result);
    }
}