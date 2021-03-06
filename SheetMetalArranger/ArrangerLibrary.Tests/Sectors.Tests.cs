﻿using System.Collections.Generic;
using ArrangerLibrary.Abstractions;
using ArrangerLibrary;
using Xunit;
using Xunit.Abstractions;

namespace ArrangerLibrary.Tests
{
    public class SectorsTests : FactoryBase
    {
        private readonly ITestOutputHelper output;

        private List<IBox> execute(IBox _container, IItem _item, ISector _sector)
        {

            return _sector.DoSection(_container, _item, false);
        }

        public SectorsTests(ITestOutputHelper _output)
        {
            output = _output;
        }

        [Fact]
        public void ItemTakesWholeBox()
        {
            IBox cnt = new Box(10, 10, 6, 10);
            IItem itm = new Item(6, 10);
            List<IBox> resultH = execute(cnt, itm, DefaultFactory.HSector);
            Assert.Equal(0, resultH.Count);
            List<IBox> resultV = execute(cnt, itm, DefaultFactory.VSector);
            Assert.Equal(0, resultV.Count);
        }

        [Fact]
        public void rect3x6into5x8()
        {
            IBox cnt = new Box(10, 10, 5, 8);
            IItem itm = new Item(3, 6);
            IBox resultH1 = new Box(10, 13, 2, 8);
            IBox resultH2 = new Box(16, 10, 3, 2);
            IBox resultV1 = new Box(10, 13, 2, 6);
            IBox resultV2 = new Box(16, 10, 5, 2);
            List<IBox> resultH = execute(cnt, itm, DefaultFactory.HSector);
            List<IBox> resultV = execute(cnt, itm, DefaultFactory.VSector);
            Assert.Contains<IBox>(resultH1, resultH, DefaultFactory.BoxEqualityComparer);
            Assert.Contains<IBox>(resultH2, resultH, DefaultFactory.BoxEqualityComparer);
            Assert.Equal(2, resultH.Count);
            Assert.Contains<IBox>(resultV1, resultV, DefaultFactory.BoxEqualityComparer);
            Assert.Contains<IBox>(resultV2, resultV, DefaultFactory.BoxEqualityComparer);
            Assert.Equal(2, resultV.Count);
        }

        [Fact]
        public void rect5x6into5x8width()
        {
            IBox cnt = new Box(10, 10, 5, 8);
            IItem itm = new Item(5, 6);
            IBox result = new Box(16, 10, 5, 2);
            List<IBox> resultV = execute(cnt, itm, DefaultFactory.VSector);
            List<IBox> resultH = execute(cnt, itm, DefaultFactory.HSector);
            Assert.Contains<IBox>(result, resultV, DefaultFactory.BoxEqualityComparer);
            Assert.Contains<IBox>(result, resultH, DefaultFactory.BoxEqualityComparer);
            Assert.Equal(1, resultV.Count);
            Assert.Equal(1, resultH.Count);
        }

        [Fact]
        public void rect1x1into10x10()
        {
            IBox cnt = new Box(10, 10, 10, 10);
            IItem itm = new Item(1, 1);
            IBox resultV1 = new Box(10, 11, 9, 1);
            IBox resultV2 = new Box(11, 10, 10, 9);
            IBox resultH1 = new Box(10, 11, 9, 10);
            IBox resultH2 = new Box(11, 10, 1, 9);
            List<IBox> resultV = execute(cnt, itm, DefaultFactory.VSector);
            List<IBox> resultH = execute(cnt, itm, DefaultFactory.HSector);
            Assert.Contains<IBox>(resultV1, resultV, DefaultFactory.BoxEqualityComparer);
            Assert.Contains<IBox>(resultV2, resultV, DefaultFactory.BoxEqualityComparer);
            Assert.Equal(2, resultV.Count);
            Assert.Contains<IBox>(resultH1, resultH, DefaultFactory.BoxEqualityComparer);
            Assert.Contains<IBox>(resultH2, resultH, DefaultFactory.BoxEqualityComparer);
            Assert.Equal(2, resultH.Count);
        }

    }
}
