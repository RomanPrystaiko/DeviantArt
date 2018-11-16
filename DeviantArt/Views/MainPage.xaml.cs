using DeviantArtCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using deviantArt;
using deviantArt.Shared.ViewModels;
using Windows.Storage.Pickers;
using deviantArt.Shared.Models;
using deviantArt.Shared.BL;
using System.Threading.Tasks;
using deviantArt.Shared.DAL;
using deviantArt.Shared.HelpingClasses;
using System.Windows.Input;
using Unity;
using System.Diagnostics;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DeviantArt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel _viewModel;
        private int index = 0;
        private ScrollViewer _scrollViewer;

        public MainPage()
        {
            this.InitializeComponent();
            _viewModel = (Application.Current as App).Container.Resolve<MainViewModel>();
            DataContext = ViewModel;
        }

        public MainViewModel ViewModel
        {
            get { return _viewModel; }
        }

        private void OnImageCategoriesListBoxLoaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel.FillImageCategoriesCollection.CanExecute(null))
            {
                ViewModel.FillImageCategoriesCollection.Execute(null);
            }

            var lb = sender as ListBox;

            SelectListBoxItem(lb, 0);
        }

        private void OnHamburgerButtonClick(object sender, RoutedEventArgs e)
        {
            deviantArtSplitView.IsPaneOpen = !deviantArtSplitView.IsPaneOpen;
        }

        private void OnImageCategoriesListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProgressRing.IsActive = true;
            var lb = sender as ListBox;
            ViewModel.SelectedCategoryItem = lb.SelectedItem as CategoryItem;

            if (ViewModel.FillImageItemCollection.CanExecute(null))
            {
                ViewModel.FillImageItemCollection.Execute(false);
            }

            ProgressRing.IsActive = false;
        }

        private void GetGridViewScrollViewer(object sender, RoutedEventArgs e)
        {
            _scrollViewer = GridView.ChildrenBreadthFirst().OfType<ScrollViewer>().First();
            _scrollViewer.ViewChanged += OnScrollViewerViewChanged;
        }

        private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            var verticalOffset = scrollViewer.VerticalOffset;
            var maxVerticalOffset = scrollViewer.ScrollableHeight;

            if (maxVerticalOffset < 0 ||
                verticalOffset == maxVerticalOffset)
            {
                if (index == 2)
                {
                    index = 0;
                }

                index++;

                if (index <= 1)
                {
                    ProgressRing.IsActive = true;

                    if (ViewModel.FillImageItemCollection.CanExecute(null))
                    {
                        ViewModel.FillImageItemCollection.Execute(true);
                    }

                    ProgressRing.IsActive = false;
                }
            }
        }

        private void SelectListBoxItem(ListBox listBox, int index)
        {
            if (listBox.Items.Count > 0)
            {
                listBox.SelectedIndex = index;
            }
        }

        private async void OpenFileSavePickerToSaveImage(ImageItem item)
        {
            FileSavePicker fileSavePicker = new FileSavePicker()
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                SuggestedFileName = "DeviantImage"
            };

            fileSavePicker.FileTypeChoices.Add("JPEG files", new List<string>() { ".jpg" });

            var outputFile = await fileSavePicker.PickSaveFileAsync();

            if (outputFile == null)
            {
                // The user cancelled the picking operation
                return;
            }
            else
            {
                if (ViewModel.SaveImage.CanExecute(null))
                {
                    ViewModel.SaveImage.Execute(new Tuple<StorageFile, ImageItem>(outputFile, item));
                }
            }

        }

        private void ClosePopup(object sender, RoutedEventArgs e)
        {
            if (StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = false;
                deviantArtSplitView.Opacity = 1;
                deviantArtSplitView.IsEnabled = true;
            }
        }

        private void SaveImage(object sender, SelectionChangedEventArgs e)
        {
            var gridView = sender as GridView;
            var item = gridView.SelectedItem as ImageItem;

            try
            {
                if (item.IsDownloadable)
                {
                    OpenFileSavePickerToSaveImage(item);
                }
                else
                {
                    if (!StandardPopup.IsOpen)
                    {
                        deviantArtSplitView.IsEnabled = false;
                        deviantArtSplitView.Opacity = 0.5;
                        StandardPopup.IsOpen = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(true, ex.Message);
            }
        }
    }
}
