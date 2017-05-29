using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public class Item : IItem
    {
        public int ItemHeight { get; set; }
        public int ItemWidth { get; set; }
        public int Margin { get; set; }
        public bool Rotatable { get; set; }

        public int Height
        {
            get { return ItemHeight + 2 * Margin; }
        }

        public int Width
        {
            get { return ItemWidth + 2 * Margin; }
        }

        public int Area
        {
            get { return Height * Width; }
        }

        public Item(int _height, int _width, int _margin, bool _rotation)
        {
            ItemHeight = _height;
            ItemWidth = _width;
            Margin = _margin;
            Rotatable = _rotation;
        }

        public Item(int _height, int _width, int _margin)
        {
            ItemHeight = _height;
            ItemWidth = _width;
            Margin = _margin;
            Rotatable = false;
        }

        public Item(int _height, int _width)
        {
            ItemHeight = _height;
            ItemWidth = _width;
            Margin = 0;
            Rotatable = false;
        }

        public IItem CreateCopy()
        {
            IItem copy = new Item(ItemHeight, ItemWidth, Margin, Rotatable);
            return copy;
        }
    }
}
