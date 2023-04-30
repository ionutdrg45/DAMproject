using FinalProject.Models;
using FinalProject.ViewModels;
using FinalProject.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FinalProject
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BrowsePage), typeof(BrowsePage));
            Routing.RegisterRoute(nameof(NewAdPage), typeof(NewAdPage));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
