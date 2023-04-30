using FinalProject.Models;
using FinalProject.ViewModels;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace FinalProject.Views
{
    public partial class BrowsePage : ContentPage
    {
        public BrowsePage()
        {
            InitializeComponent();
            BindingContext = new BrowseViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Update the view model with the user information
            var vm = (BrowseViewModel)this.BindingContext;
            vm.RefreshPage();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is AdModel selectedAd)
            {
                var vm = (BrowseViewModel)this.BindingContext;
                // Execute the command with the selected item ID
                vm.ItemSelectedCommand.Execute(selectedAd.Id);
            }

            // Clear the selection
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}