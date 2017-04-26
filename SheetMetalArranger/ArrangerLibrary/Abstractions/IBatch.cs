using System.Collections.Generic;

namespace ArrangerLibrary.Abstractions
{
    public interface IBatch
    {
        int Remaining { get; }
        void AddItem(IItem _item);
        bool RemoveItem(IItem _item);
        void AddItems(List<IItem> _items);
        void Clear();
        IItem GetFirst(IComparer<IItem> _comparer1, IComparer<IItem> _comparer2, IComparer<IItem> _comparer3);
        List<IItem> Content();
    }
}
