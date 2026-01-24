using FluentValidation;
using GymManagementSystem.Core.DTO.MembershipFeature;

namespace GymManagementSystem.Core.Validators.MembershipFeature;
public class MembershipFeatureAddRequestValidator : AbstractValidator<MembershipFeatureAddRequest>
{
    public MembershipFeatureAddRequestValidator()
    {
        RuleFor(item => item.MembershipId).NotEmpty().WithMessage("MembershipId is empty");
        RuleFor(item => item.FeatureDescription).NotEmpty().WithMessage("Feature description is empty");
    }
}
