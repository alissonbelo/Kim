using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Persons.Queries.GetExternalPersonData;

public class GetExternalDataQuery :  IRequest<IEnumerable<string>>
{
    public class GetExternalDataServiceAQueryHandler : IRequestHandler<GetExternalDataQuery, IEnumerable<string>>
    {
        private readonly IExternalPartnerService _externalDataService;
        private readonly ISecondExternalPartnerService _secondExternalPartnerService;

        public GetExternalDataServiceAQueryHandler(IExternalPartnerService externalDataService,
            ISecondExternalPartnerService secondExternalPartnerService)
        {
            _externalDataService = externalDataService;
            _secondExternalPartnerService = secondExternalPartnerService;
        }

        public async Task<IEnumerable<string>> Handle(GetExternalDataQuery request, CancellationToken cancellationToken)
        {
            var dataFromServiceA = await _externalDataService.GetDataAsync();
            var dataFromServiceB = await _secondExternalPartnerService.GetDataAsync();
            
            var combinedData = new List<string> { dataFromServiceA, dataFromServiceB };

            return combinedData;
        }
    }
}


