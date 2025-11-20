using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Core.DTO.GeneralGymDetail
{
    public class GeneralGymUpdateRequest
    {
        public string GymName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string ContactNumber { get; set; } = default!;
        public string PrimaryColor { get; set; } = default!;
        public string BackgroundColor { get; set; } = default!;
        public string SecondColor { get; set; } = default!;
    }
}
