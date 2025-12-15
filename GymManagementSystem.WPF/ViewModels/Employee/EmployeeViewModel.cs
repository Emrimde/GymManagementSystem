using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Employee;
public class EmployeeViewModel : ViewModel
{
    private int _currentPage;
    public int CurrentPage
    {
        get { return _currentPage; }
        set
        {
            _currentPage = value; OnPropertyChanged();

            OnPropertyChanged(nameof(CanGoPrevious));
            OnPropertyChanged(nameof(CanGoNext));
            OnPropertyChanged(nameof(VisiblePages));
            OnPropertyChanged(nameof(start));
            OnPropertyChanged(nameof(end));
        }
    }

    private int _totalPages;

    public int TotalPages
    {
        get { return _totalPages; }
        set
        {
            _totalPages = value; OnPropertyChanged();

            OnPropertyChanged(nameof(CanGoNext));
            OnPropertyChanged(nameof(VisiblePages));
        }
    }

    public bool CanGoNext => CurrentPage < TotalPages;
    public bool CanGoPrevious => CurrentPage > 1;
    private int start => Math.Max(1, CurrentPage - 2);
    private int end => Math.Min(TotalPages, CurrentPage + 2);

    private int count => end - start + 1;
    public List<int> VisiblePages => Enumerable.Range(start, count).ToList();

    private string _searchText;

    public string SearchText
    {
        get { return _searchText; }
        set { _searchText = value; OnPropertyChanged(); }
    }

    public ICommand SearchEmployeesCommand { get; }
    private readonly EmployeeHttpClient _employeeHttpClient;
    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; }
    }
    public SidebarViewModel SidebarView { get; set; }

    private ObservableCollection<EmployeeResponse> _employees;
    public ObservableCollection<EmployeeResponse> Employees
    {
        get { return _employees; }
        set
        {
            if (_employees != value)
            {
                _employees = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand OpenAddEmployeeViewCommand { get; }

    public EmployeeViewModel(EmployeeHttpClient employeeHttpClient, INavigationService navigation, SidebarViewModel sidebar)
    {
        CurrentPage = 1;
        TotalPages = 1;
        _employeeHttpClient = employeeHttpClient;
        Navigation = navigation;
        SidebarView = sidebar;
        OpenAddEmployeeViewCommand = new RelayCommand(item => OpenAddEmployeeView(), item => true);
        Employees = new ObservableCollection<EmployeeResponse>();
        _ = LoadEmployees();
        SearchEmployeesCommand = new AsyncRelayCommand(item => SearchEmployees(), item => true);
    }

    private async Task SearchEmployees()
    {
        Result<ObservableCollection<EmployeeResponse>> result = await _employeeHttpClient.GetAllEmployeesAsync(SearchText);
        if (result.IsSuccess)
        {
            Employees = result.Value!;
        }
        else
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
    }

    private void OpenAddEmployeeView()
    {
        Navigation.NavigateTo<EmployeeDecisionViewModel>();
    }

    private async Task LoadEmployees()
    {
        Result<ObservableCollection<EmployeeResponse>> result = await _employeeHttpClient.GetAllEmployeesAsync();
        if (result.IsSuccess)
        {
            Employees = result.Value!;
        }
        else
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
    }
}
