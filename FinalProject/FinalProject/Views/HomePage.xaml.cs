using FinalProject.Models;
using FinalProject.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Update the view model with the user information
            var vm = (HomeViewModel)this.BindingContext;
            vm.RefreshPage();
        }
    }
}