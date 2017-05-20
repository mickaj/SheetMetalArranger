using ArrangerLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    public sealed class PanelUtilisationComparer : IComparer<IPanel>
    {
        private static readonly PanelUtilisationComparer instance = new PanelUtilisationComparer();

        private PanelUtilisationComparer()
        { }

        static PanelUtilisationComparer()
        { }

        public static PanelUtilisationComparer Instance
        {
            get { return instance; }
        }

        public int Compare(IPanel _panel1, IPanel _panel2)
        {
            if (_panel1 == null)
            {
                if (_panel2 == null) { return 0; }
                else { return -1; }
            }
            else
            {
                if (_panel2 == null) { return 1; }
                else { return (_panel1.Utilisation.CompareTo(_panel2.Utilisation)); }
            }
        }
    }
}
