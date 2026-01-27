using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;


namespace GymManagementSystem.Core.Mappers;
public static class TrainerContractMapper
{
    public static TrainerContract ToTrainerContract(this TrainerContractAddRequest request)
    {
        return new TrainerContract()
        {
            ClubCommissionPercent = request.ClubCommissionPercent,
            ContractType = request.ContractType,
            TrainerType = request.TrainerType,
            PersonId = request.PersonId,
        };
    }

    public static TrainerContractInfoResponse ToTrainerContractInfoResponse(this TrainerContract trainerContract)
    {
        return new TrainerContractInfoResponse()
        {
            Id = trainerContract.Id,
            FullName = trainerContract.Person?.FirstName  + " " + trainerContract.Person?.LastName
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
    public static TrainerContractDetailsResponse ToTrainerContractDetailsResponse(this TrainerContract trainerContract)
    {
        return new TrainerContractDetailsResponse()
        {
            ContractType = "Contract of mandate",
            FirstName = trainerContract.Person?.FirstName,
            LastName = trainerContract.Person?.LastName,
            PhoneNumber = trainerContract.Person?.PhoneNumber,
            Email = trainerContract.Person?.Email,
            ClubCommissionPercent = trainerContract.ClubCommissionPercent.ToString() + "%",
            Id = trainerContract.Id,
            TrainerType = trainerContract.TrainerType == TrainerTypeEnum.PersonalTrainer ? "Personal trainer" : "Group instructor",
            CanShowBooking = trainerContract.TrainerType == TrainerTypeEnum.PersonalTrainer && trainerContract.ValidFrom <= DateTime.UtcNow && !(trainerContract.Person?.EmploymentTerminations.Any(item => item.EffectiveDate.Date <= DateTime.UtcNow.Date) ?? false),
            Valid = trainerContract.ValidFrom.ToString("dd.MM:yyyy") + "-" + (trainerContract.ValidTo?.ToString("dd.MM:yyyy") ?? "Permanent"),
            PersonId = trainerContract.PersonId,
            CanTerminate = !(trainerContract.Person?
                                            .EmploymentTerminations
                                            .Any(item => item.EffectiveDate > DateTime.UtcNow) ?? true),
        };
    }
}
