using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    class ArrangerResults
    {
        public List<Container> usedContainers;
        public List<Container> availableContainers;
        public List<ItemContainerPair> assignment;

        public ArrangerResults(int _height, int _width)
        {
            assignment = new List<ItemContainerPair>();
            usedContainers = new List<Container>();
            availableContainers = new List<Container>();
            Container initialContainer = new Container();
            initialContainer.Height = _height;
            initialContainer.Width = _width;
            availableContainers.Add(initialContainer);
        }

        public void Assign(Item _item, Container _container)
        {
            ItemContainerPair newAssignment = new ItemContainerPair(_item, _container);
            assignment.Add(newAssignment);
            usedContainers.Add(_container);
            availableContainers.Remove(_container);
        }
    }
}
