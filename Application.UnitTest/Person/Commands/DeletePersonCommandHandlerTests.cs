using Application.Persons.Commands.DeletePerson;
using Domain.Abstractions;
using Moq;

namespace Application.UnitTest.Person.Commands;

public class DeletePersonCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldDeleteExistingPerson()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var personRepositoryMock = new Mock<IPersonRepository>();

        var existingPersonId = Guid.NewGuid();
        var existingPerson = new Domain.Entities.Person
        {
            Id = existingPersonId,
            Name = "Ted Mosby",
            Document = "12345678901",
            Email = "ted.mosby@example.com",
            Phone = "1234567890"
        };

        personRepositoryMock.Setup(repo => repo.Get(existingPersonId, CancellationToken.None))
            .ReturnsAsync(existingPerson);

        var handler = new DeletePersonCommandHandler(unitOfWorkMock.Object, personRepositoryMock.Object);

        var request = new DeletePersonCommand
        {
            Id = existingPersonId
        };
        
        var result = await handler.Handle(request, CancellationToken.None);
        
        personRepositoryMock.Verify(repo => repo.Delete(existingPerson), Times.Once);
        unitOfWorkMock.Verify(uow => uow.Commit(CancellationToken.None), Times.Once);
        
        Assert.Equal(existingPerson, result);
    }
}