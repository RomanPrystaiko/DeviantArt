using DeviantArtCore;
using System.Threading.Tasks;

namespace deviantArt.Shared.DAL
{
    public interface IDeviantArtRepository
    {
        Task<DeviantItemCollection> GetDeviantItemsRangeAsync(int offset, ImageCatogory imageCategory);
        Task<DownloadableImage> DownloadImageInfoAsync(string deviationId);
    }
}
