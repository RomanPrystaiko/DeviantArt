using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using deviantArt.Shared.ViewModels;
using deviantArt.Shared.Models;
using deviantArt.Shared.DAL;
using deviantArt.Shared.BL;
using DeviantArtCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviantArt.UnitTests
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void FillUpDeviantImageCategoriesCollectionTest()
        {
            //arange
            MainViewModel vm = new MainViewModel(new StubDeviantManager());
            var previousCount = 0;

            //act
            if (vm.FillImageCategoriesCollection.CanExecute(null))
            {
                vm.FillImageCategoriesCollection.Execute(null);
            }
            var actualCount = vm.DeviantImagesCategoriesCollection.Count;

            //asert
            Assert.IsTrue(actualCount > previousCount);
        }

        [TestMethod]
        public void FillUpDeviantImageItemCollectionAsynWhenFalseTest()
        {
            //arange
            MainViewModel vm = new MainViewModel(new StubDeviantManager());

            //act
            vm.SelectedCategoryItem = new CategoryItem();

            if (vm.FillImageItemCollection.CanExecute(null))
            {
                vm.FillImageItemCollection.Execute(true);
                vm.FillImageItemCollection.Execute(false); // fill up second time.
            }

            var actualCount = vm.DeviantImageItemCollection.Count;

            //asert
            Assert.IsTrue(actualCount == 1);
        }

        [TestMethod]
        public void FillUpDeviantImageItemCollectionAsynWhenTrueTest()
        {
            //arange
            MainViewModel vm = new MainViewModel(new StubDeviantManager());
            //act
            vm.SelectedCategoryItem = new CategoryItem();
            if (vm.FillImageItemCollection.CanExecute(null))
            {
                vm.FillImageItemCollection.Execute(true);
                vm.FillImageItemCollection.Execute(true); // fill up second time.
            }

            var actualCount = vm.DeviantImageItemCollection.Count;

            //asert
            Assert.IsTrue(actualCount == 10);
        }
    }
}
