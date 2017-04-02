using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ArrangerLibrary.Tests
{
    public class ReturnTest
    {
        private readonly ITestOutputHelper output;
        private IRectangle rctTest;

        public ReturnTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        private IRectangle execute()
        {
            rctTest = new Item(20, 20);
            return rctTest = new Item(300, 30);
        }

        [Fact]
        public void ReturnEqualsTest()
        {
            IRectangle rct1 = execute();
            Assert.Equal<uint>(300, rctTest.Height);
            Assert.Equal<uint>(30, rctTest.Width);
            output.WriteLine("Rectangle height: {0}", rctTest.Height);
            output.WriteLine("Rectangle width: {0}", rctTest.Width);
         }
    }
}
