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
using System.Threading.Tasks;
using Notisblokk.Theme;
using Notisblokk.Model;

namespace Notisblokk
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Note selectedNote;
        private NoteViewModel viewModel;
        ThemeHandler handler;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            viewModel = new NoteViewModel();
            viewModel.Notes.CollectionChanged += Notes_CollectionChanged;
            ItemControlNotes.ItemsSource = viewModel.Notes;
            handler = ThemeHandler.getInstance();
            handler.setApp(App.Current);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            String id, description, content;

            // Navigated from EditNote.xml
            if (NavigationContext.QueryString.TryGetValue("id", out id))
            {
                if (NavigationContext.QueryString.TryGetValue("description", out description))
                {
                    if (NavigationContext.QueryString.TryGetValue("content", out content))
                    {
                        viewModel.ChangeDetails(id, description, content);
                    }
                }
            }else{ // Navigated from NewNote.xml
                if (NavigationContext.QueryString.TryGetValue("description", out description))
                {
                    if (NavigationContext.QueryString.TryGetValue("content", out content))
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
                            viewModel.AddNote(description, content);
                        });
                    }
                }
            }
            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
            NavigationContext.QueryString.Clear();
                            
        }

        private void Notes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                ItemControlNotes.ItemsSource = null;
                ItemControlNotes.ItemsSource = viewModel.Notes;
            });
        }

        private void newNote_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewNote.xaml", UriKind.Relative));
        }

        private void Item_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            selectedNote = ((TextBlock)sender).Tag as Note;
            selectedNote = viewModel.GetItem(selectedNote.Id);

            ContextHeader.Header = selectedNote.Description;
            if (selectedNote.IsPinned)
                ContextPin.Header = "Remove pin";
            else
                ContextPin.Header = "Pin to start";
            NotesMenu.IsOpen = true;
        }

        void Change_Tap(object sender, RoutedEventArgs e)
        {
            NotesMenu.IsOpen = false;
            NavigationService.Navigate(new Uri("/EditNote.xaml?id=" + selectedNote.Id + 
                "&description=" + selectedNote.Description + "&content=" + selectedNote.Content, UriKind.Relative));
        }

        void Delete_Tap(object sender, RoutedEventArgs e)
        {
            NotesMenu.IsOpen = false;
            if (MessageBox.Show("Are you sure?", "Delete " + selectedNote.Description + "?",
                MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if (selectedNote.IsPinned)
                {
                    ShellTile activeTile = ShellTile.ActiveTiles.First();
                    activeTile.Update(Flipper.ClearTileData());
                }
                Dispatcher.BeginInvoke(() =>
                {
                    viewModel.Delete(selectedNote);
                });
            }
        }

        private void PinTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (selectedNote.IsPinned)
            {
                ShellTile activeTile = ShellTile.ActiveTiles.First();
                activeTile.Update(Flipper.ClearTileData());
            }
            else
            {
                ShellTile tile = ShellTile.ActiveTiles.First();
                tile.Update(Flipper.GetFlipTile(selectedNote));
            }

            viewModel.PinnedChanged(selectedNote);
        }

        private void ScrollViewer_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // Disables context menu when ViewModel is empty
            e.Handled = true;
        }
    }
}