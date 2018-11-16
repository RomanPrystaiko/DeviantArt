using deviantArt.Shared.BL;
using deviantArt.Shared.DAL;
using deviantArt.Shared.HelpingClasses;
using deviantArt.Shared.Models;
using DeviantArtCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace deviantArt.Shared.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<ImageItem> _deviantImageItemCollection;
        private ObservableCollection<CategoryItem> _deviantImagesCategoriesCollection;
        RelayCommand _saveImage;
        RelayCommand _fillImageCategoriesCollection;
        RelayCommand _fillImageItemCollection;
        private IDeviantManager _manager;

        #endregion

        public MainViewModel(IDeviantManager manager)
        {
            _deviantImagesCategoriesCollection = new ObservableCollection<CategoryItem>();
            _deviantImageItemCollection = new ObservableCollection<ImageItem>();
            _manager = manager;
        }

        #region Properties

        public ObservableCollection<ImageItem> DeviantImageItemCollection
        {
            get
            {
                if (_deviantImageItemCollection != null)
                {
                    return _deviantImageItemCollection;
                }
                else
                {
                    throw new NullReferenceException("Image source collection is null");
                }
            }

            private set
            {
                _deviantImageItemCollection = value;
                OnPropertyChanged("DeviantImageItemCollection");
            }
        }

        public ObservableCollection<CategoryItem> DeviantImagesCategoriesCollection
        {
            get
            {
                if (_deviantImageItemCollection != null)
                {
                    return _deviantImagesCategoriesCollection;
                }
                else
                {
                    throw new NullReferenceException("Image categories collection is null");
                }
            }

            private set
            {
                _deviantImagesCategoriesCollection = value;
                OnPropertyChanged("DeviantImagesCategoriesCollection");
            }
        }

        public CategoryItem SelectedCategoryItem { get; set; }

        public ICommand FillImageItemCollection
        {
            get
            {
                if (_fillImageItemCollection == null)
                    _fillImageItemCollection = new RelayCommand(ExecuteFillImageItemCollectionCommand, CanExecuteFillImageItemCollectionCommand);
                return _fillImageItemCollection;
            }
        }

        public ICommand FillImageCategoriesCollection
        {
            get
            {
                if (_fillImageCategoriesCollection == null)
                    _fillImageCategoriesCollection = new RelayCommand(ExecuteFillImageCategoriesCollectionCommand, CanExecuteFillImageCategoriesCollectionCommand);
                return _fillImageCategoriesCollection;
            }
        }

        public ICommand SaveImage
        {
            get
            {
                if (_saveImage == null)
                    _saveImage = new RelayCommand(ExecuteSaveImageCommand, CanExecuteSaveImageCommand);
                return _saveImage;
            }
        }

        #endregion

        #region Methods
        private async void ExecuteSaveImageCommand(object parameter)
        {
            var tuple = parameter as Tuple<StorageFile, ImageItem>;

            if (tuple != null && tuple.Item2.IsDownloadable)
            {
                var url = await _manager.GetImageSourceToDownloadAsync(tuple.Item2.DeviationId);
                var uri = new Uri(url);    // URL of thumbnail.
                var thumbNail = RandomAccessStreamReference.CreateFromUri(uri);
                var remoteFile = await StorageFile.CreateStreamedFileFromUriAsync("file", uri, thumbNail);
                var storedFile = await remoteFile.CopyAsync(KnownFolders.SavedPictures, "DeviantImage", NameCollisionOption.GenerateUniqueName);
                await storedFile.CopyAndReplaceAsync(tuple.Item1);
                await storedFile.DeleteAsync();
            }
        }

        private bool CanExecuteSaveImageCommand(object parameter)
        {
            if (_manager != null && InternetHelper.CheckInternetConnection())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ExecuteFillImageCategoriesCollectionCommand(object parameter)
        {
            var ci = new ObservableCollection<CategoryItem>(_manager.GetImagesCategories());

            foreach (var item in ci)
            {
                DeviantImagesCategoriesCollection.Add(item);
            }
        }

        private bool CanExecuteFillImageCategoriesCollectionCommand(object parameter)
        {
            if (_manager != null && DeviantImagesCategoriesCollection != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void ExecuteFillImageItemCollectionCommand(object parameter)
        {
            try
            {
                var isSameCategory = (bool)(parameter as bool?);

                if (!isSameCategory)
                {
                    ClearDeviantImageItemCollection();
                }

                var imageItemcollection = await _manager.GetImagesSrcRangeAsync(isSameCategory, SelectedCategoryItem);

                foreach (var item in imageItemcollection)
                {
                    DeviantImageItemCollection.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(true, ex.Message);
            }
        }
        private bool CanExecuteFillImageItemCollectionCommand(object parameter)
        {
            if (_manager != null && DeviantImagesCategoriesCollection != null && SelectedCategoryItem != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ClearDeviantImageItemCollection()
        {
            if (DeviantImageItemCollection != null)
            {
                DeviantImageItemCollection.Clear();
            }
        }

        #endregion
    }
}
