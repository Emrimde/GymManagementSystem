using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Staff.Models;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Staff;
public class StaffAddViewModel : ViewModel
{
    public StaffAddViewModel(INavigationService navigation, StaffHttpClient staffHttpClient, SidebarViewModel sidebarView)
    {
        Navigation = navigation;
        _staffHttpClient = staffHttpClient;
        SidebarView = sidebarView;
        AddPersonAsyncCommand = new AsyncRelayCommand(item => AddPersonAsync(), item => CanAddPerson());
    }

    private bool CanAddPerson()
    {
        return PersonAdd.IsFormComplete && !PersonAdd.HasErrors;
    }

    private async Task AddPersonAsync()
    {
        PersonAddRequest request = new PersonAddRequest()
        {
            City = PersonAdd.City,
            Email = PersonAdd.Email,
            FirstName = PersonAdd.FirstName,
            LastName = PersonAdd.LastName,
            PhoneNumber = PersonAdd.PhoneNumber,
            Street = PersonAdd.Street,
        };

        Result<PersonInfoResponse> result = await _staffHttpClient.PostPersonToStaffAsync(request);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.GetUserMessage());
            return;
        }
        Navigation.NavigateTo<StaffViewModel>();
    }

    public PersonAddForm PersonAdd { get; set; } = new();
    public INavigationService Navigation { get; set; }
    public StaffHttpClient _staffHttpClient { get; set; }
    public SidebarViewModel SidebarView { get; set; }
    public ICommand AddPersonAsyncCommand { get; set; }

}
