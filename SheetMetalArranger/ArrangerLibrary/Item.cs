using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    public interface IRectangle
    {
        uint Width { get; set; }
        uint Height { get; set; }
        uint Area { get; }
        uint LongerEdge { get; }
    }

    public class Item : IRectangle
    {
        private uint width;
        public uint Width
        {
            get { return width; }
            set { width = value; }
        }

        private uint height;
        public uint Height
        {
            get { return height; }
            set { height = value; }
        }

        public uint Area
        {
            get { return height * width; }
        }

        public uint LongerEdge
        {
            get
            {
                if(width>height) { return width; }
                else { return height; }
            }
        }

        public Item(uint _height, uint _width)
        {
            width = _width;
            height = _height;
        }
    }
}
