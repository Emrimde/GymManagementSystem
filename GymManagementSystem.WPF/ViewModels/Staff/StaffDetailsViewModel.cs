using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Windows;

namespace GymManagementSystem.WPF.ViewModels.Staff;
public class StaffDetailsViewModel : ViewModel, IParameterReceiver
{
    private readonly StaffHttpClient _staffHttpClient;
    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation { get; set; }

    private PersonDetailsResponse _person;

    public StaffDetailsViewModel(StaffHttpClient staffHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _staffHttpClient = staffHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        Person = new PersonDetailsResponse();

    }

    public PersonDetailsResponse Person
    {
        get { return _person; }
        set { _person = value; OnPropertyChanged(); }
    }


    public Guid PersonId { get; set; }
    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid personId)
        {
            PersonId = personId;
            _ = LoadPersonDetailsAsync();
        }
    }

    private async Task LoadPersonDetailsAsync()
    {
        Result<PersonDetailsResponse> result = await _staffHttpClient.GetPersonDetailsAsync(PersonId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
            return;
        }
        Person = result.Value!;
    }
}
