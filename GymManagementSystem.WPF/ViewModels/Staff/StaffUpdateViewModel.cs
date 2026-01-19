using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client.Models;
using GymManagementSystem.WPF.ViewModels.Staff.Models;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Staff;
public class StaffUpdateViewModel : ViewModel , IParameterReceiver
{

    public ICommand LoadPersonCommand { get; }
    public ICommand CancelCommand { get; }

    public StaffUpdateViewModel(INavigationService navigation, StaffHttpClient staffHttpClient, SidebarViewModel sidebarView)
    {
        Navigation = navigation;
        _staffHttpClient = staffHttpClient;
        SidebarView = sidebarView;
        UpdatePersonCommand = new AsyncRelayCommand(item => UpdatePersonAsync(), item => CanUpdatePerson());
        PersonEditFormModel.ErrorsChanged += (_, __) => ((AsyncRelayCommand)UpdatePersonCommand).RaiseCanExecuteChanged();
        LoadPersonCommand = new AsyncRelayCommand(item => LoadPersonAsync(_personId), item => true);
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<StaffViewModel>(), item => true);
    }

    private async Task LoadPersonAsync(Guid personId)
    {
       Result<PersonForEditResponse> result = await _staffHttpClient.GetPersonForEditAsync(personId);

       PersonEditFormModel.PhoneNumber = result.Value!.PhoneNumber;
       PersonEditFormModel.City = result.Value.City;
       PersonEditFormModel.Street = result.Value.Street;
       PersonEditFormModel.LastName = result.Value.LastName;
    }

    private Guid _personId; 

    private bool CanUpdatePerson()
    {
        return PersonEditFormModel.IsFormComplete && !PersonEditFormModel.HasErrors;
    }

    private async Task UpdatePersonAsync()
    {
        PersonUpdateRequest request = new PersonUpdateRequest()
        {
            PersonId = _personId,
            City = PersonEditFormModel.City,
            LastName = PersonEditFormModel.LastName,
            PhoneNumber = PersonEditFormModel.PhoneNumber,
            Street = PersonEditFormModel.Street,
        };

        Result<Unit> result = await _staffHttpClient.PutPersonToStaffAsync(request);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.GetUserMessage());
            return;
        }
        Navigation.NavigateTo<StaffViewModel>();
    }

    public void ReceiveParameter(object parameter)
    {
       if(parameter is Guid personId)
        {
            _personId = personId;
        }
    }


    private PersonEditForm _personEditFormModel = new();
    public PersonEditForm PersonEditFormModel
    {
        get { return _personEditFormModel; }
        set
        {
            if (_personEditFormModel != value)
            {
                _personEditFormModel = value;
                OnPropertyChanged();
            }
        }
    }
    public INavigationService Navigation { get; set; }
    public StaffHttpClient _staffHttpClient { get; set; }
    public SidebarViewModel SidebarView { get; set; }
    public ICommand UpdatePersonCommand { get; set; }

}
