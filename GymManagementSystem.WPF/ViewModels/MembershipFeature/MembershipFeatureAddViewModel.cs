using GymManagementSystem.Core.DTO.Feature;
using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.MembershipFeature;
public class MembershipFeatureAddViewModel : ViewModel, IParameterReceiver
{
    public MembershipFeatureAddRequest MembershipFeatureAdd { get; set; }

    private ObservableCollection<FeatureSelectResponse> _features;

    public ObservableCollection<FeatureSelectResponse> Features
    {
        get { return _features; }
        set { _features = value; OnPropertyChanged(); }
    }

    private FeatureSelectResponse _selectedFeature ;

    public FeatureSelectResponse SelectedFeature
    {
        get { return  _selectedFeature; }
        set {  _selectedFeature = value; 
        MembershipFeatureAdd.FeatureId = value.FeatureId;
        }
    }


    public ICommand AddMembershipFeatureCommand { get;  }

    private readonly FeatureHttpClient _featureHttpClient;
    public INavigationService Navigation { get; set; }
    public SidebarViewModel SidebarView { get; set; }
    private readonly MembershipHttpClient _membershipHttpClient;
    public IEnumerable<PeriodEnum> Periods => [PeriodEnum.Every3Months,PeriodEnum.Every6Months, PeriodEnum.None];


    private PeriodEnum _selectedPeriod;

    public PeriodEnum SelectedPeriod
    {
        get { return _selectedPeriod; }
        set { _selectedPeriod = value; MembershipFeatureAdd.Period = value; }
    }

    public MembershipFeatureAddViewModel(INavigationService navigation, SidebarViewModel sidebarView, MembershipHttpClient membershipHttpClient, FeatureHttpClient featureHttpClient)
    {
        Navigation = navigation;
        SidebarView = sidebarView;
        _membershipHttpClient = membershipHttpClient;
        MembershipFeatureAdd = new MembershipFeatureAddRequest();
        _featureHttpClient = featureHttpClient;
        Features = new ObservableCollection<FeatureSelectResponse>();
        SelectedFeature = new FeatureSelectResponse();
        
        AddMembershipFeatureCommand = new AsyncRelayCommand(item => AddMembershipFeatureAsync(), item => true);
    }
    private async Task AddMembershipFeatureAsync()
    {
        Result<Unit> result = await _membershipHttpClient.PostMembershipFeatureAsync(MembershipFeatureAdd);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }

    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id) 
        {
            MembershipFeatureAdd.MembershipId = id;

            _ = LoadFeatures();
            
        }
    }

    private async Task LoadFeatures()
    {
        Result<ObservableCollection<FeatureSelectResponse>> result = await _featureHttpClient.GetFeaturesForSelect();
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
            return;
        }

        Features = result.Value!;

    }
}
