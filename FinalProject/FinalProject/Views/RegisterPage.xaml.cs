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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Update the view model with the user information
            var vm = (RegisterViewModel)this.BindingContext;
            vm.RefreshPage();
        }
    }
}