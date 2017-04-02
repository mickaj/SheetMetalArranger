using System;
using System.Collections.Generic;

namespace ArrangerLibrary
{
    
    public enum SortCondition
    {
        Highest,
        Widest,
        Area
    }

    #region container comparers
    public class ContainerEquality : IEqualityComparer<IContainer>
    {
        public bool Equals(IContainer x, IContainer y)
        {
            if ((x.Height == y.Height)
                && (x.Width == y.Width)
                && (x.PosX == y.PosX)
                && (x.PosY == y.PosY)) { return true; }
            return false;
        }

        public int GetHashCode(IContainer obj)
        {
            throw new NotImplementedException();
        }
    }

    public class ContainerAreaComparer : IComparer<IContainer>
    {
        public int Compare(IContainer _cnt1, IContainer _cnt2)
        {
            if (_cnt1 == null)
            {
                if (_cnt2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return (_cnt1.Area.CompareTo(_cnt2.Area));
            }
        }
    }

    public class ContainerHeightComparer : IComparer<IContainer>
    {
        public int Compare(IContainer _cnt1, IContainer _cnt2)
        {
            if (_cnt1 == null)
            {
                if (_cnt2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return (_cnt1.Height.CompareTo(_cnt2.Height));
            }
        }
    }

    public class ContainerWidthComparer : IComparer<IContainer>
    {
        public int Compare(IContainer _cnt1, IContainer _cnt2)
        {
            if (_cnt1 == null)
            {
                if (_cnt2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return (_cnt1.Width.CompareTo(_cnt2.Width));
            }
        }
    }
    #endregion

    #region item comparers
    public class ItemAreaComparer : IComparer<IRectangle>
    {
        public int Compare(IRectangle _rct1, IRectangle _rct2)
        {
            if (_rct1 == null)
            {
                if (_rct2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return (_rct1.Area.CompareTo(_rct2.Area));
            }
        }
    }

    public class ItemHeightComparer : IComparer<IRectangle>
    {
        public int Compare(IRectangle _rct1, IRectangle _rct2)
        {
            if (_rct1 == null)
            {
                if (_rct2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return (_rct1.Height.CompareTo(_rct2.Height));
            }
        }
    }

    public class ItemWidthComparer : IComparer<IRectangle>
    {
        public int Compare(IRectangle _rct1, IRectangle _rct2)
        {
            if (_rct1 == null)
            {
                if (_rct2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return (_rct1.Width.CompareTo(_rct2.Width));
            }
        }
    }
    #endregion
}
