using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArrangerLibrary.Tests
{

    public class ContainerDividerTester_Section
    {
        private List<IContainer> execute(IContainer _container, IRectangle _item, SortCondition _condition)
        {
            IContainerDivider _Section = new ContainerDivider(_container, _item);
            return _Section.Section(_condition);
        }

        [Fact]
        public void ItemTakesWholeContainer()
        {
            IContainer cnt = new Container(10, 10, 6, 10);
            IRectangle itm = new Item(6, 10);
            List<IContainer> result = execute(cnt, itm, SortCondition.Area);
            Assert.Equal(0, result.Count());
        }

        //that setup should return containers as follows:
        //1) x=10,y=13,h=2,w=6
        //2) x=16,y=10,h=5,w=2
        [Fact]
        public void rect3x6into5x8height()
        {
            IContainer cnt = new Container(10, 10, 5, 8);
            IRectangle itm = new Item(3, 6);
            IContainer result1 = new Container(10, 13, 2, 6);
            IContainer result2 = new Container(16, 10, 5, 2);
            List<IContainer> result = execute(cnt, itm, SortCondition.Highest);
            Assert.Contains<IContainer>(result1, result, new ContainerEquality());
            Assert.Contains<IContainer>(result2, result, new ContainerEquality());
            Assert.Equal(2, result.Count);
        }

        //that setup should return containers as follows:
        //1) x=10,y=13,h=2,w=8
        //2) x=16,y=10,h=3,w=2
        [Fact]
        public void rect3x6into5x8area()
        {
            IContainer cnt = new Container(10, 10, 5, 8);
            IRectangle itm = new Item(3, 6);
            IContainer result1 = new Container(10, 13, 2, 8);
            IContainer result2 = new Container(16, 10, 3, 2);
            List<IContainer> result = execute(cnt, itm, SortCondition.Area);
            Assert.Contains<IContainer>(result1, result, new ContainerEquality());
            Assert.Contains<IContainer>(result2, result, new ContainerEquality());
            Assert.Equal(2, result.Count);
        }

        //that setup should return containers as follows:
        //1) x=10,y=13,h=2,w=8
        //2) x=16,y=10,h=3,w=2
        [Fact]
        public void rect3x6into5x8width()
        {
            IContainer cnt = new Container(10, 10, 5, 8);
            IRectangle itm = new Item(3, 6);
            IContainer result1 = new Container(10, 13, 2, 8);
            IContainer result2 = new Container(16, 10, 3, 2);
            List<IContainer> result = execute(cnt, itm, SortCondition.Widest);
            Assert.Contains<IContainer>(result1, result, new ContainerEquality());
            Assert.Contains<IContainer>(result2, result, new ContainerEquality());
            Assert.Equal(2, result.Count);
        }

        //that setup should return containers as follows:
        //1) x=16,y=10,h=5,w=2
        [Fact]
        public void rect5x6into5x8width()
        {
            IContainer cnt = new Container(10, 10, 5, 8);
            IRectangle itm = new Item(5, 6);
            IContainer result1 = new Container(16, 10, 5, 2);
            List<IContainer> result = execute(cnt, itm, SortCondition.Widest);
            Assert.Contains<IContainer>(result1, result, new ContainerEquality());
            Assert.Equal(1, result.Count);
        }

        //that setup should return containers as follows:
        //1) x=10,y=13,h=2,w=8
        [Fact]
        public void rect3x8into5x8width()
        {
            IContainer cnt = new Container(10, 10, 5, 8);
            IRectangle itm = new Item(3, 8);
            IContainer result1 = new Container(10, 13, 2, 8);
            List<IContainer> result = execute(cnt, itm, SortCondition.Widest);
            Assert.Contains<IContainer>(result1, result, new ContainerEquality());
            Assert.Equal(1, result.Count);
        }

        //that setup should return containers as follows
        //1) x=10,y=11,h=9,w=10
        //2) x=11,y=10,h=1,w=9
        [Fact]
        public void rect1x1into10x10width()
        {
            IContainer cnt = new Container(10, 10, 10, 10);
            IRectangle itm = new Item(1, 1);
            IContainer result1 = new Container(10, 11, 9, 10);
            IContainer result2 = new Container(11, 10, 1, 9);
            List<IContainer> result = execute(cnt, itm, SortCondition.Widest);
            Assert.Contains<IContainer>(result1, result, new ContainerEquality());
            Assert.Contains<IContainer>(result2, result, new ContainerEquality());
            Assert.Equal(2, result.Count);
        }

        //that setup should return containers as follows
        //1) x=10,y=11,h=9,w=1
        //2) x=11,y=10,h=10,w=9
        [Fact]
        public void rect1x1into10x10height()
        {
            IContainer cnt = new Container(10, 10, 10, 10);
            IRectangle itm = new Item(1, 1);
            IContainer result1 = new Container(10, 11, 9, 1);
            IContainer result2 = new Container(11, 10, 10, 9);
            List<IContainer> result = execute(cnt, itm, SortCondition.Highest);
            Assert.Contains<IContainer>(result1, result, new ContainerEquality());
            Assert.Contains<IContainer>(result2, result, new ContainerEquality());
            Assert.Equal(2, result.Count);
        }

        //that setup should return containers as follows
        //1) x=10,y=11,h=9,w=1
        //2) x=11,y=10,h=10,w=9
        [Fact]
        public void rect1x1into10x10area()
        {
            IContainer cnt = new Container(10, 10, 10, 10);
            IRectangle itm = new Item(1, 1);
            IContainer result1 = new Container(10, 11, 9, 1);
            IContainer result2 = new Container(11, 10, 10, 9);
            List<IContainer> result = execute(cnt, itm, SortCondition.Area);
            Assert.Contains<IContainer>(result1, result, new ContainerEquality());
            Assert.Contains<IContainer>(result2, result, new ContainerEquality());
            Assert.Equal(2, result.Count);
        }
    }
}
