using System;
using System.Collections.Generic;

namespace ArrangerLibrary
{
    public class Rect
    {
        public int width;
        public int height;
        public int area
        {
            get
            {
                return width * height;
            }
        }

        public Rect(int h, int w)
        {
            width = w;
            height = h;
        }
    }

    public class Region
    {
        public List<Rect> items;
        public List<Rect> sheet;

        public Region(int height, int width)
        {
            items = new List<Rect>();
            sheet = new List<Rect>();
            sheet.Add(new Rect(height, width));
        }


        public void Arrange()
        {
            //find longest edge
            Comparison<Rect> SortByLongestEdge = new Comparison<Rect>((Rect x, Rect y) => x.height.CompareTo(y.height));
            items.Sort(SortByLongestEdge);
            items.Reverse();
            int maxHeight = items[0].height;
            SortByLongestEdge = new Comparison<Rect>((Rect x, Rect y) => x.width.CompareTo(y.width));
            items.Sort(SortByLongestEdge);
            items.Reverse();
            int maxWidth = items[0].width;
            Console.WriteLine("Max height: {0}", maxHeight);
            Console.WriteLine("Max width: {0}", maxWidth);
        }
    }

    public struct Coords
    {
        int x;
        int y;
    }

    public class Assignment
    {
        Rect item;
        Coords position; //bottom left corner
    }

}
