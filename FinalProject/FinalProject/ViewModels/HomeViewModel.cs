using FinalProject.Models;
using FinalProject.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FinalProject.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command NewAdCommand { get; }
        public Command BrowseCommand { get; }

        private UserModel _UserLoggedIn;

        public HomeViewModel()
        {
            Title = "Home";

            _UserLoggedIn = App.UserLoggedIn != null ? App.UserLoggedIn : null;

            /*var item = new UserModel { Username = "John", Email = "john@example.com" };
            App.Database.SaveItem(item);


            var items = App.Database.GetItems<UserModel>();*/

            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
            NewAdCommand = new Command(OnNewAdClicked);
            BrowseCommand = new Command(OnBrowseClicked);

        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async void OnNewAdClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(NewAdPage)}");
        }

        private async void OnBrowseClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(BrowsePage)}");
        }

        private async void OnRegisterClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }

        public ICommand OpenWebCommand { get; }

        public void RefreshPage()
        {
            // Retrieve the user from App properties
            _UserLoggedIn = App.UserLoggedIn != null ? App.UserLoggedIn : null;

            // Notify any dependent properties that they have changed
            OnPropertyChanged(nameof(IsUserLoggedIn));
            OnPropertyChanged(nameof(IsUserNotLoggedIn));
            OnPropertyChanged(nameof(HomeLabel));
        }

        public bool IsUserLoggedIn => _UserLoggedIn != null;
        public bool IsUserNotLoggedIn => _UserLoggedIn == null;
        public string HomeLabel => _UserLoggedIn == null ? "Seems that you are not logged in, please log in with your account or create one." : "Welcome back, " + _UserLoggedIn.FirstName + "! Feel free to add a new ad or browse the users ads.";
    }
}