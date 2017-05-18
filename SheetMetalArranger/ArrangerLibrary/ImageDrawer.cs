using ArrangerLibrary.Abstractions;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace ArrangerLibrary
{
    public class ImageDrawer
    {
        public Bitmap Draw(IPanel _panel)
        {
            Bitmap output = new Bitmap(_panel.Width, _panel.Height, PixelFormat.Format32bppRgb);
            using (Graphics graphBuffer = Graphics.FromImage(output))
            {
                graphBuffer.Clear(Color.Gray);
                Pen dwgPen = new Pen(Color.Black, 1);
                Font sizeFont = new Font(FontFamily.GenericMonospace, 10);
                foreach (IAssignment i in _panel.Assignments)
                {
                    int itemH, itemW;
                    if(i.Rotated)
                    {
                        itemH = i.Value.Width;
                        itemW = i.Value.Height;
                    }
                    else
                    {
                        itemH = i.Value.Height;
                        itemW = i.Value.Width;
                    }
                    int X = i.Key.PosX;
                    int Y = _panel.Height - i.Key.PosY - itemH;
                    int h = itemH;
                    int w = itemW;
                    graphBuffer.DrawRectangle(dwgPen, X, Y, w, h);
                    string size = w.ToString() + "x" + h.ToString();
                    graphBuffer.DrawString(size, sizeFont, Brushes.Black, X + 10, Y + 10);
                }
                graphBuffer.Dispose();
            }
            return output;
        }
    }
}
