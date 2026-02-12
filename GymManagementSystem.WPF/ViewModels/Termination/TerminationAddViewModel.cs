using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Termination;

public class TerminationAddViewModel : ViewModel, IParameterReceiver
{
	private readonly TerminationHttpClient _httpClient;
    public INavigationService Navigation { get; set; }
    public Guid ClientId { get; set; }
	public SidebarViewModel SidebarView { get; }
	public ICommand CreateTerminationCommand { get; }

    public TerminationAddViewModel(SidebarViewModel sidebarView, INavigationService navigation, TerminationHttpClient httpClient)
    {
        SidebarView = sidebarView;
        Navigation = navigation;
        TerminationAddRequest = new TerminationAddRequest();
		_httpClient = httpClient;
		CreateTerminationCommand = new AsyncRelayCommand(item => CreateTerminationAsync(), item => true);
    }

    private async Task CreateTerminationAsync()
    {
		MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure to terminate this memberhip?","Membership termination", MessageBoxButton.YesNo, MessageBoxImage.Question);
		if(messageBoxResult == MessageBoxResult.No)
		{
			return;
		}
		Result<TerminationResponse> result = await _httpClient.PostTerminationAsync(TerminationAddRequest);
		if (!result.IsSuccess)
		{
			MessageBox.Show($"{result.GetUserMessage()}");
			return;
		}
		Navigation.NavigateTo<ClientDetailsViewModel>(ClientId);

    }

    private TerminationAddRequest _terminationAddRequest = new();
    public TerminationAddRequest TerminationAddRequest
    {
		get { return _terminationAddRequest; }
		set { _terminationAddRequest = value; OnPropertyChanged(); }
	}
	
	public void ReceiveParameter(object parameter)
    {
		if (parameter is Guid clientId)
		{
			TerminationAddRequest.ClientId = clientId;
			ClientId = clientId;
        }
    }
}
