using FinalProject.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    public partial class AdPage : ContentPage
    {
        private int _id;

        public AdPage()
        {
            InitializeComponent();
            BindingContext = new AdViewModel();
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                ((AdViewModel)BindingContext).RefreshPage(_id);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((AdViewModel)BindingContext).RefreshPage(App.NaivgateAdId);
        }
    }
}
