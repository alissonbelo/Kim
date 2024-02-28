using Application.Persons.Commands.CreatePerson;
using Domain.Abstractions;
using Moq;

namespace Application.UnitTest.Person.Commands;

public class CreatePersonCommandHandlerTests
{[Fact]
    public async Task Handle_ShouldCreateNewPerson()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var personRepositoryMock = new Mock<IPersonRepository>();
        var personRegistrationQueueMock = new Mock<IPersonRegistrationQueue>();

        var handler = new CreatePersonCommandHandler(unitOfWorkMock.Object, personRepositoryMock.Object, personRegistrationQueueMock.Object);

        var request = new CreatePersonCommand
        {
            Name = "Ted Mosby",
            Document = "11111111111",
            Email = "ted.mnosby@example.com",
            Phone = "1111111111"
        };

        var result = await handler.Handle(request, CancellationToken.None);
        
        personRepositoryMock.Verify(repo => repo.Create(It.IsAny<Domain.Entities.Person>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.Commit(CancellationToken.None), Times.Once);
        personRegistrationQueueMock.Verify(queue => queue.EnqueueAsync(It.IsAny<Domain.Entities.Person>(), CancellationToken.None), Times.Once);
        
        Assert.IsType<Domain.Entities.Person>(result);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Document, result.Document);
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(request.Phone, result.Phone);
    }
}