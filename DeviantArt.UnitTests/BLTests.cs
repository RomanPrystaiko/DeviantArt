using deviantArt.Shared.BL;
using deviantArt.Shared.DAL;
using deviantArt.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviantArt.UnitTests
{
    [TestClass]
    public class BLTests
    {
        [TestMethod]
        public async Task GetImageSourceToDownloadAsyncTest()
        {
            //arange
            DeviantManager dm = new DeviantManager(new StubDeviantArtRepository());
            var expectedSrc = "Src";

            //act
            var currentSrc = await dm.GetImageSourceToDownloadAsync("FakeId");
            //asert
            Assert.IsTrue(expectedSrc == currentSrc);
        }

        [TestMethod]
        public async Task GetImagesSrcRangeAsyncTest()
        {
            //arange
            DeviantManager dm = new DeviantManager(new StubDeviantArtRepository());
            bool expectedIsDownloadable = false;

            //act
            var c = await dm.GetImagesSrcRangeAsync(0, new CategoryItem() { ImageCategory = ImageCatogory.hot });
            var currentIsDownloadable = c.ToList()[1].IsDownloadable;
            //asert
            Assert.IsTrue(expectedIsDownloadable == currentIsDownloadable);
        }

        [TestMethod]
        public async Task GetImagesSrcRangeAsyncIsSameCategotyTest()
        {
            //arange
            DeviantManager dm = new DeviantManager(new StubDeviantArtRepository());
            var expectedCount = 6;

            //act
            var c = await dm.GetImagesSrcRangeAsync(true, new CategoryItem() { ImageCategory = ImageCatogory.hot });
            c = await dm.GetImagesSrcRangeAsync(true, new CategoryItem() { ImageCategory = ImageCatogory.hot });

            //asert
            Assert.IsTrue(expectedCount == c.ToList().Count());
        }

        [TestMethod]
        public async Task GetImagesSrcRangeAsyncIsNotSameCategoryTest()
        {
            //arange
            DeviantManager dm = new DeviantManager(new StubDeviantArtRepository());
            var expectedCount = 3;

            //act
            var c = await dm.GetImagesSrcRangeAsync(true, new CategoryItem() { ImageCategory = ImageCatogory.hot });
            c = await dm.GetImagesSrcRangeAsync(true, new CategoryItem() { ImageCategory = ImageCatogory.hot });
            c = await dm.GetImagesSrcRangeAsync(false, new CategoryItem() { ImageCategory = ImageCatogory.hot });

            //asert
            Assert.IsTrue(expectedCount == c.ToList().Count());
        }

        [TestMethod]
        public void GetImagesCategoriesTest()
        {
            //arange
            DeviantManager dm = new DeviantManager(new StubDeviantArtRepository());
            var expectedCount = 10;

            //act
            var c = dm.GetImagesCategories();
            //asert
            Assert.IsTrue(expectedCount == c.Count());
        }

    }
}
