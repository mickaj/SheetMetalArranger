﻿using Xunit;
namespace ArrangerLibrary.Tests
{
    public class BoxTests
    {
        private readonly Box box = new Box(0,0,5,10);
        private Item item;


        [Fact]
        public void ItemFillsBoxNoRotationNoMargin()
        {
            item = new Item(5, 10, 0, false);
            DoAssertionTrue();
        }

        [Fact]
        public void ItemFillsBoxNoRotationWithMargin()
        {
            item = new Item(3, 8, 1, false);
            DoAssertionTrue();
        }

        [Fact]
        public void ItemFillsBoxRotationNoMargin()
        {
            item = new Item(10, 5, 0, true);
            DoAssertionRotated();
        }

        [Fact]
        public void ItemExceedsBoth()
        {
            item = new Item(11, 11, 0, false);
            DoAssertionFalse();
        }

        [Fact]
        public void ItemExceedsWidthNoRotation()
        {
            item = new Item(5, 11, 0, false);
            DoAssertionFalse();
        }

        [Fact]
        public void ItemExceedsHeightNoRotation()
        {
            item = new Item(11, 5, 0, false);
            DoAssertionFalse();
        }

        [Fact]
        public void ItemFitsRotationMargin()
        {
            item = new Item(8, 3, 1, true);
            DoAssertionRotated();
        }

        private void DoAssertionTrue()
        {
            Assert.Equal(1,box.CanHold(item));
        }

        private void DoAssertionFalse()
        {
            Assert.Equal(0, box.CanHold(item));
        }

        private void DoAssertionRotated()
        {
            Assert.Equal(2,box.CanHold(item));
        }

    }
}
