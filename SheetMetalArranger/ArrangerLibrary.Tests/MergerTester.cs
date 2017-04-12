using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ArrangerLibrary.Tests
{
    public class MergerTester
    {
        private readonly ITestOutputHelper output;
        private IMergeRects merger;
        private IMergeRects merger2;

        public MergerTester(ITestOutputHelper output)
        {
            this.output = output;
            merger = new Merger();
            merger2 = new Merger();
        }

        [Fact]
        public void mergeTestHorizontal()
        {
            IContainer cnt1 = new Container(3, 8, 3, 4);
            IContainer cnt2 = new Container(7, 8, 3, 10);
            IContainer cnt3 = new Container(18, 1, 10, 2);
            List<IContainer> input = new List<IContainer>();
            input.Add(cnt1);
            input.Add(cnt2);
            input.Add(cnt3);
            List<IContainer> merged = merger2.GetMerged(input);
            Assert.Equal(2, merged.Count);
            output.WriteLine(merger.GetResultString());
        }

        [Fact]
        public void mergeTestVertical()
        {
            IContainer cnt1 = new Container(0, 0, 3, 4);
            IContainer cnt2 = new Container(0, 3, 10, 4);
            IContainer cnt3 = new Container(18, 1, 10, 2);
            List<IContainer> input = new List<IContainer>();
            input.Add(cnt1);
            input.Add(cnt2);
            input.Add(cnt3);
            List<IContainer> merged = merger.GetMerged(input);
            Assert.Equal(2, merged.Count);
            output.WriteLine(merger.GetResultString());
        }

        [Fact]
        public void noneMerged()
        {
            IContainer cnt1 = new Container(0, 0, 3, 4);
            IContainer cnt2 = new Container(0, 3, 10, 3);
            IContainer cnt3 = new Container(18, 1, 10, 2);
            List<IContainer> input = new List<IContainer>();
            input.Add(cnt1);
            input.Add(cnt2);
            input.Add(cnt3);
            List<IContainer> merged = merger.GetMerged(input);
            Assert.Equal(3, merged.Count);
            output.WriteLine(merger.GetResultString());
        }

        [Fact]
        public void merge1()
        {
            IContainer cnt1 = new Container(5, 5, 3, 4);
            IContainer cnt2 = new Container(0, 10, 10, 3);
            IContainer cnt3 = new Container(18, 1, 10, 2);
            IContainer cnt4 = new Container(0, 5, 3, 5);
            List<IContainer> input = new List<IContainer>();
            input.Add(cnt1);
            input.Add(cnt2);
            input.Add(cnt3);
            input.Add(cnt4);
            List<IContainer> merged = merger.GetMerged(input);
            Assert.Equal(3, merged.Count);
            output.WriteLine(merger.GetResultString());
        }
    }
}
