using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary.Abstractions
{
    public interface IArrangement
    {
        double Utilisation { get; }

        List<IPanel> GetPanels();
        IPanel GetPanel(int _index);

        void AddPanel(IPanel _panel);
        List<IItem> GetLeftItems();
        void LeaveItem(IItem _item);
        void AddPanels(List<IPanel> _panels);

        IArrangement NewBranch();

        IPanel GetBestPanel();

        IPanel GetWorstPanel();

        int TotalPanelsArea { get; }
        int TotalItemsArea { get; }
        int ItemsArranged { get; }
    }
}
