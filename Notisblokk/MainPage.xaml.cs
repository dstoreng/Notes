using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Notisblokk.Resources;

namespace Notisblokk
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static readonly String FILENAME = "notes.xml";
        private Note selectedNote;
        private NoteViewModel viewModel;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            viewModel = new NoteViewModel();
            viewModel._notes.CollectionChanged += Notes_CollectionChanged;
            ItemControlNotes.ItemsSource = viewModel._notes;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            String id, description, content;

            // Navigated from EditNote.xml
            if (NavigationContext.QueryString.TryGetValue("id", out id))
            {
                if (NavigationContext.QueryString.TryGetValue("description", out description))
                {
                    if (NavigationContext.QueryString.TryGetValue("content", out content))
                    {
                        Note n = viewModel.GetItem(id);
                        viewModel.ChangeDetails(n, description, content);
                    }
                }
            }else{ // Navigated from NewNote.xml
                if (NavigationContext.QueryString.TryGetValue("description", out description))
                {
                    if (NavigationContext.QueryString.TryGetValue("content", out content))
                    {
                        viewModel.AddNote(description, content);
                        NavigationService.RemoveBackEntry();
                    }
                }
            }
            NavigationService.RemoveBackEntry();
        }

        private void Notes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ItemControlNotes.ItemsSource = null;
            ItemControlNotes.ItemsSource = viewModel._notes;
        }

        private void newNote_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewNote.xaml", UriKind.Relative));
        }

        private void Item_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            selectedNote = ((TextBlock)sender).Tag as Note;
            NotesMenu.IsOpen = true;
        }

        void Change_Tap(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/EditNote.xaml?id=" + selectedNote.Id + "&description=" + selectedNote.Description + "&content=" + selectedNote.Content, UriKind.Relative));
        }

        async void Delete_Tap(object sender, RoutedEventArgs e)
        {
            viewModel.Delete(selectedNote);
        }
    }
}