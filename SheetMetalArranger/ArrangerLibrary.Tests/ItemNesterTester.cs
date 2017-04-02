using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArrangerLibrary.Tests
{
    public class ItemNesterTester
    {
        //nesterTest specs
        //sheet size: h=1250, w=2500
        //item1: h=1000, w=1000
        //item2: h=200, w=1000
        //condition: area
        [Fact]
        public void nesterTest1()
        {
            IRectangle item1 = new Item(1000, 1000);
            IRectangle item2 = new Item(200, 1000);
            ItemNester nester = new ItemNester(1250, 2500);
            nester.AddItem(item1);
            nester.AddItem(item2);
            Dictionary<uint, IArrangerResult> results = nester.Calculate(SortCondition.Area);
            //all items should fit in one sheet - results.count = 1;
            Assert.Equal(1, results.Count);
            //none items has been left
            Assert.Equal(0, nester.LeftCount);
        }
    }
}
