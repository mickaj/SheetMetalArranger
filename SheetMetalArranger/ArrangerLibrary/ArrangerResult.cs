using System.Collections.Generic;

namespace ArrangerLibrary
{
    public interface IArrangerResult
    {
        uint SheetHeight { get; }
        uint SheetWidth { get; }
        float UtilisationRatio { get; }
        int AvailableContainersCount { get; }
        int UsedContainersCount { get; }
        int AssignmentCount { get; }

        void AddContainer(IContainer _container);
        void AddContainers(List<IContainer> _containers);
        void MoveToUsed(IContainer _container);
        void Assign(IRectangle _item, IContainer _container);
        IContainer GetAvailableContainerByIndex(int _index);
        List<KeyValuePair<IRectangle, IContainer>> GetAssignments();
        List<IContainer> GetAvailableContainers();
        void ClearAvailableContainers();
    }

    public class ArrangerResult : IArrangerResult
    {
        private class ItemContainerPair
        {
            private IRectangle occupant;
            private IContainer occupied;

            public IRectangle Occupant
            {
                get { return occupant; }
            }

            public IContainer Occupied
            {
                get { return occupied; }
            }

            public ItemContainerPair(IRectangle _item, IContainer _container)
            {
                occupant = _item;
                occupied = _container;
            }
        }

        private List<IContainer> usedContainers;
        private List<IContainer> availableContainers;
        private List<ItemContainerPair> assignments;

        private uint initialHeight;
        private uint initialWidth;

        public uint SheetHeight
        {
            get { return initialHeight; }
        }

        public uint SheetWidth
        {
            get { return initialWidth; }
        }

        public float UtilisationRatio
        {
            get
            {
                uint usedArea = 0;
                foreach (ItemContainerPair i in assignments)
                {
                    usedArea += i.Occupant.Area;
                }
                return (float)usedArea / (initialHeight * initialWidth);
            }
        }

        public int AvailableContainersCount
        {
            get { return availableContainers.Count; }
        }

        public int UsedContainersCount
        {
            get { return usedContainers.Count; }
        }

        public int AssignmentCount
        {
            get { return assignments.Count; }
        }

        public ArrangerResult(uint _height, uint _width)
        {
            assignments = new List<ItemContainerPair>();
            usedContainers = new List<IContainer>();
            availableContainers = new List<IContainer>();
            IContainer initialContainer = new Container(0,0,_height,_width);
            initialHeight = _height;
            initialWidth = _width;
            availableContainers.Add(initialContainer);
        }

        #region Methods allowing access to private lists: usedContainers, available containers, assignments
        public void MoveToUsed(IContainer _container)
        {
            if (availableContainers.Contains(_container))
            {
                usedContainers.Add(_container);
                availableContainers.Remove(_container);
            }
        }

        public void AddContainers(List<IContainer> _containers)
        {
            availableContainers.AddRange(_containers);
        }

        public void AddContainer(IContainer _container)
        {
            availableContainers.Add(_container);
        }

        public void Assign(IRectangle _item, IContainer _container)
        {
            ItemContainerPair assignment = new ItemContainerPair(_item, _container);
            assignments.Add(assignment);
            MoveToUsed(_container);
        }

        public IContainer GetAvailableContainerByIndex(int _index)
        {
            return availableContainers[_index];
        }

        public List<KeyValuePair<IRectangle,IContainer>> GetAssignments()
        {
            List<KeyValuePair<IRectangle, IContainer>> result = new List<KeyValuePair<IRectangle, IContainer>>();
            foreach (ItemContainerPair i in assignments)
            {
                KeyValuePair<IRectangle, IContainer> j = new KeyValuePair<IRectangle, IContainer>(i.Occupant, i.Occupied);
                result.Add(j);
            }
            return result;
        }

        public List<IContainer> GetAvailableContainers()
        {
            return availableContainers;
        }

        public void ClearAvailableContainers()
        {
            availableContainers.Clear();
        }
        #endregion
    }
}
