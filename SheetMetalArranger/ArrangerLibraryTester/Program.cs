using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArrangerLibrary;

namespace ArrangerLibraryTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Region myRegion = new Region(1000, 2000);
            myRegion.items.Add(new Rect(100, 200));
            myRegion.items.Add(new Rect(200, 300));
            myRegion.items.Add(new Rect(50, 1000));
            myRegion.Arrange();
        }
    }
}
