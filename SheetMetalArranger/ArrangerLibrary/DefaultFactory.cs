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
        private readonly IComparer<IItem> itemHeightComparer = new ItemHeightComparer();
        private readonly IComparer<IItem> itemWidthComparer = new ItemWidthComparer();
        private readonly IComparer<IItem> itemAreaComparer = new ItemAreaComparer();
        private readonly IEqualityComparer<IItem> itemEqualityComparer = new ItemEquality();
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

        public IComparer<IItem> ItemWidthComparer
        {
            get { return itemWidthComparer; }
        }

        public IComparer<IItem> ItemHeightComparer
        {
            get { return itemHeightComparer; }
        }

        public IComparer<IItem> ItemAreaComparer
        {
            get { return itemAreaComparer; }
        }

        public IEqualityComparer<IItem> ItemEqualityComparer
        {
            get { return itemEqualityComparer; }
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

        public IItem NewItem(int _height, int _width, int _margin, bool _rotation)
        {
            return new Item(_height, _width, _margin, _rotation);
        }

        public IItem NewItem(int _height, int _width, int _margin)
        {
            return new Item(_height, _width, _margin);
        }

        public IItem NewItem(int _height, int _width)
        {
            return new Item(_height, _width);
        }

        public IBatch NewBatch()
        {
            return new Batch();
        }

        public IBatch NewBatch(List<IItem> _items)
        {
            return new Batch(_items);
        }

        public ICalculation NewCalculation(IBatch _batch, int _newHeight, int _newWidth)
        {
            return new Calculation(_batch, _newHeight, _newWidth);
        }
        public ICalculation NewCalculation(IBatch _batch, IPanel _defaultPanel)
        {
            return new Calculation(_batch, _defaultPanel);
        }
        public ICalculation NewCalculation(IBatch _batch, List<IPanel> _panels)
        {
            return new Calculation(_batch, _panels);
        }
        public ICalculation NewCalculation(IBatch _batch, List<IPanel> _panels, int _newHeight, int _newWidth)
        {
            return new Calculation(_batch, _panels, _newHeight, _newWidth);
        }
    }
}
