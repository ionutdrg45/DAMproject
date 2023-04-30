using FinalProject.Models;
using FinalProject.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FinalProject.ViewModels
{
    public class LogoutViewModel : BaseViewModel
    {
        public Command LogoutCommand { get; }
        public Command GoBackCommand { get; }

        private UserModel _UserLoggedIn;

        public LogoutViewModel()
        {
            LogoutCommand = new Command(OnLogoutClicked);
            GoBackCommand = new Command(OnGoBackClicked);

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
        }

        private async void OnLogoutClicked(object obj)
        {
            App.UserLoggedIn = null;
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            await Application.Current.MainPage.DisplayAlert("Logged out", "You logged out.", "OK");
        }

        private async void OnGoBackClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        public bool IsUserLoggedIn => _UserLoggedIn != null;
        public bool IsUserNotLoggedIn => _UserLoggedIn == null;
    }
}