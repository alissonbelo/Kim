using Domain.Abstractions;
using Domain.Entities;
using Moq;

namespace Domain.UnitTest;

public class RepositoryTests
{        
    [Fact]
    public async Task GetPerson_Success()
    {
        var mockRepository = new Mock<IBaseRepository<Person>>();
        var personId = Guid.NewGuid();
        var expectedPerson = new Person(personId, "John Doe", "12345678901", "john@example.com", "1234567890");

        mockRepository.Setup(repo => repo.Get(personId, CancellationToken.None))
            .ReturnsAsync(expectedPerson);
        
        var result = await mockRepository.Object.Get(personId, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(expectedPerson, result);
    }

    [Fact]
    public async Task GetAllPersons_Success()
    {
        var mockRepository = new Mock<IBaseRepository<Person>>();
        var expectedPersons = new List<Person>
        {
            new Person(Guid.NewGuid(), "John Doe", "12345678901", "john@example.com", "1234567890"),
            new Person(Guid.NewGuid(), "Jane Doe", "23456789012", "jane@example.com", "2345678901")
        };

        mockRepository.Setup(repo => repo.GetAll(CancellationToken.None))
            .ReturnsAsync(expectedPersons);
        
        var result = await mockRepository.Object.GetAll(CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(expectedPersons, result);
    }

    [Fact]
    public async Task UpdatePerson_Success()
    {
        var mockRepository = new Mock<IBaseRepository<Person>>();
        var personId = Guid.NewGuid();
        var updatedPerson = new Person(personId, "John Doe", "12345678901", "john@example.com", "1234567890");
    
        mockRepository.Setup(repo => repo.Update(It.IsAny<Person>()))
            .Callback((Person person) =>
            {
                Assert.Equal(personId, person.Id);
            });
        
        mockRepository.Object.Update(updatedPerson);
        
        mockRepository.Verify(repo => repo.Update(updatedPerson), Times.Once);
    }

    [Fact]
    public async Task DeletePerson_Success()
    {
        var mockRepository = new Mock<IBaseRepository<Person>>();
        var personId = Guid.NewGuid();
        var deletedPerson = new Person { Id = personId };
        
        mockRepository.Setup(repo => repo.Delete(It.IsAny<Person>()))
            .Callback((Person person) =>
            {
                Assert.Equal(personId, person.Id);
            });
        
        mockRepository.Object.Delete(deletedPerson);
        
        mockRepository.Verify(repo => repo.Delete(deletedPerson), Times.Once);
    }
}