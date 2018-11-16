using deviantArt.Shared.DAL;
using deviantArt.Shared.Models;
using DeviantArtCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace deviantArt.Shared.BL
{
    public class DeviantManager : IDeviantManager
    {
        private IDeviantArtRepository _repository;
        private int _offset;

        public DeviantManager(IDeviantArtRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetImageSourceToDownloadAsync(string deviationId)
        {
            DownloadableImage imageInfo = await _repository.DownloadImageInfoAsync(deviationId);
            return imageInfo.Src;
        }

        public async Task<IEnumerable<ImageItem>> GetImagesSrcRangeAsync(int offset, CategoryItem selectedCategoryItem)
        {
            List<ImageItem> list = null;

            if (offset >= 0)
            {
                DeviantItemCollection collction = await _repository.GetDeviantItemsRangeAsync(offset, selectedCategoryItem.ImageCategory);
                list = await PullOutImageSrcesAsync(collction);
            }

            return list;
        }

        public async Task<IEnumerable<ImageItem>> GetImagesSrcRangeAsync(bool isSameCategory, CategoryItem selectedCategoryItem)
        {
            var srcCollection = new List<ImageItem>();
            return srcCollection = await GetFirstOrNextSrcCollectionAsync(isSameCategory, selectedCategoryItem.ImageCategory);
        }

        public IEnumerable<CategoryItem> GetImagesCategories()
        {
            var list = new List<CategoryItem>()
            {
               new CategoryItem(){ ItemName = "Newest", IconText = "\uE7BF", ImageCategory = ImageCatogory.newest},
               new CategoryItem() { ItemName = "What's hot", IconText = "\uE113", ImageCategory = ImageCatogory.hot},
               new CategoryItem() { ItemName = "Undiscovered", IconText = "\uE9CE", ImageCategory = ImageCatogory.undiscovered},
               new CategoryItem() { ItemName = "Daily deviations", IconText = "\uE909", ImageCategory = ImageCatogory.dailyDeviation},
               new CategoryItem() { ItemName = "Popular 8 hours", IconText = "\uE916", ImageCategory = ImageCatogory.popular8hr },
               new CategoryItem() { ItemName = "Popular 24 hours", IconText = "\uE916", ImageCategory = ImageCatogory.popular24hrs },
               new CategoryItem() { ItemName = "Popular 3 days", IconText = "\uE916", ImageCategory = ImageCatogory.popular3days },
               new CategoryItem() { ItemName = "Popular 1 week", IconText = "\uE916" , ImageCategory = ImageCatogory.popular1week },
               new CategoryItem() { ItemName = "Popular 1 month", IconText = "\uE916", ImageCategory = ImageCatogory.popular1month},
               new CategoryItem() { ItemName = "Popular All Time", IconText = "\uE895", ImageCategory = ImageCatogory.popularAlltime}
            };

            return list;
        }

        private async Task<List<ImageItem>> GetFirstOrNextSrcCollectionAsync(bool isSameCollection, ImageCatogory ic)
        {
            if (isSameCollection)
            {
                return await GetNextSrcRangeAsync(ic);
            }
            else
            {
                return await GetFirstSrcRangeAsync(ic);
            }
        }

        private async Task<List<ImageItem>> GetFirstSrcRangeAsync(ImageCatogory ic)
        {
            ResetOffset();
            var c = await _repository.GetDeviantItemsRangeAsync(_offset, ic);
            _offset = c.NextOffset;
            return await PullOutImageSrcesAsync(c);
        }

        private async Task<List<ImageItem>> GetNextSrcRangeAsync(ImageCatogory ic)
        {
            var c = await _repository.GetDeviantItemsRangeAsync(_offset, ic);
            _offset = c.NextOffset;
            return await PullOutImageSrcesAsync(c);
        }

        private void ResetOffset()
        {
            _offset = 0;
        }

        private Task<List<ImageItem>> PullOutImageSrcesAsync(DeviantItemCollection collection)
        {
            var task = new Task<List<ImageItem>>(() =>
            {
                var list = new List<ImageItem>();

                if (collection != null)
                {
                    foreach (var item in collection.DeviantItems)
                    {
                        if (item.Content != null)
                        {
                            var imageItem = new ImageItem() { Src = item.Preview.Src, IsDownloadable = item.IsDownloadable, ContentSource = item.Content.Src, DeviationId = item.DeviationId };
                            list.Add(imageItem);
                        }
                    }
                }

                return list;
            });

            task.Start();

            return task;
        }
    }
}
