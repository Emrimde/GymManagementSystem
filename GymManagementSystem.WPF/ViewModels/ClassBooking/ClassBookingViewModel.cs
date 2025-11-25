using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;

namespace GymManagementSystem.WPF.ViewModels.ClassBooking;
public class ClassBookingViewModel : ViewModel
{
    private readonly ClassBookingHttpClient _httpClient;
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ClassBookingResponse> ClassBookings { get; set; }
    public SidebarViewModel SidebarView { get; }
    public ClassBookingViewModel(ClassBookingHttpClient httpClient, INavigationService navigation,SidebarViewModel sidebarViewModel)
    {
        SidebarView = sidebarViewModel;
        _httpClient = httpClient;
        Navigation = navigation;
        ClassBookings = new ObservableCollection<ClassBookingResponse>();
        _ = LoadClassBookings();
    }

    private async Task LoadClassBookings()
    {
        Result<ObservableCollection<ClassBookingResponse>> result = await _httpClient.GetClassBookings();

        if (!result.IsSuccess) 
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        foreach (ClassBookingResponse classBooking in result.Value!)
        {
            ClassBookings.Add(classBooking);
        }
    }
}
