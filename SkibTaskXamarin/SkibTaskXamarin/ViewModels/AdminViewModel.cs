using SkibTaskXamarin.Models;
using SkibTaskXamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using SkibTaskXamarin.Views;


namespace SkibTaskXamarin.ViewModels
{
    public class AdminViewModel : BindableObject
    {
        private readonly AuthService _authService = new AuthService();

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }

        public AdminViewModel()
        {
            LoadUsers();
            EditUserCommand = new Command(OnEditUser);
            DeleteUserCommand = new Command(OnDeleteUser);
        }

        private void LoadUsers()
        {
            var users = _authService.GetAllUsers();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private async void OnEditUser()
        {
            if (SelectedUser != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new EditUserPage(new EditUserViewModel(SelectedUser)));
            }
        }

        private void OnDeleteUser()
        {
            if (SelectedUser != null)
            {
                _authService.DeleteUser(SelectedUser.Id);
                LoadUsers();
            }
        }
    }
}
