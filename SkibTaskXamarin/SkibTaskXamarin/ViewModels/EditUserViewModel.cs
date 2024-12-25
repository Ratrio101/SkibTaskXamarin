using SkibTaskXamarin.Models;
using SkibTaskXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkibTaskXamarin.ViewModels
{
    public class EditUserViewModel : BindableObject
    {
        private readonly AuthService _authService = new AuthService();

        public User User { get; set; }

        public ICommand SaveUserCommand { get; }

        public EditUserViewModel(User user)
        {
            User = user;
            SaveUserCommand = new Command(OnSaveUser);
        }

        private async void OnSaveUser()
        {
            _authService.UpdateUser(User);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
