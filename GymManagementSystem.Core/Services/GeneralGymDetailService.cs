using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;

using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class GeneralGymDetailService : IGeneralGymDetailsService
{
    private readonly IGeneralGymRepository _generalGymRepository;
    public GeneralGymDetailService(IGeneralGymRepository generalGymRepository)
    {
        _generalGymRepository = generalGymRepository;
    }

    public async Task<Result<GeneralGymResponse>> GetSettingsByIdAsync(CancellationToken cancellationToken)
    {
        GeneralGymDetail? generalGymDetail = await _generalGymRepository.GetGeneralGymDetailsAsync(cancellationToken);
        if(generalGymDetail == null)
        {
            return Result<GeneralGymResponse>.Failure($"General gym detail not found");
        }

        return Result<GeneralGymResponse>.Success(generalGymDetail.ToGeneralGymResponse());
    }

    public async Task<Result<GeneralGymResponse>> UpdateSettingsAsync(GeneralGymUpdateRequest request, CancellationToken cancellationToken)
    {
        GeneralGymDetail? generalGymDetail = await _generalGymRepository.UpdateSettingsAsync(request,cancellationToken);
        if (generalGymDetail == null)
        {
            return Result<GeneralGymResponse>.Failure("Failed to update settings", StatusCodeEnum.InternalServerError);
        }
        return Result<GeneralGymResponse>.Success(generalGymDetail.ToGeneralGymResponse(), StatusCodeEnum.Ok);
    }
}
