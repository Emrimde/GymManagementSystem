using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.WebDTO.GeneralGymDetail;

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
            DefaultGroupClassRate = generalGymDetail.DefaultGroupClassRate,
            LogoUrl = generalGymDetail.LogoUrl,
            AboutUs = generalGymDetail.AboutUs
        };
    }
    public static GeneralPublicProfileResponse ToGeneralPublicProfileResponse(this GeneralGymDetail generalGymDetail)
    {
        return new GeneralPublicProfileResponse
        {
            GymName = generalGymDetail.GymName,
            Address = generalGymDetail.Address,
            ContactNumber = generalGymDetail.ContactNumber,
            LogoUrl = generalGymDetail.LogoUrl,
        };
    }

    public static AboutUsResponse ToAboutUsResponse(this GeneralGymDetail generalGymDetail)
    {
        return new AboutUsResponse
        {
            AboutUsContent = generalGymDetail.AboutUs,
        };
    }

    public static void UpdateGeneralGymDetail(this GeneralGymDetail generalGymDetail, GeneralGymUpdateRequest generalUpdateRequest)
    {
            generalGymDetail.GymName = generalUpdateRequest.GymName;
            generalGymDetail.Address = generalUpdateRequest.Address;
            generalGymDetail.ContactNumber = generalUpdateRequest.ContactNumber;
            generalGymDetail.BackgroundColor = generalUpdateRequest.BackgroundColor;
            generalGymDetail.PrimaryColor = generalUpdateRequest.PrimaryColor;
            generalGymDetail.SecondColor = generalUpdateRequest.SecondColor;
            generalGymDetail.DefaultRate60 = generalUpdateRequest.DefaultRate60;
            generalGymDetail.DefaultRate120 = generalUpdateRequest.DefaultRate120;
            generalGymDetail.DefaultRate90 = generalUpdateRequest.DefaultRate90;
            generalGymDetail.AboutUs = generalUpdateRequest.AboutUs;
            generalGymDetail.LogoUrl = generalUpdateRequest.LogoUrl;
    }
}
