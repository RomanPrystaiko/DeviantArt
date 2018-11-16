using deviantArt.Shared.DAL;
using deviantArt.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace deviantArt.Shared.BL
{
    public interface IDeviantManager
    {
        Task<IEnumerable<ImageItem>> GetImagesSrcRangeAsync(bool isSameCategory, CategoryItem selectedCategoryItem);
        IEnumerable<CategoryItem> GetImagesCategories();

        Task<string> GetImageSourceToDownloadAsync(string deviationId);
    }
}
