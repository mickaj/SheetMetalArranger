using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    public interface IContainer
    {
        uint PosX { get; set; }
        uint PosY { get; set; }
        uint Width { get; set; }
        uint Height { get; set; }
        uint Area { get; }

        bool CheckIfFits(IRectangle _rectangle);
    }

    public class Container : IContainer
    {
        private uint posX;
        public uint PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        private uint posY;
        public uint PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        private uint width;
        public uint Width
        {
            get { return width; }
            set { width = value; }
        }

        private uint height;
        public uint Height
        {
            get { return posY; }
            set { posY = value; }
        }

        public uint Area
        {
            get { return height * width; }
        }

        public Container(uint _x, uint _y, uint _height, uint _width)
        {
            posX = _x;
            posY = _y;
            height = _height;
            width = _width;
        }

        public bool CheckIfFits(IRectangle _rectangle)
        {
            if((width>=_rectangle.Width)&&(height>=_rectangle.Height))
            { return true; } else { return false; }
        }
    }
}
