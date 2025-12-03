using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Core.Enum;
// typ umowy do generowania 
public enum ContractTypeEnum
{
    [Display(Name = "Probation")]
    Probation,

    [Display(Name = "Permanent")]
    Permanent,

    [Display(Name = "Fixed Term")]
    FixedTerm,

    [Display(Name = "Contract of Mandate")]
    ContractOfMandate,

    [Display(Name = "B2B")]
    B2B
}
