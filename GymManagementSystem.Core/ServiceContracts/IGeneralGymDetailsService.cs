using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO.GeneralGymDetail;
using Microsoft.AspNetCore.Http;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IGeneralGymDetailsService
{
    Task<Result<AboutUsResponse?>> GetPublicAboutUsAsync();
    Task<Result<GeneralPublicProfileResponse?>> GetPublicGymProfileAsync();
    Task<Result<GeneralGymResponse>> GetSettingsByIdAsync();
    Task<Result<GeneralGymResponse>> UpdateSettingsAsync(GeneralGymUpdateRequest request);
    Task<Result<string>> UploadLogoAsync(IFormFile file);
}
