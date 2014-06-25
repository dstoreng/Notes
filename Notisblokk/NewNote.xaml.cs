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
        private String id;

        public NewNote()
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
                    if (NavigationContext.QueryString.TryGetValue("content", out content))
                    {
                        textBoxContent.Text = content;
                    }
                }
            }
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

            Note note = new Note();
            note.Description = textBoxDescription.Text;
            note.Content = textBoxContent.Text;
            if (id != null)
            {
                note.Id = id;
                PhoneApplicationService.Current.State.Add("NOTE", note);
            }
            else
            {
                PhoneApplicationService.Current.State.Add("NOTE", note);
            }

            NavigationService.GoBack();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}