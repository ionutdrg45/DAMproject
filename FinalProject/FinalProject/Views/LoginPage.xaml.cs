using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Update the view model with the user information
            var vm = (LoginViewModel)this.BindingContext;
            vm.RefreshPage();
        }
    }
}