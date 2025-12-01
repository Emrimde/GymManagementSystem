using GymManagementSystem.Core.DTO.EmploymentTemplate;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;

namespace GymManagementSystem.WPF.ViewModels.EmploymentTemplate;
public class EmploymentTemplateViewModel : ViewModel
{
    private readonly EmploymentTemplateHttpClient _employmentTemplateHttpClient;
	private INavigationService _navigation;


    public INavigationService Navigation
	{
		get { return _navigation; }
		set { _navigation = value; OnPropertyChanged(); }
	}

	public ObservableCollection<EmploymentTemplateResponse> EmploymentTemplates { get; set; }



	public SidebarViewModel SidebarView { get; }
    public EmploymentTemplateViewModel(EmploymentTemplateHttpClient employmentTemplateHttpClient, INavigationService navigation, SidebarViewModel sidebarView)
    {
        _employmentTemplateHttpClient = employmentTemplateHttpClient;
        Navigation = navigation;
        SidebarView = sidebarView;
        EmploymentTemplates = new ObservableCollection<EmploymentTemplateResponse>();
        _ = LoadEmploymentTemplates();
    }

    private async Task LoadEmploymentTemplates()
    {
       Result<ObservableCollection<EmploymentTemplateResponse>> result = await _employmentTemplateHttpClient.GetAllEmploymentTemplates();
        if (result.IsSuccess)
        {
            foreach(EmploymentTemplateResponse item in result.Value!)
            {
                EmploymentTemplates.Add(item);
            }
        }
        else
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
    }
}
