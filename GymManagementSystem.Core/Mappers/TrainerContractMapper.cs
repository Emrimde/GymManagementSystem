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
            TaxId = request?.TaxId
            
        };
    }

    public static TrainerContractInfoResponse ToTrainerContractInfoResponse(this TrainerContract trainerContract)
    {
        return new TrainerContractInfoResponse()
        {
            Id = trainerContract.Id,
            FullName = trainerContract.Person?.FirstName ?? "" + " " + trainerContract.Person?.LastName ?? ""
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
            ContractType = trainerContract.ContractType,
            FirstName = trainerContract.Person?.FirstName,
            LastName = trainerContract.Person?.LastName,
            PhoneNumber = trainerContract.Person?.PhoneNumber,
            Email = trainerContract.Person?.Email,
            ClubCommissionPercent = trainerContract.ClubCommissionPercent.ToString() + "%",
            CompanyAddress = trainerContract.CompanyAddress,
            CompanyName = trainerContract.CompanyName,
            IsSigned = trainerContract.IsSigned ? "Signed" : "Unsigned",
            TaxId = trainerContract.TaxId,
            Id = trainerContract.Id,
            TrainerType = trainerContract.TrainerType,
            IsB2B = trainerContract.ContractType == ContractTypeEnum.B2B ? true : false,
            CanShowBooking = trainerContract.TrainerType == TrainerTypeEnum.PersonalTrainer && trainerContract.ValidFrom <= DateTime.UtcNow && !(trainerContract.Person?.EmploymentTerminations.Any(item => item.EffectiveDate.Date <= DateTime.UtcNow.Date) ?? false),
            Valid = trainerContract.ValidFrom.ToString("yyyy:MM:dd") + "-" + (trainerContract.ValidTo?.ToString("yyyy:MM:dd") ?? "Permanent"),
            PersonId = trainerContract.PersonId,
            CanTerminate = !(trainerContract.Person?
                                            .EmploymentTerminations
                                            .Any(item => item.EffectiveDate > DateTime.UtcNow) ?? true),

        };
    }
}
