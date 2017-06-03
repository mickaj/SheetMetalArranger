using Xunit;
using Xunit.Abstractions;

namespace ArrangerLibrary.Tests
{
    public class ItemComparersTests:FactoryBase
    {
        private readonly ITestOutputHelper output;

        public ItemComparersTests(ITestOutputHelper _output)
        {
            output = _output;
        }

        private Item item1;
        private Item item2;

        private void execute(Item _item1, Item _item2, int _expected)
        {
            int result = DefaultFactory.ItemHeightComparer.Compare(_item1, _item2);
            Assert.Equal(_expected, result);
            output.WriteLine("Result: {0}", result);
        }

        [Fact]
        public void EqualHeight()
        {
            item1 = new Item(10, 5);
            item2 = new Item(10, 6);
            execute(item1, item2,0);

        }

        [Fact]
        public void Item1SmallerHeight()
        {
            item1 = new Item(9, 5);
            item2 = new Item(10, 5);
            execute(item1, item2, -1);
        }

        [Fact]
        public void Item1BiggerHeight()
        {
            item1 = new Item(10, 5);
            item2 = new Item(9, 5);
            execute(item1, item2, 1);
        }

        [Fact]
        public void Item1NullHeight()
        {
            item1 = null;
            item2 = new Item(10, 10);
            execute(item1, item2, -1);
        }

        [Fact]
        public void Item2NullHeight()
        {
            item1 = new Item(10, 10);
            item2 = null;
            execute(item1, item2, 1);
        }

        [Fact]
        public void BothNullHeight()
        {
            item1 = null;
            item2 = null;
            execute(item1, item2, 0);
        }
    }
}
