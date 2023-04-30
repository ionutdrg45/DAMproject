using FinalProject.Models;
using FinalProject.Utils;
using FinalProject.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FinalProject.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public Command RegisterCommand { get; }
        public Command GoBackCommand { get; }
        public Command LoginCommand { get; }

        private UserModel _UserLoggedIn;
        private string _Email;
        private string _Username;
        private string _Firstname;
        private string _Lastname;
        private string _Password;
        private string _ConfirmPassword;

        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
            GoBackCommand = new Command(OnGoBackClicked);
            LoginCommand = new Command(OnLoginClicked);

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

        public string Username
        {
            get => _Username;
            set => SetProperty(ref _Username, value);
        }

        public string Firstname
        {
            get => _Firstname;
            set => SetProperty(ref _Firstname, value);
        }

        public string Lastname
        {
            get => _Lastname;
            set => SetProperty(ref _Lastname, value);
        }

        public string ConfirmPassword
        {
            get => _ConfirmPassword;
            set => SetProperty(ref _ConfirmPassword, value);
        }

        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);
        }

        private async void OnRegisterClicked(object obj)
        {
            // Check if all required fields are filled
            if (string.IsNullOrEmpty(_Email) || string.IsNullOrEmpty(_Username) || string.IsNullOrEmpty(_Firstname) ||
                string.IsNullOrEmpty(_Lastname) || string.IsNullOrEmpty(_Password) || string.IsNullOrEmpty(_ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            // Validate the input fields
            if (!IsValidEmail(_Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid email address.", "OK");
                return;
            }
            if (!IsValidUsername(_Username))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Username must be between 3 and 20 characters and contain only alphanumerical characters.", "OK");
                return;
            }
            if (!IsValidName(_Firstname))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "First name must be between 2 and 20 characters and contain only letters.", "OK");
                return;
            }
            if (!IsValidName(_Lastname))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Last name must be between 2 and 20 characters and contain only letters.", "OK");
                return;
            }
            if (!IsValidPassword(_Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Password must be between 5 and 20 characters.", "OK");
                return;
            }
            if (_Password != _ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // All input fields are valid, create user and save to database
            var user = new UserModel
            {
                Email = _Email,
                Username = _Username,
                FirstName = _Firstname,
                LastName = _Lastname,
                Password = Encrypt.EncryptPassword(_Password)
            };
            App.Database.SaveItem(user);

            // Navigate to LoginPage
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            await Application.Current.MainPage.DisplayAlert("Success", "Your account has been created, please log in.", "OK");
        }

        // Validation methods
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool IsValidUsername(string username)
        {
            return !string.IsNullOrEmpty(username) && username.Length >= 3 && username.Length <= 20 && System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z0-9]+$");
        }
        private bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length >= 2 && name.Length <= 20 && System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }
        private bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 5 && password.Length <= 20;
        }

        private async void OnGoBackClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        public bool IsUserLoggedIn => _UserLoggedIn != null;
        public bool IsUserNotLoggedIn => _UserLoggedIn == null;
    }
}
