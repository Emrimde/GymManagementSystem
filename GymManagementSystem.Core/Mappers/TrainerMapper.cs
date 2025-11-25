using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Trainer;

namespace GymManagementSystem.Core.Mappers;
public static class TrainerMapper
{
    public static TrainerResponse ToTrainerResponse(this Trainer trainer)
    {
        return new TrainerResponse()
        {
            CreatedAt = trainer.CreatedAt.ToString("dd.MM.yyyy"),
            DeletedAt = trainer.DeletedAt?.ToString("dd.MM.yyyy") ?? string.Empty,
            UpdatedAt = trainer.UpdatedAt.ToString("dd.MM.yyyy"),
            Email = trainer.Email,
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
            PhoneNumber = trainer.PhoneNumber,
            Id = trainer.Id,
            IsActive = trainer.IsActive,
        };
    }

    public static TrainerDetailsResponse ToTrainerDetailsResponse(this Trainer trainer)
    {
        return new TrainerDetailsResponse()
        {
            CreatedAt = trainer.CreatedAt.ToString("dd.MM.yyyy"),
            DeletedAt = trainer.DeletedAt?.ToString("dd.MM.yyyy") ?? string.Empty,
            UpdatedAt = trainer.UpdatedAt.ToString("dd.MM.yyyy"),
            Email = trainer.Email,
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
            PhoneNumber = trainer.PhoneNumber,
            Id = trainer.Id,
            IsActive = trainer.IsActive,
        };
    }

    public static Trainer ToTrainer(this TrainerAddRequest trainer)
    {
        return new Trainer()
        {

            Email = trainer.Email,
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
            PhoneNumber = trainer.PhoneNumber,
        };
    }
    public static Trainer ToTrainer(this TrainerUpdateRequest trainer)
    {
        return new Trainer()
        {
            Email = trainer.Email,
            PhoneNumber = trainer.PhoneNumber,
        };
    }

    public static TrainerInfoResponse ToTrainerInfoResponse(this Trainer trainer)
    {
        return new TrainerInfoResponse()
        {
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
        };
    }
}
