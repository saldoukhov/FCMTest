using Android.App;
using Android.Gms.Common;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Iid;

namespace FCMTest.Source.Activities
{
    [Activity(Label = "FCMTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly string TAG = typeof(MainActivity).FullName;
        TextView msgText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            msgText = FindViewById<TextView>(Resource.Id.msgText);

            Log.Debug(TAG, "google app id: " + Resource.String.google_app_id);

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug(TAG, "Key: {0} Value: {1}//", key, value);
                }
            }

            if (!IsPlayServicesAvailable()) return;

            var logTokenButton = FindViewById<Button>(Resource.Id.logTokenButton);
            logTokenButton.Visibility = ViewStates.Visible;
            logTokenButton.Click += delegate {
                Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
            };
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                    Log.Debug(TAG, GoogleApiAvailability.Instance.GetErrorString(resultCode));
                }
                else
                {
                    msgText.Text = "This device is not supported";
                    Log.Debug(TAG, "Not supported device");
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                Log.Debug(TAG, "available");
                return true;
            }
        }
    }
}

