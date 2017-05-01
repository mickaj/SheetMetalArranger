using Xunit;
using Xunit.Abstractions;
using ArrangerLibrary;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary.Tests
{
    public class CalculationTests
    {
        private readonly ITestOutputHelper output;
        public CalculationTests(ITestOutputHelper _output)
        {
            output = _output;
        }

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
            calc.Calculate(ItemAreaComparer.Instance, ItemWidthComparer.Instance, ItemHeightComparer.Instance, VSector.Instance);
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
            calc.Calculate(ItemAreaComparer.Instance, ItemHeightComparer.Instance, ItemWidthComparer.Instance, HSector.Instance);
            output.WriteLine(calc.OutputBest());
        }
    }
}
