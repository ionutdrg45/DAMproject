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
    public class BrowseViewModel : BaseViewModel
    {
        private UserModel _UserLoggedIn;
        private ObservableCollection<AdModel> _ads;
        public Command GoBackCommand { get; }
        public Command ItemSelectedCommand { get; }

        public BrowseViewModel()
        {
            GoBackCommand = new Command(OnGoBackClicked);
            ItemSelectedCommand = new Command<int>(ExecuteItemSelectedCommand);

            _UserLoggedIn = App.UserLoggedIn != null ? App.UserLoggedIn : null;

            if (_UserLoggedIn == null) Shell.Current.GoToAsync($"//{nameof(HomePage)}");

            GetAds();
        }

        public void RefreshPage()
        {
            // Retrieve the user from App properties
            _UserLoggedIn = App.UserLoggedIn != null ? App.UserLoggedIn : null;

            // Notify any dependent properties that they have changed
            OnPropertyChanged(nameof(IsUserLoggedIn));
            OnPropertyChanged(nameof(IsUserNotLoggedIn));
            GetAds();
        }

        private async void ExecuteItemSelectedCommand(int itemId)
        {
            App.NaivgateAdId = itemId;
            await Shell.Current.GoToAsync($"//{nameof(AdPage)}");
        }

        public ObservableCollection<AdModel> Ads
        {
            get => _ads;
            set => SetProperty(ref _ads, value);
        }

        public bool IsUserLoggedIn => _UserLoggedIn != null;
        public bool IsUserNotLoggedIn => _UserLoggedIn == null;

        public async Task GetAds()
        {
            try
            {
                // Retrieve all ads from the database, sorted by date descending
                var items = await Task.Run(() => App.Database.GetItems<AdModel>());

                foreach (var ad in items)
                {
                    ad.ImageSource = ImageSource.FromStream(() => new MemoryStream(ad.ImageData));
                }

                Ads = new ObservableCollection<AdModel>(items.OrderByDescending(ad => ad.Date));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void OnGoBackClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}
