using FinalProject.Models;
using FinalProject.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalProject.ViewModels
{
    public class AdViewModel : BaseViewModel
    {
        private UserModel _UserLoggedIn;
        private int _adId;
        private AdModel _AdModel;
        private UserModel _AdUser;
        public Command GoBackCommand { get; }
        public Command DeleteAdCommand { get; }

        public AdViewModel()
        {
            Title = "Ad Information";
            GoBackCommand = new Command(OnGoBackClicked);
            DeleteAdCommand = new Command(DeleteAdClicked);

            _UserLoggedIn = App.UserLoggedIn != null ? App.UserLoggedIn : null;

            if (_UserLoggedIn == null) Shell.Current.GoToAsync($"//{nameof(HomePage)}");

            _AdModel = new AdModel();
            _AdUser = new UserModel();
        }

        public void RefreshPage(int adId)
        {
            _UserLoggedIn = App.UserLoggedIn != null ? App.UserLoggedIn : null;
            var adSearch = App.Database.GetItems<AdModel>().FirstOrDefault(a => a.Id == adId);
            if (adSearch != null)
            {
                adSearch.ImageSource = ImageSource.FromStream(() => new MemoryStream(adSearch.ImageData));
                Ad = adSearch;
                AdUser = App.Database.GetItems<UserModel>().FirstOrDefault(u => u.Id == Ad.UserId);
            }

            OnPropertyChanged(nameof(IsUserLoggedIn));
            OnPropertyChanged(nameof(IsUserNotLoggedIn));
            OnPropertyChanged(nameof(IsUserAd));
        }

        public AdModel Ad
        {
            get => _AdModel;
            set => SetProperty(ref _AdModel, value);
        }

        public UserModel AdUser
        {
            get => _AdUser;
            set => SetProperty(ref _AdUser, value);
        }

        public bool IsUserLoggedIn => _UserLoggedIn != null;
        public bool IsUserNotLoggedIn => _UserLoggedIn == null;

        public bool IsUserAd => _UserLoggedIn != null && _AdUser != null && _UserLoggedIn.Id == _AdUser.Id;

        private async void OnGoBackClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(BrowsePage)}");
        }
        private async void DeleteAdClicked()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Delete Ad", "Are you sure you want to delete this ad?", "Yes", "No");
            if (answer)
            {
                App.Database.DeleteItem(_AdModel);
                await Shell.Current.GoToAsync($"//{nameof(BrowsePage)}");
            }
        }
    }
}
