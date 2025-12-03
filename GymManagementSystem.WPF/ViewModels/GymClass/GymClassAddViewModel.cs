using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.GymClass
{
    public class GymClassAddViewModel : ViewModel
    {
        private readonly GymClassHtppClient _httpClient;
        private readonly TrainerHttpClient _trainerHttpClient;
        public SidebarViewModel SidebarView { get; set; }
        private INavigationService _navigation;
        public ObservableCollection<TrainerContractInfoResponse> TrainerContracts {  get; set; }

        private TrainerContractInfoResponse _selectedTrainerContract;

        public TrainerContractInfoResponse SelectedTrainerContract
        {
            get { return _selectedTrainerContract; }
            set {
                _selectedTrainerContract = value;
                GymClassAddRequest.TrainerContractId = _selectedTrainerContract.Id;
                OnPropertyChanged();
            }
        }


        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }

        private GymClassAddRequest _gymClass;
        public GymClassAddRequest GymClassAddRequest
        {
            get { return _gymClass; }
            set { _gymClass = value; OnPropertyChanged(); }
        }

        public ICommand AddGymClassCommand { get; }

        public ObservableCollection<DayItem> DaysOfWeekItems { get; }

        public DaysOfWeekFlags SelectedDays =>
            DaysOfWeekItems.Where(item => item.IsSelected)
                           .Aggregate(DaysOfWeekFlags.None, (acc, d) => acc | d.Day);

        public GymClassAddViewModel(GymClassHtppClient httpClient, SidebarViewModel sidebarView, INavigationService navigation, TrainerHttpClient trainerHttpClient)
        {
            _httpClient = httpClient;
            GymClassAddRequest = new GymClassAddRequest();
            SidebarView = sidebarView;
            Navigation = navigation;
            _trainerHttpClient = trainerHttpClient;
            TrainerContracts = new ObservableCollection<TrainerContractInfoResponse>();
            _ = LoadTrainerContracts();

            // Tworzymy listę dni
            DaysOfWeekItems = new ObservableCollection<DayItem>(
                Enum.GetValues(typeof(DaysOfWeekFlags))
                    .Cast<DaysOfWeekFlags>()
                    .Where(item => item != DaysOfWeekFlags.None)
                    .Select(item => new DayItem { Day = item, IsSelected = false })
            );

            AddGymClassCommand = new AsyncRelayCommand(item => AddGymClassAsync(), item => true);
            
        }

        private async Task LoadTrainerContracts()
        {
           Result<ObservableCollection<TrainerContractInfoResponse>> result = await _trainerHttpClient.GetInstructors();
            if (result.IsSuccess)
            {
                foreach (var item in result.Value!)
                {
                    TrainerContracts.Add(item);
                }
            }
            else
            {
                MessageBox.Show($"{result.ErrorMessage}");
            }
        }

        private async Task AddGymClassAsync()
        {
            GymClassAddRequest.DaysOfWeek = SelectedDays;
            //GymClassAddRequest.TrainerContractId = _selectedTrainerContractId;
            

            Result<GymClassInfoResponse> result = await _httpClient.PostGymClassAsync(GymClassAddRequest);

            if (result.IsSuccess)
            {
                MessageBox.Show("Gym class added!");
                Navigation.NavigateTo<GymClassViewModel>();
            }
            else
            {
                MessageBox.Show($"Error: {result.ErrorMessage}");
            }
        }
    }
}
