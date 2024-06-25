using FunTrophy.Shared.Model;
using FunTrophy.Web.Components;
using FunTrophy.Web.Contracts.Services;
using Microsoft.AspNetCore.Components;

namespace FunTrophy.Web.Pages.Authentication
{
    public partial class UsersPage
    {
        #region Properties

        [Inject]
        private IUserService UserService { get; set; } = default!;

        private List<UserDto>? Users { get; set; }

        private ConfirmDialog DeleteDialog { get; set; } = default!;

        private EditDialog EditDialog { get; set; } = default!;

        private EditDialog EditPasswordDialog { get; set; } = default!;

        private AddUserDto AddUserModel { get; set; } = new() { IsAdmin = false };

        private UpdateUserDto UpdateUserModel { get; set; } = new();

        private string? NewPassword { get; set; }

        private int? _updateUserId;

        private int? _deleteUserId;

        private int? _newPasswordUserId;

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            Users = await UserService.GetAll();
        }

        private async Task AddUser()
        {
            await UserService.Add(AddUserModel);
            AddUserModel = new AddUserDto();
            await LoadUsers();
        }

        private async Task EditUser(UserDto user)
        {
            if (user is null)
                return;

            _updateUserId = user.Id;
            UpdateUserModel.FirstName = user.FirstName;
            UpdateUserModel.LastName = user.LastName;
            UpdateUserModel.UserName = user.UserName;
            UpdateUserModel.IsAdmin = user.IsAdmin;
            await EditDialog.ShowAsync();
        }

        private async Task ConfirmEditUser(bool confirm)
        {
            if (confirm && _updateUserId.HasValue)
            {
                await UserService.Update(_updateUserId.Value, UpdateUserModel);
                await LoadUsers();
            }
        }

        private void DeleteUser(UserDto user)
        {
            _deleteUserId = user.Id;
            var message = $"Es-tu sûr de vouloir supprimer '{user.UserName}'?";
            DeleteDialog.Show(message);
        }

        private async Task ConfirmDeleteUser(bool confirm)
        {
            if (confirm && _deleteUserId.HasValue)
            {
                await UserService.Remove(_deleteUserId.Value);
                await LoadUsers();
            }
        }

        private async Task EditPassword(int userId)
        {
            _newPasswordUserId = userId;
            await EditPasswordDialog.ShowAsync();
        }

        private async Task ConfirmEditPassword(bool confirm)
        {
            if (confirm && _newPasswordUserId.HasValue && !string.IsNullOrWhiteSpace(NewPassword))
            {
                await UserService.ChangePassword(_newPasswordUserId.Value, NewPassword);
            }
            NewPassword = null;
        }
    }
}