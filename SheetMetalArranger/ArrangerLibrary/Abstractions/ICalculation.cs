using System.Collections.Generic;

namespace ArrangerLibrary.Abstractions
{
    public interface ICalculation
    {
        void Calculate(IComparer<IItem> _item1comparer, IComparer<IItem> _item2comparer, IComparer<IItem> _item3comparer, ISector _sector);
        string OutputString();
        string OutputBest();
    }
}
