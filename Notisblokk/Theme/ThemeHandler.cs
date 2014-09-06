using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace Notisblokk.Theme
{
    internal class ThemeHandler
    {
        private System.Windows.Application app;
        private static ThemeHandler instance;
        private static readonly String THEMEFILE = "themefile.xml";
        private static String currentTheme;
        private static ObservableCollection<String> _theme;

        private CustomTheme _Theme;
        public CustomTheme Theme
        {
            get
            {
                return _Theme;
            }
        } 
        
        private ThemeHandler() {}

        /* Load theme and return reference */
        public async static Task<ThemeHandler> getInstance()
        {
            if (instance == null)
            {
                String value;
                try
                { 
                    value = await LoadTheme();
                }catch(Exception e)
                {
                    // Fallback value, happens first run
                    value = "Snap";
                }
                currentTheme = value;
                instance = new ThemeHandler();
            }
            return instance;
        }

        /* Apply theme */
        public void setApp(System.Windows.Application app)
        {
            _Theme = new CustomTheme(currentTheme);

            this.app = app;
            ApplyTheme();
        }

        public async void SetTheme(String val)
        {
            _Theme = new CustomTheme(val);

            if (app != null)
            {
                ApplyTheme();
            }

            SaveTheme(val);
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

        public async static Task<String> LoadTheme()
        {
            _theme = await IsolatedStorageOperations.Load<ObservableCollection<String>>(THEMEFILE);
            return _theme[0];
        }

        private async void SaveTheme(String value)
        {
            _theme = new ObservableCollection<string>();
            _theme.Add(value);
            await _theme.Save(THEMEFILE);
        }

    }
}
