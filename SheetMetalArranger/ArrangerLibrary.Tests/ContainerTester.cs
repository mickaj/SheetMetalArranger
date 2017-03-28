using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArrangerLibrary.Tests
{
    public class ContainerTester_CheckIfFits
    {
        private bool execute(IContainer _container, IRectangle _item)
        {
            return _container.CheckIfFits(_item);
        }

        [Fact]
        public void rectangleSmaller()
        {
            IContainer testContainer = new Container(0,0,10,20);

            IRectangle testItem = new Item(5, 10);

            Assert.Equal(true, execute(testContainer, testItem));
        }

        [Fact]
        public void rectangleEqual()
        {
            IContainer testContainer = new Container(0, 0, 10, 20);

            IRectangle testItem = new Item(10, 20);

            Assert.Equal(true, execute(testContainer, testItem));
        }

        [Fact]
        public void rectangleToHigh()
        {
            IContainer testContainer = new Container(0, 0, 10, 20);

            IRectangle testItem = new Item(15, 20);

            Assert.Equal(false, execute(testContainer, testItem));
        }

        [Fact]
        public void rectangleToWide()
        {
            IContainer testContainer = new Container(0, 0, 10, 20);

            IRectangle testItem = new Item(10, 25);

            Assert.Equal(false, execute(testContainer, testItem));
        }

        [Fact]
        public void rectangleExceedsBoth()
        {
            IContainer testContainer = new Container(0, 0, 10, 20);

            IRectangle testItem = new Item(15, 25);

            Assert.Equal(false, execute(testContainer, testItem));
        }
    }
}
