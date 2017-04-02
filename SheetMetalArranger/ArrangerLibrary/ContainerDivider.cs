using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    public interface IContainerDivider
    {
        List<IContainer> Section(SortCondition _condition);
    }

    public class ContainerDivider : IContainerDivider
    {
        private IContainer container;
        private IRectangle item;
        private IComparer<IContainer> comparer;

        public ContainerDivider(IContainer _container, IRectangle _item)
        {
            container = _container;
            item = _item;
        }
        
        public List<IContainer> Section(SortCondition _condition)
        {
            List<IContainer> vList = SectionVertical();
            List<IContainer> hList = SectionHorizontal();
            if ((vList.Count == 0) && (hList.Count == 0)) return new List<IContainer>();
            if ((vList.Count > 0) && (hList.Count == 0)) return vList;
            if ((vList.Count == 0) && (hList.Count > 0)) return hList;
            comparer = SetComparer(_condition);
            vList.Sort(comparer);
            hList.Sort(comparer);
            vList.Reverse();
            hList.Reverse();
            switch(_condition)
            {
                case SortCondition.Highest:
                    {
                        if (vList[0].Height >= hList[0].Height)
                        { return vList; } else { return hList; }
                    }

                case SortCondition.Widest:
                    {
                        if (hList[0].Width >= vList[0].Width)
                        { return hList; }
                        else { return vList; }
                    }
                default:
                    {
                        if (vList[0].Area >= hList[0].Area)
                        { return vList; }
                        else { return hList; }
                    }
            }
        }

        private List<IContainer> SectionHorizontal()
        {
            uint newX;
            uint newY;
            uint newWidth;
            uint newHeight;
            List<IContainer> result = new List<IContainer>();
            //horizontal section
            //first newly created container
            newX = container.PosX + item.Width;
            newY = container.PosY;
            newWidth = container.Width - item.Width;
            newHeight = item.Height;
            IContainer h1 = new Container(newX,newY,newHeight,newWidth);
            if (h1.Area > 0) { result.Add(h1); }
            //second newly created container
            newX = container.PosX;
            newY = container.PosY + item.Height;
            newWidth = container.Width;
            newHeight = container.Height - item.Height;
            IContainer h2 = new Container(newX, newY, newHeight, newWidth);
            if (h2.Area > 0) { result.Add(h2); }
            return result;
        }

        private List<IContainer> SectionVertical()
        {
            uint newX;
            uint newY;
            uint newWidth;
            uint newHeight;
            List<IContainer> result = new List<IContainer>();
            //vertical section
            //first newly created container
            newX = container.PosX;
            newY = container.PosY + item.Height;
            newWidth = item.Width;
            newHeight = container.Height - item.Height;
            IContainer v1 = new Container(newX, newY, newHeight, newWidth);
            if (v1.Area > 0) { result.Add(v1); }
            //second newly created container
            newX = container.PosX + item.Width;
            newY = container.PosY;
            newWidth = container.Width - item.Width;
            newHeight = container.Height;
            IContainer v2 = new Container(newX, newY, newHeight, newWidth);
            if (v2.Area > 0) { result.Add(v2); }
            return result;
        }

        private IComparer<IContainer> SetComparer(SortCondition _condition)
        {
            switch(_condition)
            {
                case SortCondition.Highest:
                    {
                        return new ContainerHeightComparer();
                    }
                case SortCondition.Widest:
                    {
                        return new ContainerWidthComparer();
                    }
                default:
                    {
                        return new ContainerAreaComparer();
                    }
            }
        }
    }
}
