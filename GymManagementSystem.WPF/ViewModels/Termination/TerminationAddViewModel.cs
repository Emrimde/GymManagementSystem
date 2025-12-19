using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Termination;

public class TerminationAddViewModel : ViewModel, IParameterReceiver
{
	private readonly TerminationHttpClient _httpClient;
	private INavigationService _navigation;
	public SidebarViewModel SidebarView { get; }
	public INavigationService Navigation
	{
		get { return  _navigation; }
		set { _navigation = value; OnPropertyChanged(); }
	}
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
		Result<TerminationResponse> result = await _httpClient.PostTerminationAsync(TerminationAddRequest);
		if (result.IsSuccess)
		{
			MessageBox.Show("Success");
		}
		else
		{
			MessageBox.Show($"Fail! {result.ErrorMessage}");
		}
    }

    private TerminationAddRequest _terminationAddRequest;
    public TerminationAddRequest TerminationAddRequest
    {
		get { return _terminationAddRequest; }
		set { _terminationAddRequest = value; OnPropertyChanged(); }
	}
	
	public void ReceiveParameter(object parameter)
    {
		if (parameter is Guid id)
		{
			TerminationAddRequest.ClientMembershipId = id;
		}
    }

}
