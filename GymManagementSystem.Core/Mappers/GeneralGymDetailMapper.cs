using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.GeneralGymDetail;

namespace GymManagementSystem.Core.Mappers;

public static class GeneralGymDetailMapper
{
    public static GeneralGymResponse ToGeneralGymResponse(this GeneralGymDetail generalGymDetail)
    {
        return new GeneralGymResponse
        {
            Id = generalGymDetail.Id,
            GymName = generalGymDetail.GymName,
            Address = generalGymDetail.Address,
            ContactNumber = generalGymDetail.ContactNumber,
            BackgroundColor = generalGymDetail.BackgroundColor,
            PrimaryColor = generalGymDetail.PrimaryColor,
            SecondColor = generalGymDetail.SecondColor,
            DefaultRate90 = generalGymDetail.DefaultRate90,
            DefaultRate120 = generalGymDetail.DefaultRate120,
            DefaultRate60 = generalGymDetail.DefaultRate60,
            DefaultGroupClassRate = generalGymDetail.DefaultGroupClassRate
        };
    }

    public static GeneralGymDetail ToGeneralGymDetail(this GeneralGymUpdateRequest generalGymUpdateRequest)
    {
        return new GeneralGymDetail
        {
            GymName = generalGymUpdateRequest.GymName,
            Address = generalGymUpdateRequest.Address,
            ContactNumber = generalGymUpdateRequest.ContactNumber,
            BackgroundColor = generalGymUpdateRequest.BackgroundColor,
            PrimaryColor = generalGymUpdateRequest.PrimaryColor,
            SecondColor = generalGymUpdateRequest.SecondColor,
            DefaultRate60 = generalGymUpdateRequest.DefaultRate60,
            DefaultRate120 = generalGymUpdateRequest.DefaultRate120,
            DefaultRate90 = generalGymUpdateRequest.DefaultRate90
        };
    }
}
