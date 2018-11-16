using deviantArt.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace deviantArt.Shared.BL
{
    public class StubDeviantManager : IDeviantManager
    {
        public IEnumerable<CategoryItem> GetImagesCategories()
        {
            var list = new List<CategoryItem>() { new CategoryItem() };
            return list;
        }

        public Task<string> GetImageSourceToDownloadAsync(string deviationId)
        {
            return Task.FromResult("true");
        }

        public Task<IEnumerable<ImageItem>> GetImagesSrcRangeAsync(bool isSameCategory, CategoryItem selectedCategoryItem)
        {

            if (isSameCategory)
            {
                var list = new List<ImageItem>() { new ImageItem(), new ImageItem(), new ImageItem(), new ImageItem(), new ImageItem() };
                return Task.FromResult(list as IEnumerable<ImageItem>);
            }
            else
            {
                var list = new List<ImageItem>() { new ImageItem() };
                return Task.FromResult(list as IEnumerable<ImageItem>);
            }

        }
    }

}
