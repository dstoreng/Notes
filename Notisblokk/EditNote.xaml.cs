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
    public partial class EditNote : PhoneApplicationPage
    {
        private String description, content, id;
        private String oldDescription, oldContent;

        public EditNote()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue("id", out id))
            {
                if (NavigationContext.QueryString.TryGetValue("description", out description))
                {
                    textBoxDescription.Text = description;
                    oldDescription = description;
                    if (NavigationContext.QueryString.TryGetValue("content", out content))
                    {
                        oldContent = content;
                        textBoxContent.Text = content;
                    }
                }
            }
        }

        private async void save_Click(object sender, EventArgs e)
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

            description = (textBoxDescription.Text.Trim(' ') != "") ? textBoxDescription.Text : oldDescription;
            content = (textBoxDescription.Text.Trim(' ') != "") ? textBoxContent.Text : oldContent;

            NavigationService.Navigate(new Uri("/MainPage.xaml?id=" + id + "&description=" + description + "&content=" + content, UriKind.Relative));
        }

        private async void cancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}