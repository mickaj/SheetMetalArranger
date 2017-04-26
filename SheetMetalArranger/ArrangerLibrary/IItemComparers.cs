using System;
using System.Collections.Generic;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public sealed class ItemHeightComparer : IComparer<IItem>
    {
        private static readonly ItemHeightComparer instance = new ItemHeightComparer();

        public static ItemHeightComparer Instance
        {
            get { return instance; }
        }

        public int Compare(IItem _item1, IItem _item2)
        {
            if (_item1 == null)
            {
                if (_item2 == null) { return 0; }
                else { return -1; }
            }
            else
            {
                if (_item2 == null) { return 1; }
                else { return (_item1.Height.CompareTo(_item2.Height)); }
            }
        }
    }

    public sealed class ItemWidthComparer : IComparer<IItem>
    {
        private static readonly ItemWidthComparer instance = new ItemWidthComparer();

        public static ItemWidthComparer Instance
        {
            get { return instance; }
        }

        public int Compare(IItem _item1, IItem _item2)
        {
            if (_item1 == null)
            {
                if (_item2 == null) { return 0; }
                else { return -1; }
            }
            else
            {
                if (_item2 == null) { return 1; }
                else { return (_item1.Width.CompareTo(_item2.Width)); }
            }
        }
    }

    public sealed class ItemAreaComparer : IComparer<IItem>
    {
        private static readonly ItemAreaComparer instance = new ItemAreaComparer();

        public static ItemAreaComparer Instance
        {
            get { return instance; }
        }

        public int Compare(IItem _item1, IItem _item2)
        {
            if (_item1 == null)
            {
                if (_item2 == null) { return 0; }
                else { return -1; }
            }
            else
            {
                if (_item2 == null) { return 1; }
                else { return (_item1.Area.CompareTo(_item2.Area)); }
            }
        }
    }

    public sealed class ItemEquality : IEqualityComparer<IItem>
    {
        private static readonly ItemEquality instance = new ItemEquality();

        public static ItemEquality Instance
        {
            get
            {
                return instance;
            }
        }

        public bool Equals(IItem x, IItem y)
        {
            if ((x.Height == y.Height)
                && (x.Width == y.Width)
                && (x.Rotatable == y.Rotatable)) { return true; }
            return false;
        }

        public int GetHashCode(IItem obj)
        {
            throw new NotImplementedException();
        }

    }
}
