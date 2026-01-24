using FluentValidation;
using GymManagementSystem.Core.DTO.MembershipFeature;

namespace GymManagementSystem.Core.Validators.MembershipFeature;
public class MembershipFeatureUpdateRequestValidator : AbstractValidator<MembershipFeatureUpdateRequest>
{
    public MembershipFeatureUpdateRequestValidator()
    {
        RuleFor(item => item.MembershipFeatureId).NotEmpty().WithMessage("MembershipId is empty");
        RuleFor(item => item.FeatureDescription).NotEmpty().WithMessage("Feature description is empty");
    }
}
