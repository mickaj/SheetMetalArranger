using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using ArrangerLibrary;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary.Tests
{
    public class CalculationTests : FactoryBase
    {
        private readonly ITestOutputHelper output;
        public CalculationTests(ITestOutputHelper _output)
        {
            output = _output;
        }

        private Action<int> DoNothing = delegate(int i) { /*do nothing*/ };

        [Fact]
        public void Test1()
        {
            IBatch input = new Batch();
            input.AddItem(new Item(3, 5));
            input.AddItem(new Item(5, 3));
            input.AddItem(new Item(5, 2));
            input.AddItem(new Item(3, 3));
            input.AddItem(new Item(4, 3));
            input.AddItem(new Item(1,5));
            input.AddItem(new Item(1, 5));
            for (int i = 0; i < 6; i++)
            {
                input.AddItem(new Item(2, 3));
            }
            input.AddItem(new Item(8, 14));
            ICalculation calc = new Calculation(input, 8, 14);
            calc.Calculate(DefaultFactory.ItemAreaComparer,DefaultFactory.ItemWidthComparer, DefaultFactory.ItemHeightComparer, DefaultFactory.HSector, DoNothing);
            output.WriteLine(calc.OutputBest());
        }

        [Fact]
        public void Test2()
        {
            int margin = 10;
            IBatch input = new Batch();
            input.AddItem(new Item(1000, 2000, margin, true));
            input.AddItem(new Item(100, 300, margin, false));
            input.AddItem(new Item(400, 600, margin, true));
            input.AddItem(new Item(600, 200, margin, true));
            input.AddItem(new Item(250, 400, margin, true));
            input.AddItem(new Item(230, 100, margin, true));
            input.AddItem(new Item(600, 1000, margin, true));
            for (int i = 0; i < 10; i++)
            {
                input.AddItem(new Item(100, 100, margin, true));
            }
            ICalculation calc = new Calculation(input, 1500, 3000);
            calc.Calculate(DefaultFactory.ItemAreaComparer, DefaultFactory.ItemHeightComparer, DefaultFactory.ItemWidthComparer, DefaultFactory.HSector, DoNothing);
            output.WriteLine(calc.OutputBest());
        }

        //here are some basics about random test:
        /* panel size will be constant: 1500x3000 - this is standard sheet plate size, most commonly used (at least in my experience)
         * the test will limit number of panles to 10 - this is to avoid having last panel filled only in quarter or half; the test is to check how densly will items be packed
         * the test will generate items as follows:
         * 10 pcs of 50~60% of panel area - that equals items from 750x1500 to 900x1800
         * 15 pcs of 30~40% of panel area - 450x900 to 600x1200 
         * 20 pcs of 20~30% of panel area - 300x600 to 449x899
         * 50 pcs of 5~20% of panel area - 75x150 to 299x599
         * wheater an item is rotatable will also be decided randomly
         * margin will be constant: 10 units
         */
        [Fact]
        public void RandomTester()
        {
            IBatch batch = GetRandomBatch(1500, 3000, 10, 15, 20, 30, 50);
            List<IPanel> panels = new List<IPanel>();
            for(int i = 1; i <=10; i++)
            {
                panels.Add(new Panel(1500, 3000));
            }
            ICalculation calc = new Calculation(batch, panels);
            calc.Calculate(DefaultFactory.ItemAreaComparer, DefaultFactory.ItemHeightComparer, DefaultFactory.ItemWidthComparer, DefaultFactory.HSector, DoNothing);
            output.WriteLine(calc.OutputBest());
        }

        //here are some basics about random test:
        /* panel size will be constant: 1500x3000 - this is standard sheet plate size, most commonly used (at least in my experience)
         * the test will NOT limit number of panles
         * the test will generate items as follows:
         * 10 pcs of 50~60% of panel area - that equals items from 750x1500 to 900x1800
         * 15 pcs of 30~40% of panel area - 450x900 to 600x1200 
         * 20 pcs of 20~30% of panel area - 300x600 to 449x899
         * 50 pcs of 5~20% of panel area - 75x150 to 299x599
         * wheater an item is rotatable will also be decided randomly
         * margin will be constant: 10 units
         */
        [Fact]
        public void RandomTesterUnlimited()
        {
            IBatch batch = GetRandomBatch(1500, 3000, 10, 15, 20, 30, 50);
            ICalculation calc = new Calculation(batch, 1500,3000);
            calc.Calculate(DefaultFactory.ItemAreaComparer, DefaultFactory.ItemHeightComparer, DefaultFactory.ItemWidthComparer, DefaultFactory.HSector, DoNothing);
            output.WriteLine(calc.OutputBest());
        }

        [Fact]
        public void RandomTesterWithDefinedPanelsAndNewPanelsAllowed()
        {
            IBatch batch = GetRandomBatch(1500, 3000, 10, 15, 20, 30, 50);
            List<IPanel> panels = new List<IPanel>();
            for (int i = 1; i <= 10; i++)
            {
                panels.Add(new Panel(1500, 3000));
            }
            ICalculation calc = new Calculation(batch, panels, 1000,2000);
            calc.Calculate(DefaultFactory.ItemAreaComparer, DefaultFactory.ItemHeightComparer, DefaultFactory.ItemWidthComparer, DefaultFactory.HSector, DoNothing);
            output.WriteLine(calc.OutputBest());
        }

        private IBatch GetRandomBatch(int _h, int _w, int _m, int _r1, int _r2, int _r3, int _r4)
        {
            Random randGen = new Random(DateTime.Now.GetHashCode());
            Random randBool = new Random(DateTime.Now.GetHashCode());
            IBatch batch = new Batch();
            RandomRange r1 = new RandomRange(Convert.ToInt32(_h*0.6), Convert.ToInt32(_w * 0.6), Convert.ToInt32(_h * 0.5), Convert.ToInt32(_w * 0.5), _r1);
            RandomRange r2 = new RandomRange(Convert.ToInt32(_h * 0.4), Convert.ToInt32(_w * 0.4), Convert.ToInt32(_h * 0.3), Convert.ToInt32(_w * 0.3), _r2);
            RandomRange r3 = new RandomRange(Convert.ToInt32(_h * 0.3), Convert.ToInt32(_w * 0.3), Convert.ToInt32(_h * 0.2), Convert.ToInt32(_w * 0.2), _r3);
            RandomRange r4 = new RandomRange(Convert.ToInt32(_h * 0.2), Convert.ToInt32(_w * 0.2), Convert.ToInt32(_h * 0.05), Convert.ToInt32(_w * 0.05), _r4);
            RandomRange[] ranges = { r1, r2, r3, r4 };
            foreach(RandomRange r in ranges)
            {
                for(int i = 1; i<=r.QTY; i++)
                {
                    int h = randGen.Next(r.MinH, r.MaxH);
                    int w = randGen.Next(r.MinW, r.MaxW);
                    bool rot = false;
                    if (randBool.Next(1, 1000) > 500) { rot = true; }
                    batch.AddItem(new Item(h, w, _m, rot));
                }
            }
            return batch;
        }

        private struct RandomRange
        {
            public int MaxH;
            public int MaxW;
            public int MinH;
            public int MinW;
            public int QTY;

            public RandomRange(int _maxH, int _maxW, int _minH, int _minW, int _qty)
            {
                MaxH = _maxH;
                MaxW = _maxW;
                MinH = _minH;
                MinW = _minW;
                QTY = _qty;
            }
        }
    }
}
