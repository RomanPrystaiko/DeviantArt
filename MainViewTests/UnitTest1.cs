using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MainViewTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //arange
            MainViewModel vm = new MainViewModel();
            var expectedCount = 10;

            //act
            vm.FillUpDeviantImageCategoriesCollection();
            var actualCount = vm.DeviantImagesCategoriesCollection.Count;

            //asert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
