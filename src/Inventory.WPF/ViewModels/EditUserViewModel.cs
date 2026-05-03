using Inventory.WPF.Helpers;
using Inventory.WPF.Models;
using Inventory.WPF.Services;
using Inventory.WPF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

public class EditUserViewModel : BaseViewModel
{
    private readonly ApiService _api;

    public Guid UserId { get; set; }

    public string Contact { get; set; }

    public ObservableCollection<AreaVm> Areas { get; set; }
    public ObservableCollection<RoleVm> Roles { get; set; }

    public AreaVm SelectedArea { get; set; }
    public RoleVm SelectedRole { get; set; }

    public RelayCommand SaveCommand => new RelayCommand(async w => await Save(w));

    public EditUserViewModel(UserVm user, ObservableCollection<AreaVm> areas, ObservableCollection<RoleVm> roles, ApiService api)
    {
        _api = api;

        UserId = user.Id;
        Contact = user.Contact;

        Areas = areas;
        Roles = roles;

        SelectedArea = areas.FirstOrDefault(a => a.Name == user.AreaName);
        SelectedRole = roles.FirstOrDefault(r => r.Name == user.RoleName);
    }

    private async Task Save(object windowObj)
    {
        var window = windowObj as Window;

        if (window == null)
            return;

        await _api.UpdateUserContact(UserId, Contact, SelectedArea.Id, SelectedRole.Id);

        window.Close();
    }
}