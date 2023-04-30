using FinalProject.Models;
using FinalProject.Utils;
using FinalProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FinalProject.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command GoBackCommand { get; }
        public Command CreateAccountCommand { get; }

        private UserModel _UserLoggedIn;
        private string _Email;
        private string _Password;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            GoBackCommand = new Command(OnGoBackClicked);
            CreateAccountCommand = new Command(OnCreateAccountClicked);

            _UserLoggedIn = App.UserLoggedIn;

            if (_UserLoggedIn != null) Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        public void RefreshPage()
        {
            // Retrieve the user from App properties
            _UserLoggedIn = App.UserLoggedIn != null ? App.UserLoggedIn : null;

            // Notify any dependent properties that they have changed
            OnPropertyChanged(nameof(IsUserLoggedIn));
            OnPropertyChanged(nameof(IsUserNotLoggedIn));

            if (_UserLoggedIn != null) Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        public string Email
        {
            get => _Email;
            set => SetProperty(ref _Email, value);
        }

        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);
        }

        private async void OnLoginClicked(object obj)
        {
            if(String.IsNullOrEmpty(_Email) || String.IsNullOrEmpty(_Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            // Query the database to check if user exists
            var user = App.Database.GetItems<UserModel>().FirstOrDefault(u => u.Email == _Email && u.Password == Encrypt.EncryptPassword(Password));

            if (user == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid email or password", "OK");
            }
            else
            {
                App.UserLoggedIn = user;
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
        }

        private async void OnGoBackClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private async void OnCreateAccountClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }

        public bool IsUserLoggedIn => _UserLoggedIn != null;
        public bool IsUserNotLoggedIn => _UserLoggedIn == null;
    }
}
