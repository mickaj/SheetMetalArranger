using System.Collections.Generic;

namespace ArrangerLibrary
{
    public class Item
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Area
        {
            get
            {
                return Height * Width;
            }
        }
        public int LongerEdge
        {
            get
            {
                if (Width >= Height)
                {
                    return Width;
                }
                else
                {
                    return Height;
                }
            }
        }
    }

    public class ItemAreaComparer : IComparer<Item>
    {
        public int Compare(Item _item1, Item _item2)
        {
            if (_item1 == null)
            {
                if (_item2 == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // ...and y is not null, compare the 
                // lengths of the two strings.
                //
                return (_item1.Area.CompareTo(_item2.Area));
            }
        }
    }

    public class ItemHeightComparer : IComparer<Item>
    {
        public int Compare(Item _item1, Item _item2)
        {
            if (_item1 == null)
            {
                if (_item2 == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // ...and y is not null, compare the 
                // lengths of the two strings.
                //
                return (_item1.Height.CompareTo(_item2.Height));
            }
        }
    }

    public class ItemWidthComparer : IComparer<Item>
    {
        public int Compare(Item _item1, Item _item2)
        {
            if (_item1 == null)
            {
                if (_item2 == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // ...and y is not null, compare the 
                // lengths of the two strings.
                //
                return (_item1.Width.CompareTo(_item2.Width));
            }
        }
    }

    public class ItemLongerEdgeComparer : IComparer<Item>
    {
        public int Compare(Item _item1, Item _item2)
        {
            if (_item1 == null)
            {
                if (_item2 == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // ...and y is not null, compare the 
                // lengths of the two strings.
                //
                return (_item1.LongerEdge.CompareTo(_item2.LongerEdge));
            }
        }
    }
}