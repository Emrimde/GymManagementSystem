using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.GeneralGymDetail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GymManagementSystem.Core.Services;

public class GeneralGymDetailService : IGeneralGymDetailsService
{
    private readonly IGeneralGymRepository _generalGymRepository;
    private readonly IWebHostEnvironment _env;
    private readonly IUnitOfWork _unitOfWork;
    public GeneralGymDetailService(IGeneralGymRepository generalGymRepository, IUnitOfWork unitOfWork, IWebHostEnvironment env)
    {
        _generalGymRepository = generalGymRepository;
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<Result<AboutUsResponse?>> GetPublicAboutUsAsync()
    {
        GeneralGymDetail? generalGymDetail = await _generalGymRepository.GetGeneralGymDetailsAsync();
        return Result<AboutUsResponse?>.Success(generalGymDetail?.ToAboutUsResponse());
    }

    public async Task<Result<GeneralPublicProfileResponse?>> GetPublicGymProfileAsync()
    {
        GeneralGymDetail? generalGymDetail = await _generalGymRepository.GetGeneralGymDetailsAsync();
        return Result<GeneralPublicProfileResponse?>.Success(generalGymDetail?.ToGeneralPublicProfileResponse());
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

    public async Task<Result<string>> UploadLogoAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return Result<string>.Failure("",StatusCodeEnum.BadRequest);

        var ext = Path.GetExtension(file.FileName);
        var fileName = $"logo_{Guid.NewGuid()}{ext}";

        var folder = Path.Combine(_env.WebRootPath, "uploads", "logos");
        Directory.CreateDirectory(folder);

        var path = Path.Combine(folder, fileName);

        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);

        var url = $"/uploads/logos/{fileName}";
        return Result<string>.Success(url, StatusCodeEnum.Ok);
    }
}
