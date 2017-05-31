using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary.Abstractions
{
    public interface IFactory
    {
        IMerger NewMerger();
        IMerger NewMerger(IFactory _factory);

        IAssignment NewAssignment(IBox _box, IItem _item, bool _rotated);
        IBox NewBox(int _posX, int _posY, int _h, int _w);

        IArrangement NewArrangement();
        IArrangement NewArrangement(IFactory _factory);

        IPanel NewPanel(int _h, int _w);
        IPanel NewPanel(int _h, int _w, IFactory _factory);


        ISector HSector { get; }
        ISector VSector { get; }

        IComparer<IPanel> PanelComparer { get; }

        IComparer<IBox> BoxHeightComparer { get; }
        IComparer<IBox> BoxWidthComparer { get; }
        IComparer<IBox> BoxAreaComparer { get; }
        IEqualityComparer<IBox> BoxEqualityComparer { get; }

        IComparer<IArrangement> ArrangementRatioComparer { get; }


    }
}
