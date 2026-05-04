using Inventory.WPF.Helpers;
using Inventory.WPF.Models;
using Inventory.WPF.Services;
using Inventory.WPF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

public class EditUserViewModel : BaseViewModel, IDataErrorInfo
{
    private readonly ApiService _api;

    public Guid UserId { get; set; }

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

    public ObservableCollection<AreaVm> Areas { get; set; }
    public ObservableCollection<RoleVm> Roles { get; set; }

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
    // VALIDACIONES
    // =========================
    public string Error => null;

    public string this[string columnName]
    {
        get
        {
            switch (columnName)
            {
                case nameof(Contact):
                    if (string.IsNullOrWhiteSpace(Contact))
                        return "Contacto requerido";

                    if (!Regex.IsMatch(Contact, @"^\d{1,10}$"))
                        return "Solo números (máx 10)";
                    break;

                case nameof(Email):
                    if (string.IsNullOrWhiteSpace(Email))
                        return "Email requerido";

                    if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                        return "Email inválido";
                    break;
            }

            return null;
        }
    }

    public bool HasErrors =>
        this[nameof(Contact)] != null ||
        this[nameof(Email)] != null;

    // =========================
    // COMMAND
    // =========================
    public RelayCommand SaveCommand =>
        new RelayCommand(async w => await Save(w), _ => !HasErrors);

    // =========================
    // CONSTRUCTOR
    // =========================
    public EditUserViewModel(UserVm user,
        ObservableCollection<AreaVm> areas,
        ObservableCollection<RoleVm> roles,
        ApiService api)
    {
        _api = api;

        UserId = user.Id;
        Contact = user.Contact;
        Email = user.Email;

        Areas = areas;
        Roles = roles;

        SelectedArea = areas.FirstOrDefault(a => a.Name == user.AreaName);
        SelectedRole = roles.FirstOrDefault(r => r.Name == user.RoleName);
    }

    // =========================
    // SAVE
    // =========================
    private async Task Save(object windowObj)
    {
        if (HasErrors || SelectedArea == null || SelectedRole == null)
            return;

        var window = windowObj as Window;

        await _api.UpdateUserContact(UserId, Contact, Email, SelectedArea.Id, SelectedRole.Id);

        window?.Close();
    }
}