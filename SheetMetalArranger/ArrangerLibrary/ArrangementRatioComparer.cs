using System.Collections.Generic;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public sealed class ArrangementRatioComparer : IComparer<IArrangement>
    {
        private static readonly ArrangementRatioComparer instance = new ArrangementRatioComparer();

        private ArrangementRatioComparer()
        { }

        static ArrangementRatioComparer()
        { }

        public static ArrangementRatioComparer Instance
        {
            get { return instance; }
        }

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
