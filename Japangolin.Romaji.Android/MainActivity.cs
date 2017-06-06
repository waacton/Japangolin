namespace Japangolin.Romaji.Android
{
    using System;

    using global::Android.App;
    using global::Android.OS;
    using global::Android.Widget;

    using Wacton.Japangolin.Romaji.Domain.JapanesePhrases;

    [Activity(Label = "Japangolin (Android)", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private IJapanesePhraseRepository japanesePhraseRepository;
        private JapanesePhrase japanesePhrase;

        private TextView japaneseTextView;
        private EditText romajiTextBox;
        private Button checkButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);

            // get the UI controls from the loaded layout:
            this.japaneseTextView = this.FindViewById<TextView>(Resource.Id.JapaneseTextView);
            this.romajiTextBox = this.FindViewById<EditText>(Resource.Id.RomajiTextBox);
            this.checkButton = this.FindViewById<Button>(Resource.Id.CheckButton);

            this.checkButton.Enabled = false;

            this.InitialiseJapanesePhraseRepository();
            this.UpdateJapanesePhrase();

            this.checkButton.Click += this.CheckRomaji;

            this.checkButton.Enabled = true;
        }

        private void InitialiseJapanesePhraseRepository()
        {
            this.japanesePhraseRepository = new JapanesePhraseRepository();
        }

        private void UpdateJapanesePhrase()
        {
            this.japanesePhrase = this.japanesePhraseRepository.GetRandomPhrase();
            this.japaneseTextView.Text = this.japanesePhrase.Kana;
            this.romajiTextBox.Text = string.Empty;
        }

        private void CheckRomaji(object sender, EventArgs e)
        {
            if (this.romajiTextBox.Text.ToLower() == this.japanesePhrase.Romaji.ToLower())
            {
                this.UpdateJapanesePhrase();
            }
        }
    }
}

