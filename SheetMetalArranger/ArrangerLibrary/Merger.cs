using System.Collections.Generic;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public class Merger : FactoryBase, IMerger
    {
        private List<IBox> input = new List<IBox>();
        private List<IBox> output = new List<IBox>();

        public Merger()
        { }

        public Merger(IFactory _factory)
        {
            DefaultFactory = _factory;
        }

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
            return DefaultFactory.NewBox(_mrg1.PosX, _mrg1.PosY, _mrg1.Height, _mrg1.Width + _mrg2.Width);
        }

        private IBox mergeVertical(IBox _mrg1, IBox _mrg2)
        {
            return DefaultFactory.NewBox(_mrg1.PosX, _mrg1.PosY, _mrg1.Height + _mrg2.Height, _mrg1.Width);
        }
    }
}
