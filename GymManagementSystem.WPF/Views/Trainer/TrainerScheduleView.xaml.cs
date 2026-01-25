using GymManagementSystem.WPF.ViewModels.Trainer;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Trainer;
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

    private void Schedule_AppointmentDropping(object sender, AppointmentDroppingEventArgs e)
    {
        e.Cancel = true;
    }



    private void Schedule_AppointmentDeleting(object sender, AppointmentDeletingEventArgs e)
    {
        e.Cancel = true;
    }


    private void Schedule_AppointmentDragStarting(object sender, AppointmentDragStartingEventArgs e)
    {
        e.Cancel = true;
    }
}
