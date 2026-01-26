using GymManagementSystem.Core.DTO.EmploymentTermination;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace GymManagementSystem.WPF.ViewModels.EmploymentTermination;
public class EmploymentTerminationViewModel : ViewModel
{
    private readonly EmploymentTerminationHttpClient _employmentHttpClient;
    public SidebarViewModel SidebarView { get; set; }
    public ObservableCollection<EmploymentTerminationResponse> EmploymentTerminations { get; set; } = new();

    public EmploymentTerminationViewModel(EmploymentTerminationHttpClient employmentTerminationHttpClient, SidebarViewModel sidebarView)
    {
        EmploymentTerminations = new ObservableCollection<EmploymentTerminationResponse>();
        _employmentHttpClient = employmentTerminationHttpClient;
        SidebarView = sidebarView;
        _ = LoadEmploymentTerminations();
    }

    private async Task LoadEmploymentTerminations()
    {
        Result<ObservableCollection<EmploymentTerminationResponse>> result = await _employmentHttpClient.GetAllEmploymentTerminationsAsync();
        if (result.IsSuccess)
        {
            foreach (var termination in result.Value!)
            {
                EmploymentTerminations.Add(termination);
            }
        }
        else
        {
            MessageBox.Show($"Error: {result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
