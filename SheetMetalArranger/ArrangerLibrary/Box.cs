using System;
using ArrangerLibrary.Abstractions;
using System.Collections.Generic;

namespace ArrangerLibrary
{
    public class Box:IBox
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public int Area
        {
            get { return Width * Height; }
        }

        public int PosX { get; set; }
        public int PosY { get; set; }

        public bool CanHold(IItem _item)
        {
            if ((Width >= _item.Width) && (Height >= _item.Height)) { return true; }
            if ((Width >= _item.Height) && (Height >= _item.Width) && (_item.Rotatable)) { return true; }
            return false;
        }

        public Box(int _posX, int _posY, int _height, int _width)
        {
            Height = _height;
            Width = _width;
            PosX = _posX;
            PosY = _posY;
        }
    }

    public class BoxEquality : IEqualityComparer<IBox>
    {
        private static readonly BoxEquality instance = new BoxEquality();

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
