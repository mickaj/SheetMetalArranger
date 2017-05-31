using ArrangerLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    public class DefaultFactory : IFactory
    {
        private readonly ISector hSector = new HSector();
        private readonly ISector vSector = new VSector();
        private readonly IComparer<IPanel> panelComparer = new PanelUtilisationComparer();
        private readonly IComparer<IBox> boxHeightComparer = new BoxHeightComparer();
        private readonly IComparer<IBox> boxWidthComparer = new BoxWidthComparer();
        private readonly IComparer<IBox> boxAreaComparer = new BoxAreaComparer();
        private readonly IEqualityComparer<IBox> boxEqualityComparer = new BoxEquality();
        private readonly IComparer<IArrangement> arrangementComparer = new ArrangementRatioComparer();

        public IMerger NewMerger()
        {
            return new Merger();
        }

        public IMerger NewMerger(IFactory _factory)
        {
            return new Merger(_factory);
        }

        public IAssignment NewAssignment(IBox _box, IItem _item, bool _rotated)
        {
            return new Assignment(_box, _item, _rotated);
        }

        public ISector HSector
        {
            get { return hSector; }
        }

        public ISector VSector
        {
            get { return vSector; }
        }

        public IComparer<IPanel> PanelComparer
        {
            get { return panelComparer; }
        }

        public IComparer<IBox> BoxWidthComparer
        {
            get { return boxWidthComparer; }
        }

        public IComparer<IBox> BoxHeightComparer
        {
            get { return boxHeightComparer; }
        }

        public IComparer<IBox> BoxAreaComparer
        {
            get { return boxAreaComparer; }
        }

        public IEqualityComparer<IBox> BoxEqualityComparer
        {
            get { return boxEqualityComparer; }
        }

        public IComparer<IArrangement> ArrangementRatioComparer
        {
            get { return arrangementComparer; }
        }

        public IBox NewBox(int _posX, int _posY, int _h, int _w)
        {
            return new Box(_posX, _posY, _h, _w);
        }

        public IArrangement NewArrangement()
        {
            return new Arrangement();
        }

        public IArrangement NewArrangement(IFactory _factory)
        {
            return new Arrangement(_factory);
        }

        public IPanel NewPanel(int _h, int _w)
        {
            return new Panel(_h, _w);
        }

        public IPanel NewPanel(int _h, int _w, IFactory _factory)
        {
            return new Panel(_h, _w, _factory);
        }
    }
}
