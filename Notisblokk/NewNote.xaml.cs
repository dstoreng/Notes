using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Notisblokk
{
    public partial class NewNote : PhoneApplicationPage
    {
        private String content;
        private String description;

        public NewNote()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (textBoxDescription.Text.Trim(' ') == "" || textBoxContent.Text.Trim(' ') == "")
            {
                MessageBox.Show("Both fields must contain some letters.");
                return;
            }
            if (textBoxDescription.Text.ToArray().Count() > 13)
            {
                MessageBox.Show("Description cannot exceed 13 letters.");
                return;
            }
            description = textBoxDescription.Text;
            content = textBoxContent.Text;
            NavigationService.Navigate(new Uri("/MainPage.xaml?description=" + description + "&content=" + content, UriKind.Relative));
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}