using DeviantArtCore;
using System.Threading.Tasks;
using System;

namespace deviantArt.Shared.DAL
{
    public class DeviantArtRepository : IDeviantArtRepository
    {
        private IDeviantArtManager manager;

        public DeviantArtRepository(IDeviantArtManager manager)
        {
            this.manager = manager;
        }

        public async Task<DownloadableImage> DownloadImageInfoAsync(string deviationId)
        {
            return await manager.GetImageSourceToDownloadAsync(deviationId);
        }

        public async Task<DeviantItemCollection> GetDeviantItemsRangeAsync(int offset, ImageCatogory imageCategory)
        {
            var collection = new DeviantItemCollection();

            switch (imageCategory)
            {
                case ImageCatogory.hot:
                    var c = await manager.GetHotestItemsAsync(offset);
                    collection = c;
                    break;
                case ImageCatogory.newest:
                    var c1 = await manager.GetNewestItemsAsync(offset);
                    collection = c1;
                    break;
                case ImageCatogory.undiscovered:
                    var c2 = await manager.GetUndiscoveredItemsAsync(offset);
                    collection = c2;
                    break;
                case ImageCatogory.popular8hr:
                    var c3 = await manager.GetPopularItemsAsync(PopularTimeRange.hr8, offset);
                    collection = c3;
                    break;
                case ImageCatogory.popular24hrs:
                    var c4 = await manager.GetPopularItemsAsync(PopularTimeRange.hr24, offset);
                    collection = c4;
                    break;
                case ImageCatogory.popular3days:
                    var c5 = await manager.GetPopularItemsAsync(PopularTimeRange.days3, offset);
                    collection = c5;
                    break;
                case ImageCatogory.popular1week:
                    var c6 = await manager.GetPopularItemsAsync(PopularTimeRange.oneWeek, offset);
                    collection = c6;
                    break;
                case ImageCatogory.popular1month:
                    var c7 = await manager.GetPopularItemsAsync(PopularTimeRange.oneMonth, offset);
                    collection = c7;
                    break;
                case ImageCatogory.popularAlltime:
                    var c8 = await manager.GetPopularItemsAsync(PopularTimeRange.allTime, offset);
                    collection = c8;
                    break;
                case ImageCatogory.dailyDeviation:
                    var c9 = await manager.GetDailyDeviationsItemsAsync();
                    collection = c9;
                    break;
            }

            return collection;
        }
    }
}
