using System;
using System.Collections.Generic;
using ArrangerLibrary.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace ArrangerLibrary.Tests
{
    public class PanelTests
    {
        private IPanel panel;
        private readonly ITestOutputHelper output;

        public PanelTests(ITestOutputHelper _output)
        {
            output = _output;
        }

        [Fact]
        public void UtilisationZero()
        {
            panel = new Panel(1000, 2000);
            Assert.Equal(0, panel.Utilisation);
        }

        [Fact]
        public void GetBoxByIndex0()
        {
            panel = new Panel(1000, 2000);
            IBox boxFromPanel = panel.GetBox(0);
            IBox expectedBox = new Box(0, 0, 1000, 2000);
            Assert.Equal(expectedBox, boxFromPanel, BoxEquality.Instance);
        }

        [Fact]
        public void TryToGetBoxOutOfCollection()
        {
            panel = new Panel(1000, 2000);
            Assert.Throws<InvalidOperationException>(() => panel.GetBox(1));
        }

        [Fact]
        public void TryToAssignToBoxOutOfCollection()
        {
            panel = new Panel(1000, 2000);
            IBox newBox = new Box(10, 10, 10, 10);
            IItem newItem = new Item(10, 10, 10);
            Assert.Throws<InvalidOperationException>(() => panel.Assign(newBox, newItem, HSector.Instance));
        }

        [Fact]
        public void Assign1to1AndUtilisation1()
        {
            panel = new Panel(1000, 2000);
            IItem itm = new Item(1000, 2000);
            IBox box = panel.GetBox(0);
            panel.Assign(box, itm, HSector.Instance);
            Assert.Equal(0, panel.AvailableBoxes);
            Assert.Equal(1, panel.Utilisation);
        }

        [Fact]
        public void AssignAndUtilisation()
        {
            panel = new Panel(1000, 2000);
            IItem itm = new Item(765, 1648);
            IBox box = panel.GetBox(0);
            panel.Assign(box, itm, VSector.Instance);
            Assert.Equal(2, panel.AvailableBoxes);
            Assert.InRange<double>(panel.Utilisation, 0, 1);
            string utl = panel.Utilisation.ToString("N3");
            output.WriteLine("Utilisation: {0}", utl);
            //output.WriteLine(panel.Assigned.ToString());
        }

        [Fact]
        public void AssignAndMerge()
        {
            panel = new Panel(5, 5);
            IItem i2x2 = new Item(2, 2);
            IItem i2x3 = new Item(2, 3);
            panel.Assign(panel.GetBox(0), i2x2, VSector.Instance);
            IBox box = new Box(0, 0, 0, 0);
            //List<IBox> boxes = new List<IBox>(panel.GetBoxes());
            foreach (IBox b in panel.GetBoxes())
            {
                if (b.CanHold(i2x3)) { box = b; }
                output.WriteLine("Box[{0}] H={1} W={2} X={3} Y={4}", panel.GetBoxes().IndexOf(b), b.Height, b.Width, b.PosX, b.PosY);
            }
            panel.Assign(box, i2x3, HSector.Instance);
            foreach (IBox b in panel.GetBoxes())
            {
                if (b.CanHold(i2x3)) { box = b; }
                output.WriteLine("Box[{0}] H={1} W={2} X={3} Y={4}", panel.GetBoxes().IndexOf(b), b.Height, b.Width, b.PosX, b.PosY);
            }
            Assert.Equal(1, panel.AvailableBoxes);
        }
    }
}
