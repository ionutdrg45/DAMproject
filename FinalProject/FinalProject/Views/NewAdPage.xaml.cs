using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using FinalProject.ViewModels;

namespace FinalProject.Views
{
    public partial class NewAdPage : ContentPage
    {
        public NewAdPage()
        {
            InitializeComponent();
            this.BindingContext = new NewAdViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Update the view model with the user information
            var vm = (NewAdViewModel)this.BindingContext;
            vm.RefreshPage();
        }
    }
}
