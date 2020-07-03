﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wacton.Japangolin.UI.Mains
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            this.SetUserInputLanguage();
        }

        private void SetUserInputLanguage()
        {
            var japaneseCultureInfo = new CultureInfo("ja-JP");
            if (!this.IsInputLanguageAvailable(japaneseCultureInfo))
            {
                return;
            }

            // should set input to Japanese on focus, restore previously language on lose focus
            InputLanguageManager.SetInputLanguage(this.Input, japaneseCultureInfo);
            InputLanguageManager.SetRestoreInputLanguage(this.Input, true);
        }

        private bool IsInputLanguageAvailable(CultureInfo cultureInfo)
        {
            var availableInputLanguages = InputLanguageManager.Current.AvailableInputLanguages;
            if (availableInputLanguages == null)
            {
                return false;
            }

            if (!availableInputLanguages.Cast<CultureInfo>().Contains(cultureInfo))
            {
                return false;
            }

            return true;
        }
    }
}