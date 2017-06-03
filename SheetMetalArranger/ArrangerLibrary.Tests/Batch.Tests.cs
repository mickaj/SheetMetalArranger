using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary.Tests
{
    public class BatchTests:FactoryBase
    {
        private readonly ITestOutputHelper output;

        private Batch batch;
        private Item existing;
        private Item nonExisting;

        public BatchTests(ITestOutputHelper _output)
        {
            output = _output;
            batch = new Batch();
            batch.AddItem(new Item(100, 100, 1, true));
            existing = new Item(105, 105, 0, true);
            batch.AddItem(existing);
            nonExisting = new Item(2, 2, 0, true);
        }

        private void PrepareSortingTest()
        {
            batch.Clear();
            batch.AddItem(new Item(1, 4));
            batch.AddItem(new Item(2, 5));
            batch.AddItem(new Item(3, 6));
            batch.AddItem(new Item(2, 6));
            batch.AddItem(new Item(3, 4));
        }

        [Fact]
        public void RemoveExisting()
        {
            Assert.True(batch.RemoveItem(existing));
        }

        [Fact]
        public void RemoveNonExisting()
        {
            Assert.False(batch.RemoveItem(nonExisting));
        }

        [Fact]
        public void SortAHW()
        {
            PrepareSortingTest();
            IItem results = batch.GetFirst(DefaultFactory.ItemAreaComparer, DefaultFactory.ItemHeightComparer,DefaultFactory.ItemWidthComparer);
            Item expected = new Item(3, 6, 0, false);
            Assert.Equal(expected, results, DefaultFactory.ItemEqualityComparer);
            PrintBatch(batch.Content());
        }

        [Fact]
        public void SortAWH()
        {
            PrepareSortingTest();
            IItem results = batch.GetFirst(DefaultFactory.ItemAreaComparer, DefaultFactory.ItemWidthComparer, DefaultFactory.ItemHeightComparer);
            Item expected = new Item(3, 6, 0, false);
            Assert.Equal(expected, results, DefaultFactory.ItemEqualityComparer);
            PrintBatch(batch.Content());
        }

        [Fact]
        public void SortHWA()
        {
            PrepareSortingTest();
            IItem results = batch.GetFirst(DefaultFactory.ItemAreaComparer, DefaultFactory.ItemWidthComparer, DefaultFactory.ItemHeightComparer);
            Item expected = new Item(3, 6, 0, false);
            Assert.Equal(expected, results, DefaultFactory.ItemEqualityComparer);
            PrintBatch(batch.Content());
        }

        private void PrintBatch(List<IItem> _list)
        {
            foreach(Item item in _list)
            {
                output.WriteLine("--------------------------------------");
                output.WriteLine("ItemHeight: {0}\n ItemWidth: {1}\n Margin: {2}\n Rotatable: {3}\nArea: {4}", item.ItemHeight, item.ItemWidth, item.Margin, item.Rotatable, item.Area);
                output.WriteLine("--------------------------------------");
            }
        }


    }
}
