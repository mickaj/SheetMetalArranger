using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArrangerLibrary;
using System.Drawing;
using System.Drawing.Imaging;

namespace ArrangerLibraryTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Arranger newArranger = new Arranger();
            newArranger.DisplayResults();
            ArrangerResultsPNG newResults = new ArrangerResultsPNG(newArranger.GetResultsByIndex(0));
            newResults.SaveToC();
        }
    }
}
