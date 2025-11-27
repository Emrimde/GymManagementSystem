using GymManagementSystem.WPF.ViewModels.Trainer;
using Microsoft.VisualBasic;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Trainer
{
    /// <summary>
    /// Logika interakcji dla klasy TrainerScheduleView.xaml
    /// </summary>
    public partial class TrainerScheduleView : UserControl
    {
        public TrainerScheduleView()
        {
            InitializeComponent();
            Schedule.DaysViewSettings.TimeInterval = new System.TimeSpan(0, 15, 0);
            Schedule.DaysViewSettings.MinimumAppointmentDuration = new System.TimeSpan(0, 60, 0);
            Schedule.DaysViewSettings.TimeRulerFormat = "hh mm";
        }

        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
            if(DataContext is TrainerScheduleViewModel vm)
            {
                vm.OpenEditorCommand.Execute(e);
            }
        }
    }
}
