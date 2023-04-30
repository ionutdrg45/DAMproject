using FinalProject.Models;
using FinalProject.ViewModels;
using FinalProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            InitializeComponent();
            this.BindingContext = new LogoutViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Update the view model with the user information
            var vm = (LogoutViewModel)this.BindingContext;
            vm.RefreshPage();
        }
    }
}