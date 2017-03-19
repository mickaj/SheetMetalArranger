using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace ArrangerLibrary
{
    public class ArrangerResults
    {
        public List<Container> usedContainers;
        public List<Container> availableContainers;
        public List<ItemContainerPair> assignment;
        private int initialHeight;
        private int initialWidth;
        public int SheetHeight
        {
            get
            {
                return initialHeight;
            }
        }
        public int SheetWidth
        {
            get
            {
                return initialWidth;
            }
        }
        public float UtilisationRatio
        {
            get
            {
                int usedArea=0;
                foreach (ItemContainerPair i in assignment)
                {
                    usedArea += i.Occupant.Area;
                }
                return (float)usedArea / (initialHeight * initialWidth); 
            }
        }

        public ArrangerResults(int _height, int _width)
        {
            assignment = new List<ItemContainerPair>();
            usedContainers = new List<Container>();
            availableContainers = new List<Container>();
            Container initialContainer = new Container();
            initialContainer.Height = _height;
            initialContainer.Width = _width;
            initialHeight = _height;
            initialWidth = _width;
            availableContainers.Add(initialContainer);
        }

        public void Assign(Item _item, Container _container) 
        {
            ItemContainerPair newAssignment = new ItemContainerPair(_item, _container);
            assignment.Add(newAssignment);
            usedContainers.Add(_container);
            availableContainers.Remove(_container);
        }
    }

    public class ArrangerResultsPNG
    {
        private ArrangerResults input;
        private Bitmap dwg;

        public ArrangerResultsPNG(ArrangerResults _input)
        {
            input = _input;
            dwg = new Bitmap(input.SheetWidth, input.SheetHeight,PixelFormat.Format32bppRgb);
            Draw();
        }

        private void Draw()
        {
            using (Graphics graphBuffer = Graphics.FromImage(dwg))
            {
                graphBuffer.Clear(Color.White);
                Pen dwgPen = new Pen(Color.Black, 1);
                Font sizeFont = new Font(FontFamily.GenericMonospace, 10);
                foreach (ItemContainerPair i in input.assignment)
                {
                    int X = i.Occupied.X;
                    int Y = input.SheetHeight - i.Occupied.Y - i.Occupant.Height;
                    int h = i.Occupant.Height;
                    int w = i.Occupant.Width;
                    graphBuffer.DrawRectangle(dwgPen, X, Y, w, h);
                    string size = w.ToString() + "x" + h.ToString();
                    graphBuffer.DrawString(size, sizeFont, Brushes.Black, X + 10, Y + 10);
                }
                graphBuffer.Dispose();
            }
        }

        public void SaveToC()
        {
            dwg.Save(@"D:\output\image.png", ImageFormat.Png);
        }
    }
}
