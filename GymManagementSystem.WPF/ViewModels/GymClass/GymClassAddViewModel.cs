using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.GymClass.Models;
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
        
        public ObservableCollection<TrainerContractInfoResponse> TrainerContracts { get; set; } = new();

        private TrainerContractInfoResponse? _selectedTrainerContract;

        public TrainerContractInfoResponse? SelectedTrainerContract
        {
            get { return _selectedTrainerContract; }
            set {
                _selectedTrainerContract = value;
                Form.TrainerContractId = value?.Id;
                OnPropertyChanged();
            }
        }

        private GymClassAddFormModel _form = new();

        public GymClassAddFormModel Form
        {
            get { return _form; }
            set { _form = value;

                OnPropertyChanged();
            }
        }

        public INavigationService Navigation { get; }
        
        public ICommand AddGymClassCommand { get; }

        public ObservableCollection<DayItem> DaysOfWeekItems { get; }

        public DaysOfWeekFlags SelectedDays =>
            DaysOfWeekItems.Where(item => item.IsSelected)
                           .Aggregate(DaysOfWeekFlags.None, (acc, d) => acc | d.Day);

        public GymClassAddViewModel(GymClassHtppClient httpClient, SidebarViewModel sidebarView, INavigationService navigation, TrainerHttpClient trainerHttpClient)
        {
            _httpClient = httpClient;
            SidebarView = sidebarView;
            Navigation = navigation;
            _trainerHttpClient = trainerHttpClient;
           
            _ = LoadTrainerContracts();

            // Tworzymy listę dni
            DaysOfWeekItems = new ObservableCollection<DayItem>(
                Enum.GetValues(typeof(DaysOfWeekFlags))
                    .Cast<DaysOfWeekFlags>()
                    .Where(item => item != DaysOfWeekFlags.None)
                    .Select(item => new DayItem { Day = item, IsSelected = false })
            );

            AddGymClassCommand = new AsyncRelayCommand(item => AddGymClassAsync(),item => !Form.HasErrors && Form.IsFormComplete && SelectedDays != DaysOfWeekFlags.None);

            foreach (var day in DaysOfWeekItems)
            {
                day.PropertyChanged += (_, __) =>
                {
                    Form.DaysOfWeek = SelectedDays;
                    ((AsyncRelayCommand)AddGymClassCommand).RaiseCanExecuteChanged();
                };
            }

            Form.PropertyChanged += (_, __) =>
            ((AsyncRelayCommand)AddGymClassCommand).RaiseCanExecuteChanged();
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
            Form.DaysOfWeek = SelectedDays;
            

            GymClassAddRequest request = new GymClassAddRequest()
            {
                DaysOfWeek = Form.DaysOfWeek,
                MaxPeople = Form.MaxPeople,
                Name = Form.Name,
                StartHour = Form.StartHour,
                TrainerContractId = Form.TrainerContractId!.Value
            };

            Result<GymClassInfoResponse> result = await _httpClient.PostGymClassAsync(request);

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
