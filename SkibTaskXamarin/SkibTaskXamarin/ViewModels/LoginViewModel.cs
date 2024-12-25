using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using SkibTaskXamarin.Services;
using SkibTaskXamarin.Views;

namespace SkibTaskXamarin.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        private readonly AuthService _authService = new AuthService();

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
        }

        private async void OnLogin()
        {
            if (_authService.Login(Username, Password))
            {
                if (Username == "admin") // Проверка, что это администратор
                {
                    await Application.Current.MainPage.DisplayAlert("Успех", "Добро пожаловать в админку! Гы! o_0", "OK");
                    Application.Current.MainPage = new NavigationPage(new AdminPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Успех", "Вход выполнен успешно!", "OK");
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Неверный логин или пароль!", "OK");
            }
        }

        private async void OnRegister()
        {
            if (_authService.Register(Username, Password))
            {
                await Application.Current.MainPage.DisplayAlert("Успех", "Регистрация успешно выполнена!", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ой-ой!", "Такой пользователь уже есть", "OK");
            }
        }
    }
}
