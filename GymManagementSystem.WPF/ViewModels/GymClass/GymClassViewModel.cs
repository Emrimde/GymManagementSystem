using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ScheduledClass;
using GymManagementSystem.WPF.ViewModels.Staff.Models;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.GymClass;
public class GymClassViewModel : ViewModel
{
    private readonly GymClassHtppClient _httpClient;
    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation { get; set; }
    private ObservableCollection<GymClassResponse> _gymClasses = new();

    public ObservableCollection<StatusFilter> StatusFilters { get; } = new()
    {
        new StatusFilter()
        {
            Label = "Active",
            Value = true
        },
        new StatusFilter()
        {
            Label = "Unactive",
            Value = false
        },
        new StatusFilter()
        {
            Label = "All",
            Value = null
        },
    };

    private bool? _selectedStatusFilter;
    public bool? SelectedStatusFilter
    {
        get => _selectedStatusFilter;
        set
        {
            _selectedStatusFilter = value;
            OnPropertyChanged();
            LoadGymClassesCommand.Execute(null);
        }
    }


    public ICommand OpenAddGymClassCommand { get; }
    public ICommand OpenScheduledClassesViewCommand { get; }
    public ICommand OpenEditGymClassCommand { get; }
    public ICommand LoadGymClassesCommand { get; }
    public ICommand ActivateGymClassCommand { get; }
    public ICommand DeleteGymClassCommand { get; }
    public GymClassViewModel(GymClassHtppClient httpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _httpClient = httpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        OpenAddGymClassCommand = new RelayCommand(item => Navigation.NavigateTo<GymClassAddViewModel>(), item => true);
        LoadGymClassesCommand = new AsyncRelayCommand(item => LoadGymClasses(), item => true);
        DeleteGymClassCommand = new AsyncRelayCommand(item => DeleteGymClassAsync(item), item => true);
        ActivateGymClassCommand = new AsyncRelayCommand(item => RestoreGymClassAsync(item), item => true);
        OpenScheduledClassesViewCommand = new RelayCommand(item => Navigation.NavigateTo<ScheduledClassViewModel>(item!), item => true);
        OpenEditGymClassCommand = new RelayCommand(item => Navigation.NavigateTo<GymClassUpdateViewModel>(item!), item => true);
    }

    private async Task RestoreGymClassAsync(object parameter)
    {
        if (parameter is Guid gymClassId)
        {
            MessageBoxResult mbResult = MessageBox.Show($"Are you sure to restore this gym class?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mbResult == MessageBoxResult.Yes)
            {
                Result<Unit> result = await _httpClient.RestoreGymClassAsync(gymClassId);
                if (!result.IsSuccess)
                {
                    MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    LoadGymClassesCommand.Execute(null);
                }
            }
        }
    }

    private async Task DeleteGymClassAsync(object parameter)
    {
        if (parameter is Guid gymClassId)
        {
            MessageBoxResult mbResult = MessageBox.Show($"Are you sure to delete this gym class?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mbResult == MessageBoxResult.Yes)
            {
                Result<Unit> result = await _httpClient.DeleteGymClassAsync(gymClassId);
                if (!result.IsSuccess)
                {
                    MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    LoadGymClassesCommand.Execute(null);
                }
            }
        }
    }

    private async Task LoadGymClasses()
    {
        Result<ObservableCollection<GymClassResponse>> result = await _httpClient.GetGymClasses(SelectedStatusFilter);
        if (result.IsSuccess)
        {
            GymClasses = result.Value!;
        }
        else
        {
            MessageBox.Show($"Error: {result.GetUserMessage()}");
        }
    }

    public ObservableCollection<GymClassResponse> GymClasses
    {
        get { return _gymClasses; }
        set { _gymClasses = value; OnPropertyChanged(); }
    }
}
