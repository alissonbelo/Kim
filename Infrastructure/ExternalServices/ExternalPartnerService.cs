using Domain.Abstractions;
using Domain.Entities;

namespace Infrastructure.ExternalServices;

public class ExternalPartnerService : IExternalPartnerService
{
    public async Task<bool> RegisterPersonAsync(Person person)
    {
        try
        {
            return await Task.FromResult(true);
            //return await Task.FromResult(false);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao registrar pessoa no serviço externo.", ex);
        }
    }
    
    
    public Task<string> GetDataAsync()
    {
        return Task.FromResult("Dados do primeiro serviço externo");
    }
}