using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrangerLibrary
{
    public class Container
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        //public Container Left;
        //public Container Right;
        //public bool Used;
        public Item ItemPlaced;
        public int Area
        {
            get
            {
                return Height * Width;
            }
        }
    }
    public class Item
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Area
        {
            get
            {
                return Height * Width;
            }
        }
        public int LongerEdge
        {
            get
            {
                if (Width >= Height)
                {
                    return Width;
                }
                else
                {
                    return Height;
                }
            }
        }
        //public bool Allocated { get; set; }
        public Container Location { get; set; }
        //public int XAllocation { get; set; }
        //public int YAllocation { get; set; }
    }

    public class Arranger
    {
        List<Container> containers = new List<Container>();
        List<Container> usedContainers = new List<Container>();
        List<Item> items = new List<Item>();
        List<Item> placedItems = new List<Item>();
        
        public Arranger()
        {
            //initialize starting point
            containers.Add(new Container { X = 0, Y = 0, Height = 10, Width = 20 });
            items.Add(new Item { Height = 5, Width = 3});
            items.Add(new Item { Height = 3, Width = 3});
            items.Add(new Item { Height = 1, Width = 5 });
            items.Add(new Item { Height = 4, Width = 3 });
            items.Add(new Item { Height = 3, Width = 5 });
            items.Add(new Item { Height = 5, Width = 2 });
            while (items.Count >0)
            {
                //sort items according to its area
                //items.Sort((Item x, Item y) => x.Area.CompareTo(y.Area));
                //sort items according to its height
                items.Sort((Item x, Item y) => x.Height.CompareTo(y.Height));
                //sort items according to its width
                //items.Sort((Item x, Item y) => x.Width.CompareTo(y.Width));
                //sort items according to its longer edge
                //items.Sort((Item x, Item y) => x.LongerEdge.CompareTo(y.LongerEdge));
                items.Reverse();
                //cycle through available containers and find best fit (area ratio)
                int bestFitContainerIndex = -1;
                for (int i = 0; i <= containers.Count - 1; i++)
                {
                    //check if container can hold the item
                    if ((items[0].Height <= containers[i].Height) && (items[0].Width <= containers[i].Width))
                    {
                        //if there is already a container proposed compare them which is better; if not assing it ot a container 
                        if (bestFitContainerIndex >= 0)
                        {
                            float ratioCurrent = items[0].Area / containers[bestFitContainerIndex].Area;
                            float ratioProposed = items[0].Area / containers[i].Area;
                            //if proposed container has better ratio then assing the item to it
                            if (ratioProposed > ratioCurrent)
                            {
                                bestFitContainerIndex = i;
                            }
                        }
                        else
                        {
                            bestFitContainerIndex = i;
                        }
                    }
                }
                //do the assingment
                items[0].Location = containers[bestFitContainerIndex];
                containers[bestFitContainerIndex].ItemPlaced = items[0];
                //cut remaining space in container along shorter edge
                //step 1: find possible cut lines
                //int horizontal = containers[bestFitContainerIndex].Width - items[0].Width;
                //int vertical = containers[bestFitContainerIndex].Height - items[0].Height;
                //determine which is optimal for next item to be place by comparing area ration in every possible case
                //step 3a: do the section vertically
                //first newly created container
                int newX = containers[bestFitContainerIndex].X;
                int newY = containers[bestFitContainerIndex].Y + items[0].Height;
                int newWidth = items[0].Width;
                int newHeight = containers[bestFitContainerIndex].Height - items[0].Height;
                Container v1 = new Container { X = newX, Y = newY, Height = newHeight, Width = newWidth };
                //second newly created container
                newX = containers[bestFitContainerIndex].X + items[0].Width;
                newY = containers[bestFitContainerIndex].Y;
                newWidth = containers[bestFitContainerIndex].Width - items[0].Width;
                newHeight = containers[bestFitContainerIndex].Height;
                Container v2 = new Container { X = newX, Y = newY, Height = newHeight, Width = newWidth };
                //step 3b: do the section horizontally
                //first newly created container
                newX = containers[bestFitContainerIndex].X + items[0].Width;
                newY = containers[bestFitContainerIndex].Y;
                newWidth = containers[bestFitContainerIndex].Width - items[0].Width;
                newHeight = items[0].Height;
                Container h1 = new Container { X = newX, Y = newY, Height = newHeight, Width = newWidth };
                //second newly created container
                newX = containers[bestFitContainerIndex].X;
                newY = containers[bestFitContainerIndex].Y + items[0].Height;
                newWidth = containers[bestFitContainerIndex].Width;
                newHeight = containers[bestFitContainerIndex].Height - items[0].Height;
                Container h2 = new Container { X = newX, Y = newY, Height = newHeight, Width = newWidth };
                //move used container to used containers
                usedContainers.Add(containers[bestFitContainerIndex]);
                containers.Remove(containers[bestFitContainerIndex]);
                Dictionary<string, float> ratioDict = new Dictionary<string, float>();
                if (items.Count >1)
                {
                    if (v1.Area > 0) ratioDict.Add("V1", items[1].Area / v1.Area);
                    if (v2.Area > 0) ratioDict.Add("V2", items[1].Area / v2.Area);
                    if (h1.Area > 0) ratioDict.Add("H1", items[1].Area / h1.Area);
                    if (h2.Area > 0) ratioDict.Add("H2", items[1].Area / h2.Area);
                    var ratioItems = from pair in ratioDict
                                     orderby pair.Value descending
                                     select pair;
                    ratioDict = ratioItems.ToDictionary<KeyValuePair<string, float>, string, float>(pair => pair.Key, pair => pair.Value);
                    if (ratioDict.ElementAt(0).Key[0] == 'V')
                    {
                        containers.Add(v1);
                        containers.Add(v2);
                    }
                    else
                    {
                        containers.Add(h1);
                        containers.Add(h2);
                    }
                }
                //move current item to placedItems
                placedItems.Add(items[0]);
                items.Remove(items[0]);
                Console.WriteLine("Currently available containers: ");
                foreach (Container ic in containers)
                {
                    Console.WriteLine("Container: X={0}, Y={1}, H={2}, W={3}", ic.X, ic.Y, ic.Height, ic.Width);
                }
            }
        }
        
        public void DisplayResults()
        {
            Console.WriteLine("Unassinged items: {0}", items.Count);
            Console.WriteLine("Assigned items: {0}", placedItems.Count);
            Console.WriteLine("Used containers: {0}", usedContainers.Count);
            Console.WriteLine("Items: ");
            foreach(Item i in placedItems)
            {
                int w = i.Width;
                int h = i.Height;
                int x = i.Location.X;
                int y = i.Location.Y;
                Console.WriteLine("Item size: {0}x{1}   |   Container: X={2}, Y={3}", w, h, x, y);
            }
        } 
    }


     

      
}
