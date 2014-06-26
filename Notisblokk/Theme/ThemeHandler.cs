using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Notisblokk.Theme
{
    internal class ThemeHandler
    {
        private System.Windows.Application app;
        private static ThemeHandler instance;
        private static String theme;

        public static ThemeHandler getInstance()
        {
            if (instance == null)
                instance = new ThemeHandler();
            return instance;
        }

        private ThemeHandler()
        {
            GetTheme();
            changeTheme(theme);            
        }

        private async void GetTheme()
        {
            String obj = await IsolatedStorageOperations.Load<String>("THEME");

            MessageBox.Show(obj.ToString());
            if (obj == null)
            {
                theme = "Pink";
            }
            else
            {
                theme = "Green";
            }
        }

        public void SaveTheme()
        {
            theme.Save<string>("THEME");
        }

        public void setApp(System.Windows.Application app)
        {
            this.app = app;
            ApplyTheme();
        }

        private CustomTheme _Theme;

        public CustomTheme Theme
        {
            get
            {
                return _Theme;
            }
        }

        public void changeTheme(String val)
        {
            _Theme = new CustomTheme(val);

            if (app != null)
            {
                ApplyTheme();
            }
        }

        private void ApplyTheme()
        {
            (app.Resources["ThemeTitle"] as SolidColorBrush).Color = GetColorFromHexa(_Theme.Title);
            (app.Resources["ThemeHeader"] as SolidColorBrush).Color = GetColorFromHexa(_Theme.Header);
            (app.Resources["ThemeH1"] as SolidColorBrush).Color = GetColorFromHexa(_Theme.H1);
            (app.Resources["ThemeH2"] as SolidColorBrush).Color = GetColorFromHexa(_Theme.H2);
            (app.Resources["ThemeH3"] as SolidColorBrush).Color = GetColorFromHexa(_Theme.H3);
            (app.Resources["ThemeText"] as SolidColorBrush).Color = GetColorFromHexa(_Theme.Text);
            (app.Resources["ThemeBackground"] as SolidColorBrush).Color = GetColorFromHexa(_Theme.Background);
        }

        public static Color GetColorFromHexa(string hexaColor)
        {
            return Color.FromArgb(
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16),
                    Convert.ToByte(hexaColor.Substring(7, 2), 16)
            );
        }

    }
}
