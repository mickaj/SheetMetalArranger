using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrangerLibrary
{
    class PossibleFit
    {
        public Container MainContainer;
        public int ResultsIndex;
        public float AreaRatio; 
    }
    public class ItemContainerPair
    {
        public Item Occupant { get; set; }
        public Container Occupied { get; set; }

        public ItemContainerPair(Item _item, Container _container)
        {
            Occupant = _item;
            Occupied = _container;
        }
    }

    public class Arranger
    {
        List<Item> items = new List<Item>();
        List<Item> placedItems = new List<Item>();
        Dictionary<int,ArrangerResults> results = new Dictionary<int, ArrangerResults>();
        int sheetIndex = 0;
        IComparer<Item> comparer;

        public Arranger()
        {
            //initialize starting items
            items.Add(new Item { Height = 5, Width = 3 });
            items.Add(new Item { Height = 3, Width = 3 });
            items.Add(new Item { Height = 1, Width = 5 });
            items.Add(new Item { Height = 4, Width = 3 });
            items.Add(new Item { Height = 3, Width = 5 });
            items.Add(new Item { Height = 5, Width = 2 });
            results = Calculate(10, 13, 'a');
        }

        private Dictionary<int,ArrangerResults> Calculate(int _sheetHeight, int _sheetWidth, char _comparer)
        {
            //initialize first set of results
            Dictionary<int, ArrangerResults> calculationResults = new Dictionary<int, ArrangerResults>();
            ArrangerResults initialResults = new ArrangerResults(_sheetHeight, _sheetWidth);
            calculationResults.Add(sheetIndex, initialResults);
            //initialize comparer
            comparer = new ItemAreaComparer();
            if ((_comparer == 'H') || (_comparer == 'h'))
            {
                comparer = new ItemHeightComparer();
            }
            else
            {
                if ((_comparer == 'W') || (_comparer == 'w'))
                {
                    comparer = new ItemWidthComparer();
                }
            }
            do
            {
                //sort items with selected comparer and revers order
                items.Sort(comparer);
                items.Reverse();
                Item currentItem = items[0];
                Item nextItem;
                if(items.Count>1)
                {
                    nextItem = items[1];
                }
                else
                {
                    nextItem = null;
                }
                //take first items from items list and try to find a container that can hold it
                List<PossibleFit> possibleFits = new List<PossibleFit>();
                //cycle through all ArrangerResults
                foreach (KeyValuePair<int, ArrangerResults> result in calculationResults)
                {
                    //cycle through all available containers in 
                    foreach (Container thisContainer in result.Value.availableContainers)
                    {
                        if (CheckIfFits(currentItem, thisContainer))
                        {
                            PossibleFit fit = new PossibleFit();
                            fit.MainContainer = thisContainer;
                            fit.ResultsIndex = result.Key;
                            fit.AreaRatio = (float)currentItem.Area / (float)thisContainer.Area;
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
                    List<Container> newContainers = DoSection(possibleFits[0].MainContainer, currentItem, nextItem);
                    calculationResults[possibleFits[0].ResultsIndex].availableContainers.AddRange(newContainers);
                }
                else //if none of all available containers can hold the item
                {
                    //create new result set - in practice in means that new sheet must be used
                    ArrangerResults newResults = new ArrangerResults(_sheetHeight, _sheetWidth);
                    calculationResults.Add(++sheetIndex, newResults);
                    //place item in newly created result set
                    Container usedContainer = calculationResults[sheetIndex].availableContainers[0];
                    calculationResults[sheetIndex].Assign(currentItem, usedContainer);
                    //perform section of used container
                    List<Container> newContainers = DoSection(usedContainer, currentItem, nextItem);
                    calculationResults[sheetIndex].availableContainers.AddRange(newContainers);
                }
                //at the end remove currently processed item from list of items
                placedItems.Add(currentItem);
                items.Remove(currentItem);
            } while (items.Count > 0);
            return calculationResults;
        }

        private bool CheckIfFits(Item _item, Container _container)
        {
            if((_item.Height<=_container.Height)&&(_item.Width<=_container.Width))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Container> DoSection(Container _container, Item _item, Item _nextItem)
        {
            List<Container> sectionResults = new List<Container>();
            //vertical section
            //first newly created container
            int newX = _container.X;
            int newY = _container.Y + _item.Height;
            int newWidth = _item.Width;
            int newHeight = _container.Height - _item.Height;
            Container v1 = new Container { X = newX, Y = newY, Height = newHeight, Width = newWidth };
            //second newly created container
            newX = _container.X + _item.Width;
            newY = _container.Y;
            newWidth = _container.Width - _item.Width;
            newHeight = _container.Height;
            Container v2 = new Container { X = newX, Y = newY, Height = newHeight, Width = newWidth };
            //horizontal section
            //first newly created container
            newX = _container.X + _item.Width;
            newY = _container.Y;
            newWidth = _container.Width - _item.Width;
            newHeight = _item.Height;
            Container h1 = new Container { X = newX, Y = newY, Height = newHeight, Width = newWidth };
            //second newly created container
            newX = _container.X;
            newY = _container.Y + _item.Height;
            newWidth = _container.Width;
            newHeight = _container.Height - _item.Height;
            Container h2 = new Container { X = newX, Y = newY, Height = newHeight, Width = newWidth };
            Dictionary<string, float> ratioDict = new Dictionary<string, float>();
            if (_nextItem != null)
            {
                if (v1.Area > 0) ratioDict.Add("V1", _nextItem.Area / v1.Area);
                if (v2.Area > 0) ratioDict.Add("V2", _nextItem.Area / v2.Area);
                if (h1.Area > 0) ratioDict.Add("H1", _nextItem.Area / h1.Area);
                if (h2.Area > 0) ratioDict.Add("H2", _nextItem.Area / h2.Area);
                var ratioItems = from pair in ratioDict
                                 orderby pair.Value descending
                                 select pair;
                ratioDict = ratioItems.ToDictionary<KeyValuePair<string, float>, string, float>(pair => pair.Key, pair => pair.Value);
                if (ratioDict.ElementAt(0).Key[0] == 'V')
                {
                    sectionResults.Add(v1);
                    sectionResults.Add(v2);
                }
                else
                {
                    sectionResults.Add(h1);
                    sectionResults.Add(h2);
                }
            }
            return sectionResults;
        }

        public void DisplayResults()
        {
            Console.WriteLine("Unassinged items: {0}", items.Count);
            Console.WriteLine("Assigned items: {0}", placedItems.Count);
            int usedContainers = 0;
            foreach (KeyValuePair<int,ArrangerResults> res in results)
            {
                usedContainers += res.Value.usedContainers.Count;
            }
            Console.WriteLine("Used containers: {0}", usedContainers);
            Console.WriteLine("Used sheets: {0}", results.Count);
            Console.WriteLine("Items: ");
            foreach (KeyValuePair<int,ArrangerResults> i in results)
            {
                Console.WriteLine("Sheet No: {0}", i.Key);
                foreach (ItemContainerPair j in i.Value.assignment)
                {
                    int w = j.Occupant.Width;
                    int h = j.Occupant.Height;
                    int x = j.Occupied.X;
                    int y = j.Occupied.Y;
                    Console.WriteLine("Item size: {0}x{1}   |   Container: X={2}, Y={3}", w, h, x, y);
                }
            }
        }
    }
}