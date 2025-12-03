using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.TrainerContract;


namespace GymManagementSystem.Core.Mappers;
public static class TrainerContractMapper
{
    public static TrainerContract ToTrainerContract(this TrainerContractAddRequest request)
    {
        return new TrainerContract()
        {
            ClubCommissionPercent = request.ClubCommissionPercent,
            CompanyAddress = request.CompanyAddress,
            CompanyName = request.CompanyName,
            ContractType = request.ContractType,
            TrainerType = request.TrainerType,
            Person = new Person()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
            },
            ValidFrom = request?.ValidFrom ??  DateTime.UtcNow,
            ValidTo = request?.ValidTo ?? null,
            
        };
    }

    public static TrainerContractInfoResponse ToTrainerContractInfoResponse(this TrainerContract trainerContract)
    {
        return new TrainerContractInfoResponse()
        {
            Id = trainerContract.Id,
        };
    }

    public static TrainerContractResponse ToTrainerContractResponse(this TrainerContract trainerContract)
    {
        return new TrainerContractResponse()
        {
            ContractType = trainerContract.ContractType,
            FirstName = trainerContract.Person?.FirstName,
            LastName= trainerContract.Person?.LastName,
            PhoneNumber= trainerContract.Person?.PhoneNumber,
            Id = trainerContract.Id,
            TrainerType = trainerContract.TrainerType
            
        };
    }
}
