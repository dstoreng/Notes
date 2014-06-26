using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Notisblokk.Theme;

namespace Notisblokk
{
    public partial class Settings : PhoneApplicationPage
    {
        private ThemeHandler th;
        private ListPickerItem item;
        private bool init;
        public Settings()
        {   
            InitializeComponent();

            th = ThemeHandler.getInstance();

            String theme = th.Theme.name;

            foreach(ListPickerItem i in ThemePicker.Items)
                if (i.Name == theme)
                    ThemePicker.SelectedItem = i;
        }

        private void ThemePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (init)
            {
                item = (ListPickerItem)ThemePicker.SelectedItem;
                th.changeTheme(item.Name);
                th.SaveTheme();
            }

            init = true;
        }

        private void FeedbackButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EmailComposeTask mail = new EmailComposeTask();
            mail.To = "daniel@storeng.it";
            mail.Subject = "Notes application - feedback";

            mail.Show();
        }
    }
}