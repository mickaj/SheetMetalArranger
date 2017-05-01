using ArrangerLibrary.Abstractions;

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

        public int CanHold(IItem _item)
        {
            if ((Width >= _item.Width) && (Height >= _item.Height)) { return 1; } //box can hold given item without rotation
            if ((Width >= _item.Height) && (Height >= _item.Width) && (_item.Rotatable)) { return 2; } //box can hold given item if rotated
            return 0; //box cannot hold given item
        }

        public Box(int _posX, int _posY, int _height, int _width)
        {
            Height = _height;
            Width = _width;
            PosX = _posX;
            PosY = _posY;
        }
    }
}
