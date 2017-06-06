namespace Japangolin.Romaji.Android
{
    using global::Android.App;
    using global::Android.Content;
    using global::Android.OS;
    using global::Android.Widget;

    using Uri = global::Android.Net.Uri;

    [Activity(Label = "Japangolin (Android)", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);

            // Get the UI controls from the loaded layout:
            var phoneNumberText = this.FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var translateButton = this.FindViewById<Button>(Resource.Id.TranslateButton);
            var callButton = this.FindViewById<Button>(Resource.Id.CallButton);

            // Disable the "Call" button
            callButton.Enabled = false;

            // Add code to translate number
            var translatedNumber = string.Empty;

            translateButton.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    callButton.Text = "Call";
                    callButton.Enabled = false;
                }
                else
                {
                    callButton.Text = "Call " + translatedNumber;
                    callButton.Enabled = true;
                }
            };

            callButton.Click += (sender, e) =>
            {
                // On "Call" button click, try to dial phone number.
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("Call " + translatedNumber + "?");

                callDialog.SetNeutralButton("Call", (o, args) => {
                    // Create intent to dial phone
                    var callIntent = new Intent(Intent.ActionCall);
                    callIntent.SetData(Uri.Parse("tel:" + translatedNumber));
                    this.StartActivity(callIntent);
                });

                callDialog.SetNegativeButton("Cancel", (o, args) => { });

                // Show the alert dialog to the user and wait for response.
                callDialog.Show();
            };
        }
    }
}

