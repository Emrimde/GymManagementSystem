using FluentValidation;
using GymManagementSystem.Core.DTO.ClientMembership;

namespace GymManagementSystem.Core.Validators.ClientMembership;
public class ClientMembershipAddRequestValidator : AbstractValidator<ClientMembershipAddRequest>
{
    public ClientMembershipAddRequestValidator()
    {
        RuleFor(item => item.ClientId)
            .NotEmpty().WithMessage("ClientId is required.");
        RuleFor(item => item.MembershipId)
            .NotEmpty().WithMessage("MembershipId is required.");
    }
}
