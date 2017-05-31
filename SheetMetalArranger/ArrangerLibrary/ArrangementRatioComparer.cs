using System.Collections.Generic;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public sealed class ArrangementRatioComparer : IComparer<IArrangement>
    {
        public int Compare(IArrangement _arr1, IArrangement _arr2)
        {
            if (_arr1 == null)
            {
                if (_arr2 == null) { return 0; }
                else { return -1; }
            }
            else
            {
                if (_arr2 == null) { return 1; }
                else { return (_arr1.Utilisation.CompareTo(_arr2.Utilisation)); }
            }
        }
    }
}
