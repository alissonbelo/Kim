using Domain.Entities;

namespace Domain.Abstractions;

public interface IExternalPartnerService
{
    Task<bool> RegisterPersonAsync(Person person);
    Task<string> GetDataAsync();
}

public interface ISecondExternalPartnerService
{
    Task<bool> RegisterPersonAsync(Person person);
    Task<string> GetDataAsync();
}