using GymManagementSystem.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Core.DTO.Person.ReadModel
{
    public class PersonReadModel
    {
        public Guid Id { get; set; }
        public Guid? TrainerContractId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public bool HasEmployee { get; set; }
        public bool HasTrainer { get; set; }
        public EmployeeRole? EmployeeRole { get; set; }
        public TrainerTypeEnum? TrainerTypeEnum { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
