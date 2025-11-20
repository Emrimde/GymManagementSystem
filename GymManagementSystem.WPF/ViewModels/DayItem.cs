using GymManagementSystem.Core.Enum;
using GymManagementSystem.WPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.WPF.ViewModels
{
    public class DayItem : ObservableObject
    {
        public DaysOfWeekFlags Day { get; set; }
        public bool IsSelected { get; set; }
    }
}
