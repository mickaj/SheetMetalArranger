﻿using System.Collections.Generic;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public sealed class BoxHeightComparer : IComparer<IBox>
    {
        private static readonly BoxHeightComparer instance = new BoxHeightComparer();

        private BoxHeightComparer()
        { }

        static BoxHeightComparer()
        { }

        public static BoxHeightComparer Instance
        {
            get { return instance; }
        }

        public int Compare(IBox _item1, IBox _item2)
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

    public sealed class BoxWidthComparer : IComparer<IBox>
    {
        private static readonly BoxWidthComparer instance = new BoxWidthComparer();

        private BoxWidthComparer()
        { }

        static BoxWidthComparer()
        { }

        public static BoxWidthComparer Instance
        {
            get { return instance; }
        }

        public int Compare(IBox _item1, IBox _item2)
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

    public sealed class BoxAreaComparer : IComparer<IBox>
    {
        private static readonly BoxAreaComparer instance = new BoxAreaComparer();

        private BoxAreaComparer()
        { }

        static BoxAreaComparer()
        { }

        public static BoxAreaComparer Instance
        {
            get { return instance; }
        }

        public int Compare(IBox _item1, IBox _item2)
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

    public sealed class BoxEquality : IEqualityComparer<IBox>
    {
        private static readonly BoxEquality instance = new BoxEquality();

        private BoxEquality()
        { }

        static BoxEquality()
        { }

        public static BoxEquality Instance
        {
            get { return instance; }
        }

        public bool Equals(IBox x, IBox y)
        {
            if ((x.Height == y.Height)
                && (x.Width == y.Width)
                && (x.PosX == y.PosX)
                && (x.PosY == y.PosY)) { return true; }
            return false;
        }

        public int GetHashCode(IBox obj)
        {
            return obj.GetHashCode();
        }
    }
}
