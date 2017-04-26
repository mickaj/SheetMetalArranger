using System;
using System.Collections.Generic;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public class Panel:IPanel
    {
        private List<IBox> boxes = new List<IBox>();
        private List<KeyValuePair<IBox, IItem>> assignments = new List<KeyValuePair<IBox, IItem>>();

        public int Height { get; set; }
        public int Width { get; set; }
        public int Area { get { return Height * Width; } }
        public int AvailableBoxes { get { return boxes.Count; } }
        public int Assigned { get { return assignments.Count; } }

        private readonly Merger merger = new Merger();

        public double Utilisation
        {
            get
            {
                double itemsInPanel = 0;
                foreach(KeyValuePair<IBox, IItem> a in assignments)
                {
                    itemsInPanel += a.Value.Area;
                }
                return itemsInPanel/Area;
            }
        }

        public void Assign(IBox _box, IItem _item, ISector _sector)
        {
            if(boxes.Contains(_box))
            {
                KeyValuePair<IBox, IItem> a = new KeyValuePair<IBox, IItem>(_box, _item);
                assignments.Add(a);
                boxes.AddRange(_sector.DoSection(_box, _item));
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
            if(_index<boxes.Count)
            {
                return boxes[_index];
            }
            else { throw new InvalidOperationException("Attempted to access a box which is out of collection range"); }
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
            boxes.Add(new Box(0, 0, _height, _width));
        }

        private class Merger
        {
            private List<IBox> input = new List<IBox>();
            private List<IBox> output = new List<IBox>();

            public List<IBox> GetMerged(List<IBox> _list)
            {
                input.Clear();
                input.AddRange(_list);
                output.Clear();
                bool extended;
                do
                {
                    extended = false;
                    foreach (IBox cnt in _list)
                    {
                        if (extend(cnt))
                        {
                            extended = true;
                        };
                    }
                } while (extended);
                output.AddRange(input);
                return output;
            }

            private bool extend(IBox _container)
            {
                bool extended = false;
                List<IBox> buffer = new List<IBox>();
                buffer.AddRange(input);
                buffer.Remove(_container);
                int i = 0;
                while ((i < buffer.Count) && (extended == false))
                {
                    if (adjacentHorizontal(_container, buffer[i]))
                    {
                        output.Add(mergeHorizontal(_container, buffer[i]));
                        input.Remove(_container);
                        input.Remove(buffer[i]);
                        extended = true;
                    }

                    if (adjacentVertical(_container, buffer[i]))
                    {
                        output.Add(mergeVertical(_container, buffer[i]));
                        input.Remove(_container);
                        input.Remove(buffer[i]);
                        extended = true;
                    }
                    i++;
                }
                return extended;
            }

            private bool adjacentHorizontal(IBox _ext1, IBox _ext2)
            {
                if ((_ext1.Height == _ext2.Height) && (_ext2.PosY == _ext1.PosY) && (_ext2.PosX == _ext1.PosX + _ext1.Width))
                { return true; }
                return false;
            }

            private bool adjacentVertical(IBox _ext1, IBox _ext2)
            {
                if ((_ext1.Width == _ext2.Width) && (_ext2.PosX == _ext1.PosX) && (_ext2.PosY == _ext1.PosY + _ext1.Height))
                { return true; }
                return false;
            }

            private IBox mergeHorizontal(IBox _mrg1, IBox _mrg2)
            {
                return new Box(_mrg1.PosX, _mrg1.PosY, _mrg1.Height, _mrg1.Width + _mrg2.Width);
            }

            private IBox mergeVertical(IBox _mrg1, IBox _mrg2)
            {
                return new Box(_mrg1.PosX, _mrg1.PosY, _mrg1.Height + _mrg2.Height, _mrg1.Width);
            }
        }
    }
}
