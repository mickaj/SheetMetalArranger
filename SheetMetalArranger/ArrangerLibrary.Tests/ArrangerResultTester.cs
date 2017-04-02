using Xunit;

namespace ArrangerLibrary.Tests
{
    public class ArrangerResultTester_Constructor
    {
        private IArrangerResult result;

        private void execute(uint _height, uint _width)
        {
            result = new ArrangerResult(_height, _width);
        }

        [Fact]
        public void constructorTest()
        {
            execute(1250,2500);
            Assert.Equal<uint>(1250, result.SheetHeight);
            Assert.Equal<uint>(2500, result.SheetWidth);
        }
    }
}
