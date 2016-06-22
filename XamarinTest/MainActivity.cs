using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Akavache;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace XamarinTest
{
    [Activity(Label = "XamarinTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            BlobCache.ApplicationName = "XamarinTest";

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            Task.Run(() => TestAsync());

        }

        private async void TestAsync()
        {
            await BlobCache.LocalMachine.InsertObject("TestKey", "TestValue", DateTimeOffset.Now.AddDays(3));

            string s = await BlobCache.LocalMachine.GetObject<string>("TestKey");
        }

    }
}

