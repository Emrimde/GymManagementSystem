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
            SecondColor = generalGymDetail.SecondColor
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
            SecondColor = generalGymUpdateRequest.SecondColor
        };
    }
}
