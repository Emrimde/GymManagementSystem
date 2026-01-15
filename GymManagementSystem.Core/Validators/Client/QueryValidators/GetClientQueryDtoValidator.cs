using FluentValidation;
using GymManagementSystem.Core.DTO.Client.QueryDto;

namespace GymManagementSystem.Core.Validators.Client.QueryValidators;
public class GetClientQueryDtoValidator : AbstractValidator<GetClientQueryDto>
{
    public GetClientQueryDtoValidator()
    {
        RuleFor(item => item.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");
        RuleFor(item => item.SearchText)
            .MaximumLength(100).WithMessage("Search text must not exceed 100 characters.");
    }
}
