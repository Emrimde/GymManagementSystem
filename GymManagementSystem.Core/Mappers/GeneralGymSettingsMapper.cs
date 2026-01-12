using GymManagementSystem.Core.DTO.GeneralGymDetail;

namespace GymManagementSystem.Core.Mappers;
public static class GeneralGymSettingsMapper
{
    public static GeneralGymUpdateRequest ToGeneralGymUpdateRequest(this GeneralGymResponse settings)
    {
        return new GeneralGymUpdateRequest()
        {
            Address = settings.Address,
            AboutUs = settings.AboutUs,
            ContactNumber = settings.ContactNumber,
            GymName = settings.GymName,
            BackgroundColor = settings.BackgroundColor,
            PrimaryColor = settings.PrimaryColor,
            SecondColor = settings.SecondColor,
            DefaultRate60 = settings.DefaultRate60,
            DefaultRate90 = settings.DefaultRate90,
            DefaultRate120 = settings.DefaultRate120,
            LogoUrl = settings.LogoUrl
        };
    }
}
