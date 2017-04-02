using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    public class ItemNester
    {
        private class PossibleFit
        {
            public IContainer MainContainer { get; set; }
            public uint ResultsIndex { get; set; }
            public float AreaRatio { get; set; }
        }

        private List<IRectangle> items;
        private List<IRectangle> placedItems;
        private List<IRectangle> leftItems;
        private uint sheetWidth;
        private uint sheetHeight;

        private Dictionary<uint, IArrangerResult> results = new Dictionary<uint, IArrangerResult>();

        private uint sheetIndex = 0;
        private IComparer<IRectangle> comparer;

        public int LeftCount
        {
            get { return leftItems.Count; }
        }

        public ItemNester(uint _height, uint _width)
        {
            items = new List<IRectangle>();
            placedItems = new List<IRectangle>();
            leftItems = new List<IRectangle>();
            sheetHeight = _height;
            sheetWidth = _width;
        }

        public ItemNester(List<IRectangle> _items, uint _height, uint _width)
        {
            items = new List<IRectangle>();
            placedItems = new List<IRectangle>();
            leftItems = new List<IRectangle>();
            items.AddRange(_items);
            sheetHeight = _height;
            sheetWidth = _width;
        }

        public Dictionary<uint, IArrangerResult> Calculate(SortCondition _condition)
        {
            //initialize first set of results
            Dictionary<uint, IArrangerResult> calculationResults = new Dictionary<uint, IArrangerResult>();
            ArrangerResult initialResults = new ArrangerResult(sheetHeight, sheetWidth);
            calculationResults.Add(sheetIndex, initialResults);
            //initialize comparer
            comparer = SetComparer(_condition);
            do
            {
                //sort items with selected comparer and revers order
                items.Sort(comparer);
                items.Reverse();
                IRectangle currentItem = items[0];
                IRectangle nextItem;
                if (items.Count > 1)
                {
                    nextItem = items[1];
                }
                else
                {
                    nextItem = null;
                }
                //take first items from items list and try to find a container that can hold it
                List<PossibleFit> possibleFits = new List<PossibleFit>();
                foreach (KeyValuePair<uint, IArrangerResult> result in calculationResults)
                {
                    //cycle through all available containers
                    for (int i=0; i<result.Value.AvailableContainersCount; i++)
                    {
                        IContainer thisContainer= result.Value.GetAvailableContainerByIndex(i);
                        if (thisContainer.CheckIfFits(currentItem))
                        {
                            PossibleFit fit = new PossibleFit();
                            fit.MainContainer = thisContainer;
                            fit.ResultsIndex = result.Key;
                            fit.AreaRatio = (float)currentItem.Area / thisContainer.Area;
                            possibleFits.Add(fit);
                        }
                    }
                }
                if (possibleFits.Count > 0) //if there is a container in all available containers that can hold the item
                {
                    //sort possible fits by area ratio
                    possibleFits.Sort((PossibleFit x, PossibleFit y) => x.AreaRatio.CompareTo(y.AreaRatio));
                    possibleFits.Reverse();
                    //take best fit and do the assignment
                    calculationResults[possibleFits[0].ResultsIndex].Assign(currentItem, possibleFits[0].MainContainer);
                    //perform section of used container
                    IContainerDivider currentSection = new ContainerDivider(possibleFits[0].MainContainer, currentItem);
                    List<IContainer> newContainers = currentSection.Section(_condition);
                    calculationResults[possibleFits[0].ResultsIndex].AddContainers(newContainers);
                    placedItems.Add(currentItem);
                }
                else //if none of all available containers can hold the item
                {
                    if ((currentItem.Width <= sheetWidth) && (currentItem.Height <= sheetHeight))
                    {
                        //create new result set - in practice in means that new sheet must be used
                        IArrangerResult newResults = new ArrangerResult(sheetHeight, sheetWidth);
                        calculationResults.Add(++sheetIndex, newResults);
                        //place item in newly created result set
                        IContainer usedContainer = calculationResults[sheetIndex].GetAvailableContainerByIndex(0);
                        calculationResults[sheetIndex].Assign(currentItem, usedContainer);
                        //perform section of used container
                        IContainerDivider currentSection = new ContainerDivider(possibleFits[0].MainContainer, currentItem);
                        List<IContainer> newContainers = currentSection.Section(_condition);
                        calculationResults[possibleFits[0].ResultsIndex].AddContainers(newContainers);
                        placedItems.Add(currentItem);
                    }
                    else
                    {
                        leftItems.Add(currentItem);
                    }
                }
                //at the end remove currently processed item from list of items
                items.Remove(currentItem);
            } while (items.Count > 0);
            return results = calculationResults;
        }

        private IComparer<IRectangle> SetComparer(SortCondition _condition)
        {
            switch (_condition)
            {
                case SortCondition.Highest:
                    {
                        return new ItemHeightComparer();
                    }
                case SortCondition.Widest:
                    {
                        return new ItemWidthComparer();
                    }
                default:
                    {
                        return new ItemAreaComparer();
                    }
            }
        }

        public void AddItem(IRectangle _item)
        {
            items.Add(_item);
        }
    }
}
