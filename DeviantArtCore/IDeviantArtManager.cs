using System.Threading.Tasks;

namespace DeviantArtCore
{
    public interface IDeviantArtManager
    {
        Task GetAccessTokenAsync(int clientId, string clientSecreteKey);
        Task<DeviantItemCollection> GetHotestItemsAsync(int offset);
        Task<DeviantItemCollection> GetNewestItemsAsync(int offset);
        Task<DeviantItemCollection> GetUndiscoveredItemsAsync(int offset);
        Task<DeviantItemCollection> GetDailyDeviationsItemsAsync();
        Task<DeviantItemCollection> GetPopularItemsAsync(PopularTimeRange timeRange, int offset);
        Task<DownloadableImage> GetImageSourceToDownloadAsync(string deviationId);
        Task<bool> CheckApiStatus();
    }
}
