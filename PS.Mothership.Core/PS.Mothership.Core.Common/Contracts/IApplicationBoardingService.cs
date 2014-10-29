using System;
using System.ServiceModel;
using PS.Mothership.Core.Common.Dto.Application;

namespace PS.Mothership.Core.Common.Contracts
{
    [ServiceContract(Name = "ApplicationBoardingService")]
    public interface IApplicationBoardingService
    {
        [OperationContract]
        ApplicationOpportunitiesDto GetApplicationOpportunities(Guid applicationGuid);

        [OperationContract]
        ApplicationOpportunitiesDto SaveApplicationOpportunities(ApplicationOpportunitiesDto applicationOpportunitiesDto);

        [OperationContract]
        ApplicationOpportunitiesMetadataDto GetApplicationOpportunitiesMetadata();

        [OperationContract]
        ApplicationDetailsDto GetApplicationDetails(Guid applicationGuid);

        [OperationContract]
        ApplicationDetailsDto SaveApplicationDetails(ApplicationDetailsDto applicationDetailsDto);

        [OperationContract]
        ApplicationOpportunitiesMetadataDto GetApplicationDetailMetadata();
    }
}