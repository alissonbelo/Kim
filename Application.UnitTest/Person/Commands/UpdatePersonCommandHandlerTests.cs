using Application.Persons.Commands.UpdatePerson;
using Domain.Abstractions;
using Moq;

namespace Application.UnitTest.Person.Commands;

public class UpdatePersonCommandHandlerTests
{        
    [Fact]
    public async Task Handle_ShouldUpdateExistingPerson()
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

         var handler = new UpdatePersonCommandHandler(unitOfWorkMock.Object, personRepositoryMock.Object);

         var request = new UpdatePersonCommand
         {
            Id = existingPersonId,
            Name = "Robin Scherbatsky",
            Document = "10987654321",
            Email = "robin.scherbatsky@example.com",
            Phone = "0987654321"
         };
            
         var result = await handler.Handle(request, CancellationToken.None);
         
         personRepositoryMock.Verify(repo => repo.Update(existingPerson), Times.Once);
         unitOfWorkMock.Verify(uow => uow.Commit(CancellationToken.None), Times.Once);
            
         Assert.IsType<Domain.Entities.Person>(result);
         Assert.Equal(request.Name, result.Name);
         Assert.Equal(request.Document, result.Document);
         Assert.Equal(request.Email, result.Email);
         Assert.Equal(request.Phone, result.Phone);
    }
}