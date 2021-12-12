using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.Collections.Generic;
using System.IO;

namespace BlokNotes.Droid
{
    [Activity(Label = "BlokNotes", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            #region GlobalSettings

            GlobalSettings.MainSavePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "BlokNotes");
            //todo zapis android

            GlobalSettings.OnSave += (string path, string text) =>
            {
                var absolutePath = Path.Combine(GlobalSettings.SaveNotesPath, path);

                var pathDire = absolutePath.Substring(0, absolutePath.LastIndexOf(Path.DirectorySeparatorChar));
                if (!Directory.Exists(pathDire))
                    Directory.CreateDirectory(pathDire);

                if (File.Exists(absolutePath))
                    File.Delete(absolutePath);
                using (var fct = File.CreateText(absolutePath))
                {
                    fct.Write(text);
                    fct.Close();
                }
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"Use path: {absolutePath}");
#endif
            };

            GlobalSettings.GetFiles += (path) =>
            {
                List<string> tmp = new List<string>();
                Java.IO.File directory = new Java.IO.File(path);
                Java.IO.File[] files = directory.ListFiles();
                if (files?.Length > 0)
                    foreach (var item in files)
                    {
                        tmp.Add(item.AbsolutePath);
                    }
                return tmp;
            };

            #endregion

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}