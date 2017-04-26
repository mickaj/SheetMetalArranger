using Xunit;
using Xunit.Abstractions;

namespace ArrangerLibrary.Tests
{
    public class ItemTests
    {
        private readonly ITestOutputHelper output;
        public ItemTests(ITestOutputHelper _output)
        {
            output = _output;
        }

        [Fact]
        public void CopyTest()
        {
            Item item = new Item(10, 20, 5, true);
            Item copiedItem = (Item)item.CreateCopy();
            Assert.Equal(item, copiedItem, ItemEquality.Instance);
            output.WriteLine("ItemHeight: {0}\n ItemWidth: {1}\n Margin: {2}\n Rotatable: {3}", copiedItem.ItemHeight, copiedItem.ItemWidth, copiedItem.Margin, copiedItem.Rotatable);

        }
    }
}
