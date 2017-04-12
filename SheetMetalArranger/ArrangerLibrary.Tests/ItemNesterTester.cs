using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ArrangerLibrary.Tests
{
    public class ItemNesterTester
    {
        private readonly ITestOutputHelper output;

        public ItemNesterTester(ITestOutputHelper output)
        {
            this.output = output;
        }
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
            Dictionary<uint, IArrangerResult> results = nester.Calculate(SortCondition.Area, SortCondition.Area);
            //all items should fit in one sheet - results.count = 1;
            Assert.Equal(1, results.Count);
            //none items has been left
            Assert.Equal(0, nester.LeftCount);
            output.WriteLine(nester.StringOutput());
        }

        //nesterTest specs
        //sheet size: h=1250, w=2500
        //item1: h=1000, w=1000
        //item2: h=1000, w=1000
        //item3: h=1000, w=1000
        //condition: area
        [Fact]
        public void nesterTest2()
        {
            IRectangle item1 = new Item(1000, 1000);
            IRectangle item2 = new Item(1000, 1000);
            IRectangle item3 = new Item(1000, 1000);
            ItemNester nester = new ItemNester(1250, 2500);
            nester.AddItem(item1);
            nester.AddItem(item2);
            nester.AddItem(item3);
            Dictionary<uint, IArrangerResult> results = nester.Calculate(SortCondition.Area, SortCondition.Area);
            //all items should fit in one sheet - results.count = 1;
            Assert.Equal(2, results.Count);
            //none items has been left
            Assert.Equal(0, nester.LeftCount);
            output.WriteLine(nester.StringOutput());
        }

        //nesterTest specs
        //sheet size: h=1000, w=1500
        //item1: h=500, w=300
        //item2: h=300, w=300
        //item3: h=100, w=500
        //item4: h=400, w=300
        //item5: h=300, w=500
        //item6: h=500, w=200
        //condition: widest
        [Fact]
        public void nesterTest3()
        {
            IRectangle item1 = new Item(500, 300);
            IRectangle item2 = new Item(300, 300);
            IRectangle item3 = new Item(100, 500);
            IRectangle item4 = new Item(400, 300);
            IRectangle item5 = new Item(300, 500);
            IRectangle item6 = new Item(500, 200);
            ItemNester nester = new ItemNester(1000, 1500);
            nester.AddItem(item1);
            nester.AddItem(item2);
            nester.AddItem(item3);
            nester.AddItem(item4);
            nester.AddItem(item5);
            nester.AddItem(item6);
            Dictionary<uint, IArrangerResult> results = nester.Calculate(SortCondition.Area, SortCondition.Area);
            //all items should fit in one sheet - results.count = 1;
            Assert.Equal(1, results.Count);
            //none items has been left
            Assert.Equal(0, nester.LeftCount);
            output.WriteLine(nester.StringOutput());
        }

        [Fact]
        public void nesterMergedSimple()
        {
            ItemNester nester = new ItemNester(6, 6);
            nester.AddItem(new Item(3, 4));
            nester.AddItem(new Item(3, 4));
            nester.AddItem(new Item(6, 1));
            Dictionary<uint, IArrangerResult> results = nester.Calculate(SortCondition.Area, SortCondition.Widest);
            //all items should fit in one sheet - results.count = 1;
            //Assert.Equal(1, results.Count);
            //none items has been left
            Assert.Equal(0, nester.LeftCount);
            output.WriteLine(nester.StringOutput());
        }

        [Fact]
        public void nesterMerged()
        {
            ItemNester nester = new ItemNester(8, 14);
            nester.AddItem(new Item(3, 5));
            nester.AddItem(new Item(5, 3));
            nester.AddItem(new Item(5, 2));
            nester.AddItem(new Item(3, 3));
            nester.AddItem(new Item(4, 3));
            nester.AddItem(new Item(1, 5));
            nester.AddItem(new Item(1, 5));
            for (int i =0; i<6; i++)
            {
                nester.AddItem(new Item(2, 3));
            }
            Dictionary<uint, IArrangerResult> results = nester.Calculate(SortCondition.Widest, SortCondition.Area);
            Assert.Equal(0, nester.LeftCount);
            output.WriteLine(nester.StringOutput());
        }
    }
}
