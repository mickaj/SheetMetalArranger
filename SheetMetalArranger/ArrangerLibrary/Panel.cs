using System;
using System.Collections.Generic;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public class Panel:FactoryBase, IPanel
    {
        private List<IBox> boxes = new List<IBox>();
        private List<IAssignment> assignments = new List<IAssignment>();

        public int Height { get; set; }
        public int Width { get; set; }
        public int Area { get { return Height * Width; } }
        public int AvailableBoxes { get { return boxes.Count; } }
        public int Assigned { get { return assignments.Count; } }
        public List<IAssignment> Assignments { get { return new List<IAssignment>(assignments); } }

        private readonly IMerger merger;

        public double Utilisation
        {
            get
            {
                double itemsInPanel = 0;
                foreach(IAssignment a in assignments)
                {
                    itemsInPanel += a.Value.Area;
                }
                return itemsInPanel/Area;
            }
        }

        public void Assign(IBox _box, IItem _item, ISector _sector, bool _rotated)
        {
            if(boxes.Contains(_box))
            {
                assignments.Add(DefaultFactory.NewAssignment(_box, _item, _rotated));
                boxes.AddRange(_sector.DoSection(_box, _item, _rotated));
                boxes.Remove(_box);
                merge();
            }
            else { throw new InvalidOperationException("Attempted to assing an item to a box which is not present in collection."); }
        }


        public List<IBox> GetBoxes()
        {
            return new List<IBox>(boxes);
        }

        public IBox GetBox(int _index)
        {
            if(_index<boxes.Count) { return boxes[_index]; }
            else { throw new InvalidOperationException("Attempted to access a box which is out of collection range"); }
        }

        public IPanel Copy()
        {
            return new Panel(Height, Width, boxes, assignments, DefaultFactory);
        }

        private void merge()
        {
            List<IBox> tempBoxes = merger.GetMerged(boxes);
            boxes.Clear();
            boxes.AddRange(tempBoxes);
        }

        public Panel(int _height, int _width)
        {
            Height = _height;
            Width = _width;
            merger = DefaultFactory.NewMerger();
            boxes.Add(DefaultFactory.NewBox(0,0,_height, _width));
        }

        public Panel(int _height, int _width, IFactory _factory)
        {
            Height = _height;
            Width = _width;
            DefaultFactory = _factory;
            merger = DefaultFactory.NewMerger();
            boxes.Add(DefaultFactory.NewBox(0, 0, _height, _width));
        }

        private Panel(int _height, int _width, List<IBox> _boxes, List<IAssignment> _assignments, IFactory _factory)
        {
            Height = _height;
            Width = _width;
            DefaultFactory = _factory;
            merger = DefaultFactory.NewMerger();
            boxes = new List<IBox>(_boxes);
            assignments = new List<IAssignment>(_assignments);
        }
    }
}
