using GymManagementSystem.Core.Domain;
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
    private readonly IUnitOfWork _unitOfWork;
    public GeneralGymDetailService(IGeneralGymRepository generalGymRepository, IUnitOfWork unitOfWork)
    {
        _generalGymRepository = generalGymRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GeneralGymResponse>> GetSettingsByIdAsync()
    {
        GeneralGymDetail? generalGymDetail = await _generalGymRepository.GetGeneralGymDetailsAsync();
        if(generalGymDetail == null)
        {
            return Result<GeneralGymResponse>.Failure($"General gym detail not found");
        }

        return Result<GeneralGymResponse>.Success(generalGymDetail.ToGeneralGymResponse());
    }

    public async Task<Result<GeneralGymResponse>> UpdateSettingsAsync(GeneralGymUpdateRequest request)
    {
        GeneralGymDetail? generalGymDetail = await _generalGymRepository.GetGeneralGymDetailsAsync();
        if(generalGymDetail == null)
        {
            return Result<GeneralGymResponse>.Failure("General gym detail not found", StatusCodeEnum.NotFound);
        }

        generalGymDetail.UpdateGeneralGymDetail(request);
        await _unitOfWork.SaveChangesAsync();
        return Result<GeneralGymResponse>.Success(generalGymDetail.ToGeneralGymResponse(), StatusCodeEnum.Ok);
    }
}
