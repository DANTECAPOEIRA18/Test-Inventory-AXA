using Inventory.WPF.Helpers;
using Inventory.WPF.Models;
using Inventory.WPF.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Inventory.WPF.Views;

namespace Inventory.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ApiService _api = new ApiService();
        private DispatcherTimer _timer;
        private bool _isLoading;

        public MainViewModel()
        {
            _ = LoadInitialData();
            StartAutoRefresh();
        }

        // =========================
        // COLLECTIONS
        // =========================
        public ObservableCollection<UserVm> Users { get; set; } = new ObservableCollection<UserVm>();
        public ObservableCollection<AreaVm> Areas { get; set; } = new ObservableCollection<AreaVm>();
        public ObservableCollection<RoleVm> Roles { get; set; } = new ObservableCollection<RoleVm>();

        // =========================
        // FORM FIELDS (NOTIFY)
        // =========================
        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string _contact;
        public string Contact
        {
            get => _contact;
            set { _contact = value; OnPropertyChanged(); }
        }

        private AreaVm _selectedArea;
        public AreaVm SelectedArea
        {
            get => _selectedArea;
            set { _selectedArea = value; OnPropertyChanged(); }
        }

        private RoleVm _selectedRole;
        public RoleVm SelectedRole
        {
            get => _selectedRole;
            set { _selectedRole = value; OnPropertyChanged(); }
        }

        // =========================
        // UI STATE
        // =========================
        private Visibility _loadingVisibility = Visibility.Collapsed;
        public Visibility LoadingVisibility
        {
            get => _loadingVisibility;
            set { _loadingVisibility = value; OnPropertyChanged(); }
        }

        private bool _isFormEnabled = true;
        public bool IsFormEnabled
        {
            get => _isFormEnabled;
            set { _isFormEnabled = value; OnPropertyChanged(); }
        }

        // =========================
        // COMMANDS
        // =========================
        public RelayCommand LoadUsersCommand => new RelayCommand(async _ => await LoadUsers());
        public RelayCommand CreateUserCommand => new RelayCommand(async _ => await CreateUser());
        public RelayCommand EditUserCommand => new RelayCommand(OpenEditModal);

        // =========================
        // LOAD USERS
        // =========================
        private async Task LoadUsers()
        {
            if (_isLoading) return;

            _isLoading = true;

            try
            {
                LoadingVisibility = Visibility.Visible;

                var users = await _api.GetUsers();

                Users.Clear();
                foreach (var u in users)
                    Users.Add(u);
            }
            finally
            {
                LoadingVisibility = Visibility.Collapsed;
                _isLoading = false;
            }
        }

        // =========================
        // CREATE USER
        // =========================
        private async Task CreateUser()
        {
            if (SelectedArea == null || SelectedRole == null)
                return;

            try
            {
                IsFormEnabled = false;
                LoadingVisibility = Visibility.Visible;

                var user = new UserVm
                {
                    Name = Name,
                    Contact = Contact,
                    AreaId = SelectedArea.Id,
                    RoleId = SelectedRole.Id
                };

                await _api.CreateUser(user);

                await LoadUsers();

                // limpiar formulario
                Name = "";
                Contact = "";
                SelectedArea = null;
                SelectedRole = null;

                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Contact));
                OnPropertyChanged(nameof(SelectedArea));
                OnPropertyChanged(nameof(SelectedRole));
            }
            finally
            {
                IsFormEnabled = true;
                LoadingVisibility = Visibility.Collapsed;
            }
        }

        // =========================
        // LOAD COMBOS
        // =========================
        public async Task LoadCombos()
        {
            var areas = await _api.GetAreas();
            Areas.Clear();
            foreach (var a in areas)
                Areas.Add(a);

            var roles = await _api.GetRoles();
            Roles.Clear();
            foreach (var r in roles)
                Roles.Add(r);
        }

        // =========================
        // INITIAL LOAD
        // =========================
        private async Task LoadInitialData()
        {
            await LoadCombos();   // combos primero
            await LoadUsers();    // luego tabla
        }

        // =========================
        // AUTO REFRESH
        // =========================
        private void StartAutoRefresh()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(15);

            _timer.Tick += async (s, e) =>
            {
                await LoadUsers();
                //await LoadCombos();
            };

            _timer.Start();
        }

        private void StopAutoRefresh()
        {
            if (_timer != null)
                _timer.Stop();
        }

        private void ResumeAutoRefresh()
        {
            if (_timer != null)
                _timer.Start();
        }

        // =========================
        // Open Modal
        // =========================
        private void OpenEditModal(object parameter)
        {
            var user = parameter as UserVm;

            if (user == null)
                return;

            StopAutoRefresh();

            var vm = new EditUserViewModel(user, Areas, Roles, _api);

            var window = new EditUserWindow
            {
                DataContext = vm
            };

            window.ShowDialog();

            ResumeAutoRefresh();

            _ = LoadUsers();
        }
    }
}