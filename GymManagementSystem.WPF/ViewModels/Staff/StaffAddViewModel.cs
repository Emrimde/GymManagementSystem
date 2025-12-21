using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
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
        PersonAdd = new PersonAddRequest();
        AddPersonAsyncCommand = new AsyncRelayCommand(item => AddPersonAsync(), item => true);
    }

    private async Task AddPersonAsync()
    {
        Result<PersonInfoResponse> result = await _staffHttpClient.PostPersonToStaffAsync(PersonAdd);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
            return;
        }
        // person details nawigacja
    }

    public PersonAddRequest PersonAdd {  get; set; }
    public INavigationService Navigation { get; set; }
    public StaffHttpClient _staffHttpClient { get; set; }
    public SidebarViewModel SidebarView { get; set; }
    public ICommand AddPersonAsyncCommand { get; set; }



}
