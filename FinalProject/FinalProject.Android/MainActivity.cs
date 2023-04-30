using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace FinalProject.Droid
{
    [Activity(Label = "FinalProject", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly string[] permissions = { Android.Manifest.Permission.ReadExternalStorage, Android.Manifest.Permission.WriteExternalStorage, Android.Manifest.Permission.Internet };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(permissions[0]) != Permission.Granted || CheckSelfPermission(permissions[1]) != Permission.Granted || CheckSelfPermission(permissions[2]) != Permission.Granted)
                {
                    RequestPermissions(permissions, 0);
                }
            }

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
