using Application.Persons.Queries.GetAllPerson;
using Domain.Abstractions;
using Moq;

namespace Application.UnitTest.Person.Queries;

public class GetAllPersonQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAllPersons()
    {
        var personRepositoryMock = new Mock<IPersonRepository>();

        var expectedPersons = new List<Domain.Entities.Person>
        {
            new Domain.Entities.Person { Id = Guid.NewGuid(), Name = "John Doe", Document = "12345678901", Email = "john@example.com", Phone = "1234567890" },
            new Domain.Entities.Person { Id = Guid.NewGuid(), Name = "Jane Smith", Document = "10987654321", Email = "jane@example.com", Phone = "0987654321" }
        };

        personRepositoryMock.Setup(repo => repo.GetAll(CancellationToken.None))
            .ReturnsAsync(expectedPersons);

        var handler = new GetAllPersonQuery.GetAllPeronQueryHandler(personRepositoryMock.Object);

        var request = new GetAllPersonQuery();
        
        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Domain.Entities.Person>>(result);
        Assert.Equal(expectedPersons, result);
    }
}