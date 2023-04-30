using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using FinalProject.Utils;
using FinalProject.Views;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using System.Net.Http.Headers;

namespace FinalProject.ViewModels
{
    public class NewAdViewModel : BaseViewModel
    {
        private string AdTitle;
        private string AdDescription;
        private decimal AdPrice;
        private string AdImagePath;
        private byte[] AdImageData;
        private ImageSource AdImageSource;
        public ObservableCollection<County> CountiesList { get; set; } = Counties.GetCounties();
        public County SelectedCounty { get; set; }

        private UserModel _UserLoggedIn;

        public NewAdViewModel()
        {
            _UserLoggedIn = App.UserLoggedIn != null ? App.UserLoggedIn : null;

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            if (_UserLoggedIn == null) Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        public void RefreshPage()
        {
            // Retrieve the user from App properties
            _UserLoggedIn = App.UserLoggedIn != null ? App.UserLoggedIn : null;

            // Notify any dependent properties that they have changed
            OnPropertyChanged(nameof(IsUserLoggedIn));
            OnPropertyChanged(nameof(IsUserNotLoggedIn));
        }

        public bool IsUserLoggedIn => _UserLoggedIn != null;
        public bool IsUserNotLoggedIn => _UserLoggedIn == null;

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(AdTitle)
                && !String.IsNullOrWhiteSpace(AdDescription)
                && AdPrice != 0
                && !String.IsNullOrWhiteSpace(AdImagePath)
                && !String.IsNullOrWhiteSpace(SelectedCounty == null ? null : SelectedCounty.Name)
                && _UserLoggedIn != null;
        }

        public string BindAdTitle
        {
            get => AdTitle;
            set => SetProperty(ref AdTitle, value);
        }

        public string BindAdDescription
        {
            get => AdDescription;
            set => SetProperty(ref AdDescription, value);
        }

        public decimal BindAdPrice
        {
            get => AdPrice;
            set => SetProperty(ref AdPrice, value);
        }

        public string BindAdImagePath
        {
            get => AdImagePath;
            set => SetProperty(ref AdImagePath, value);
        }

        public ImageSource BindAdImageSource
        {
            get => AdImageSource;
            set => SetProperty(ref AdImageSource, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private async void OnSave()
        {
            if (AdTitle.Length < 4 || AdTitle.Length > 25)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Title must be between 4 and 25 letters long and only contain letters", "OK");
                return;
            }

            if (AdDescription.Length < 10 || AdDescription.Length > 500)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Description must be between 10 and 500 characters long", "OK");
                return;
            }

            var ad = new AdModel
            {
                Title = AdTitle,
                Description = AdDescription,
                Price = AdPrice,
                Location = SelectedCounty.Name,
                Available = true,
                ImageData = null,
                Date = DateTime.Now,
                UserId = _UserLoggedIn.Id
            };

            // Copy the image to the app's local storage and update the image path
            if (!string.IsNullOrEmpty(AdImagePath))
            {
                string imagePath = AdImagePath;

                if (!string.IsNullOrEmpty(imagePath))
                {
                    ad.ImageData = AdImageData;
                }
            }

            App.Database.SaveItem(ad);
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            await Application.Current.MainPage.DisplayAlert("Success", "Your ad was created.", "OK");
        }

        private async void OnSelectImage()
        {
            // Ask the user to pick a photo
            var file = await CrossFilePicker.Current.PickFile();

            if (file != null)
            {
                var path = file.FilePath;

                using (var memoryStream = new MemoryStream())
                {
                    await file.GetStream().CopyToAsync(memoryStream);
                    AdImageData = memoryStream.ToArray();
                }

                BindAdImagePath = path;

                BindAdImageSource = ImageSource.FromFile(path);

            }
        }

        public ICommand SelectImageCommand => new Command(OnSelectImage);
    }
}
