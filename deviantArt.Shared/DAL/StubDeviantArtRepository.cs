using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DeviantArtCore;

namespace deviantArt.Shared.DAL
{
    public class StubDeviantArtRepository : IDeviantArtRepository
    {
        public Task<DownloadableImage> DownloadImageInfoAsync(string deviationId)
        {
            var image = new DownloadableImage() { Src = "Src" };
            return Task.FromResult(image);
        }

        public Task<DeviantItemCollection> GetDeviantItemsRangeAsync(int offset, ImageCatogory imageCategory)
        {
            DeviantItemCollection collection = new DeviantItemCollection();
            collection.NextOffset = 20;

            if (offset == 0)
            {
                collection.DeviantItems = new List<DeviantItem>()
            {
                new DeviantItem(){ IsDownloadable = true, Preview = new Preview(){Src = "Src" }, Content = new Content(){Src = "Src" }, DeviationId = "Id" },
                new DeviantItem(){ IsDownloadable = false, Preview = new Preview(){Src = "Src" }, Content = new Content(){Src = "Src" }, DeviationId = "Id"  },
                new DeviantItem(){ IsDownloadable = true, Preview = new Preview(){Src = "Src" }, Content = new Content(){Src = "Src" }, DeviationId = "Id"  }
            };

            }
            else
            {
                collection.DeviantItems = new List<DeviantItem>()
            {
                new DeviantItem(){ IsDownloadable = true, Preview = new Preview(){Src = "Src" }, Content = new Content(){Src = "Src" }, DeviationId = "Id" },
                new DeviantItem(){ IsDownloadable = false, Preview = new Preview(){Src = "Src" }, Content = new Content(){Src = "Src" }, DeviationId = "Id"  },
                new DeviantItem(){ IsDownloadable = true, Preview = new Preview(){Src = "Src" }, Content = new Content(){Src = "Src" }, DeviationId = "Id"  },
                new DeviantItem(){ IsDownloadable = true, Preview = new Preview(){Src = "Src" }, Content = new Content(){Src = "Src" }, DeviationId = "Id" },
                new DeviantItem(){ IsDownloadable = false, Preview = new Preview(){Src = "Src" }, Content = new Content(){Src = "Src" }, DeviationId = "Id"  },
                new DeviantItem(){ IsDownloadable = true, Preview = new Preview(){Src = "Src" }, Content = new Content(){Src = "Src" }, DeviationId = "Id"  }
            };

            }


            return Task.FromResult<DeviantItemCollection>(collection);
        }
    }
}
