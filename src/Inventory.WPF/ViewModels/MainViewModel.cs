using Inventory.WPF.Helpers;
using Inventory.WPF.Models;
using Inventory.WPF.Services;
using Inventory.WPF.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Inventory.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel, IDataErrorInfo
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
        public ObservableCollection<TypeDocumentVm> TypeDocuments { get; set; } = new ObservableCollection<TypeDocumentVm>();

        // =========================
        // FORM FIELDS
        // =========================
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasErrors));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private string _contact;
        public string Contact
        {
            get => _contact;
            set
            {
                _contact = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasErrors));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasErrors));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private string _documentId;
        public string DocumentId
        {
            get => _documentId;
            set
            {
                _documentId = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasErrors));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public AreaVm SelectedArea { get; set; }
        public RoleVm SelectedRole { get; set; }
        public TypeDocumentVm SelectedTypedocument { get; set; }

        // =========================
        // VALIDACIONES
        // =========================
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrWhiteSpace(Name))
                            return "Nombre requerido";

                        if (!Regex.IsMatch(Name, @"^[A-Za-zÁÉÍÓÚáéíóúñÑ ]+$"))
                            return "Solo letras";

                        if (Name.EndsWith(" "))
                            return "No debe terminar en espacio";
                        break;

                    case nameof(Contact):
                        if (string.IsNullOrWhiteSpace(Contact))
                            return "Contacto requerido";

                        if (!Regex.IsMatch(Contact, @"^\d{1,10}$"))
                            return "Máx 10 números";
                        break;

                    case nameof(Email):
                        if (string.IsNullOrWhiteSpace(Email))
                            return "Email requerido";

                        if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                            return "Email inválido";
                        break;

                    case nameof(DocumentId):
                        if (string.IsNullOrWhiteSpace(DocumentId))
                            return "Documento requerido";

                        if (!Regex.IsMatch(DocumentId, @"^\d{1,10}$"))
                            return "Máx 10 números";
                        break;
                }

                return null;
            }
        }

        public bool HasErrors =>
            this[nameof(Name)] != null ||
            this[nameof(Contact)] != null ||
            this[nameof(Email)] != null ||
            this[nameof(DocumentId)] != null;

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
        public RelayCommand CreateUserCommand =>
            new RelayCommand(async _ => await CreateUser(), _ => !HasErrors);

        public RelayCommand EditUserCommand =>
            new RelayCommand(OpenEditModal);

        public RelayCommand DeleteUserCommand =>
            new RelayCommand(async u => await DeleteUser(u));

        public RelayCommand ActivateUserCommand =>
            new RelayCommand(async u => await ActivateUser(u));

        // =========================
        // METHODS
        // =========================

        private async Task DeleteUser(object parameter)
        {
            var user = parameter as UserVm;
            if (user == null) return;

            var confirm = MessageBox.Show(
                $"¿Eliminar usuario {user.Name}?",
                "Confirmación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes)
                return;

            try
            {
                LoadingVisibility = Visibility.Visible;

                await _api.DeleteUser(user.Id);

                await LoadUsers();
            }
            finally
            {
                LoadingVisibility = Visibility.Collapsed;
            }
        }

        private async Task ActivateUser(object parameter)
        {
            var user = parameter as UserVm;
            if (user == null) return;

            try
            {
                LoadingVisibility = Visibility.Visible;

                await _api.ActivateUser(user.Id);

                await LoadUsers();
            }
            finally
            {
                LoadingVisibility = Visibility.Collapsed;
            }
        }

        private async Task CreateUser()
        {
            if (HasErrors || SelectedArea == null || SelectedRole == null || SelectedTypedocument == null)
                return;

            try
            {
                IsFormEnabled = false;
                LoadingVisibility = Visibility.Visible;

                var user = new UserVm
                {
                    Name = Name,
                    Contact = Contact,
                    Email = Email,
                    DocumentNumber = int.Parse(DocumentId),
                    TypeDocumentId = SelectedTypedocument.Id,
                    AreaId = SelectedArea.Id,
                    RoleId = SelectedRole.Id
                };

                await _api.CreateUser(user);

                await LoadUsers();

                Name = "";
                Contact = "";
                Email = "";
                DocumentId = "";
                SelectedArea = null;
                SelectedRole = null;
                SelectedTypedocument = null;
            }
            finally
            {
                IsFormEnabled = true;
                LoadingVisibility = Visibility.Collapsed;
            }
        }

        private void OpenEditModal(object parameter)
        {
            var user = parameter as UserVm;
            if (user == null) return;

            _timer?.Stop();

            var vm = new EditUserViewModel(user, Areas, Roles, _api);

            var window = new EditUserWindow
            {
                DataContext = vm
            };

            window.ShowDialog();

            _timer?.Start();

            _ = LoadUsers();
        }

        private async Task LoadUsers()
        {
            if (_isLoading) return;
            _isLoading = true;

            try
            {
                var users = await _api.GetUsers();

                Users.Clear();
                foreach (var u in users)
                    Users.Add(u);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task LoadCombos()
        {
            var areas = await _api.GetAreas();
            Areas.Clear();
            foreach (var a in areas)
                Areas.Add(a);

            var roles = await _api.GetRoles();
            Roles.Clear();
            foreach (var r in roles)
                Roles.Add(r);

            var types = await _api.GetTypeDocuments();
            TypeDocuments.Clear();
            foreach (var t in types)
                TypeDocuments.Add(t);
        }

        private async Task LoadInitialData()
        {
            await LoadCombos();
            await LoadUsers();
        }

        private void StartAutoRefresh()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);

            _timer.Tick += async (s, e) =>
            {
                await LoadUsers();
            };

            _timer.Start();
        }
    }
}