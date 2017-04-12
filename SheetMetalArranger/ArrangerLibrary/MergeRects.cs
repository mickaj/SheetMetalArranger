using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    public interface IMergeRects
    {
        List<IContainer> GetMerged(List<IContainer> _list);
        string GetResultString();
    }
    public sealed class Merger : IMergeRects
    {
        private static readonly Lazy<Merger> maxRects = new Lazy<Merger>(() => new Merger());
        
        public static Merger Instance
        {
            get { return maxRects.Value; }
        }

        private List<IContainer> input = new List<IContainer>();
        private List<IContainer> output = new List<IContainer>();

        public List<IContainer> GetMerged(List<IContainer> _list)
        {
            input.Clear();
            input.AddRange(_list);
            output.Clear();
            bool extended;
            do
            {
                extended = false;
                foreach (IContainer cnt in _list)
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

        private bool extend(IContainer _container)
        {
            bool extended = false;
            List<IContainer> buffer = new List<IContainer>();
            buffer.AddRange(input);
            buffer.Remove(_container);
            int i = 0;
            while ((i<buffer.Count)&&(extended==false))
            {
                if(adjacentHorizontal(_container, buffer[i]))
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

        private bool adjacentHorizontal(IContainer _ext1, IContainer _ext2)
        {
            if((_ext1.Height == _ext2.Height)&&(_ext2.PosY==_ext1.PosY)&&(_ext2.PosX==_ext1.PosX+_ext1.Width))
            { return true; }
            return false;
        }

        private bool adjacentVertical(IContainer _ext1, IContainer _ext2)
        {
            if ((_ext1.Width == _ext2.Width) && (_ext2.PosX == _ext1.PosX) && (_ext2.PosY == _ext1.PosY + _ext1.Height))
            { return true; }
            return false;
        }

        private IContainer mergeHorizontal(IContainer _mrg1, IContainer _mrg2)
        {
            return new Container(_mrg1.PosX, _mrg1.PosY, _mrg1.Height, _mrg1.Width + _mrg2.Width);
        }

        private IContainer mergeVertical(IContainer _mrg1, IContainer _mrg2)
        {
            return new Container(_mrg1.PosX, _mrg1.PosY, _mrg1.Height+_mrg2.Height, _mrg1.Width);
        }

        public string GetResultString()
        {
            string resultTxt = "Merging results:\n";
            foreach (IContainer cnt in output)
            {
                resultTxt += "Container: X=" + cnt.PosX + " Y=" + cnt.PosY + " H=" + cnt.Height + " W=" + cnt.Width+"\n";
            }
            return resultTxt;
        }
    }
}
