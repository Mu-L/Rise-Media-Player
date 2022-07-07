﻿using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI;
using Rise.App.ViewModels;
using Rise.App.Views;
using Rise.Common.Enums;
using Rise.Common.Helpers;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Rise.App.UserControls
{
    /// <summary>
    /// A base class for pages that present media content.
    /// </summary>
    public partial class MediaPageBase : Page
    {
        /// <summary>
        /// A property that stores the page's selected item.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object),
                typeof(MediaPageBase), new PropertyMetadata(null));

        /// <summary>
        /// A helper to save session state during navigation.
        /// </summary>
        public NavigationHelper NavigationHelper { get; private set; }

        /// <summary>
        /// The ViewModel for this page. Contains the collection of media and
        /// commands responsible for sorting and starting playback.
        /// </summary>
        public MediaCollectionViewModel MediaViewModel { get; private set; }

        /// <summary>
        /// Initializes a new instance of this class with the specified
        /// type of items and ViewModel data source.
        /// </summary>
        public MediaPageBase(MediaItemType itemType, IList viewModelSource)
        {
            if (itemType == MediaItemType.Album ||
                itemType == MediaItemType.Artist ||
                itemType == MediaItemType.Genre)
            {
                MediaViewModel = new(itemType, viewModelSource,
                    App.MViewModel.Songs, App.MPViewModel);
            }
            else
            {
                MediaViewModel = new(itemType, viewModelSource, null, App.MPViewModel);
            }

            NavigationHelper = new(this);
            NavigationHelper.LoadState += NavigationHelper_LoadState;
            NavigationHelper.SaveState += NavigationHelper_SaveState;

            GoToAlbumCommand = new(GoToAlbum);
            GoToArtistCommand = new(GoToArtist);
        }
    }

    // Navigation
    public partial class MediaPageBase
    {
        /// <summary>
        /// A command to navigate to the album with the
        /// specified name.
        /// </summary>
        public RelayCommand<string> GoToAlbumCommand { get; private set; }

        /// <summary>
        /// A command to navigate to the artist with the
        /// specified name.
        /// </summary>
        public RelayCommand<string> GoToArtistCommand { get; private set; }

        /// <summary>
        /// Navigates to the album with the specified name.
        /// </summary>
        protected void GoToAlbum(string name)
            => _ = Frame.Navigate(typeof(AlbumSongsPage), name);

        /// <summary>
        /// Navigates to the artist with the specified name.
        /// </summary>
        protected void GoToArtist(string name)
            => _ = Frame.Navigate(typeof(ArtistSongsPage), name);
    }

    // Session state
    public partial class MediaPageBase
    {
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.PageState != null)
            {
                bool result = e.PageState.TryGetValue("Ascending", out var asc);
                if (result)
                    MediaViewModel.UpdateSortDirection((bool)asc ?
                        SortDirection.Ascending : SortDirection.Descending);

                result = e.PageState.TryGetValue("Property", out var prop);
                if (result)
                    MediaViewModel.SortBy(prop.ToString());
            }
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            e.PageState["Ascending"] = MediaViewModel.
                CurrentSortDirection == SortDirection.Ascending;

            e.PageState["Property"] = MediaViewModel.CurrentSortProperty;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
            => NavigationHelper.OnNavigatedTo(e);

        protected override void OnNavigatedFrom(NavigationEventArgs e)
            => NavigationHelper.OnNavigatedFrom(e);
    }
}
