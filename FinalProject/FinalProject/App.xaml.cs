
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
    public partial class App : Application
    {
        public static MyDatabaseConnection Database { get; set; }
        public static UserModel UserLoggedIn { get; set; } = null;
        public static int NaivgateAdId { get; set; } = 0;
        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FinalProject.db3");
            Database = new MyDatabaseConnection(dbPath);

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
