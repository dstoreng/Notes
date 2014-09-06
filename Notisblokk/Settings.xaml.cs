using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Notisblokk.Theme;

namespace Notisblokk
{
    public partial class Settings : PhoneApplicationPage
    {
        ThemeHandler th;
        Boolean init;
        ListPickerItem item;
        public Settings()
        {
            InitializeComponent();

            th = ThemeHandler.getInstance().Result;
            String theme = th.Theme.name;

            foreach (ListPickerItem i in ThemePicker.Items)
                if (i.Name == theme)
                    ThemePicker.SelectedItem = i;
        }

        private void ThemeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (init)
            {
                item = (ListPickerItem)ThemePicker.SelectedItem;
                th.SetTheme(item.Name);
            }

            init = true;
        }
    }
}