using System;
using System.Collections.Generic;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public class Arrangement : FactoryBase, IArrangement
    {
        private List<IPanel> panels = new List<IPanel>();
        private List<IItem> leftItems = new List<IItem>();

        public Arrangement()
        { }

        public Arrangement(IFactory _factory)
        {
            DefaultFactory = _factory;
        }

        public double Utilisation
        {
            get
            {
                int totalArea = 0;
                int totalItems = 0;
                foreach (IPanel panel in panels)
                {
                    totalArea += panel.Area;
                    foreach (IAssignment asg in panel.Assignments)
                    {
                        totalItems += asg.Value.Area;
                    }
                }
                return Convert.ToSingle(totalItems / (double)totalArea);
            }
        }

        public int TotalPanelsArea
        {
            get
            {
                int value = 0;
                foreach (IPanel panel in panels)
                {
                    value += panel.Area;
                }
                return value;
            }
        }

        public int TotalItemsArea
        {
            get
            {
                int value = 0;
                foreach (IPanel panel in panels)
                {
                    foreach(IAssignment assignment in panel.Assignments)
                    {
                        value += assignment.Value.Area;
                    }
                }
                return value;
            }
        }

        public int ItemsArranged
        {
            get
            {
                int value = 0;
                foreach (IPanel panel in panels)
                {
                    value += panel.Assignments.Count;
                }
                return value;
            }
        }

        public List<IPanel> GetPanels()
        {
            return new List<IPanel>(panels);
        }

        public IPanel GetPanel(int _index)
        {
            if (_index<panels.Count) { return panels[_index]; }
            else { throw new InvalidOperationException("Attempted to access a panel which is out of collection range"); }
        }

        public void AddPanel(IPanel _panel)
        {
            panels.Add(_panel);
        }

        public List<IItem> GetLeftItems()
        {
            return new List<IItem>(leftItems);
        }

        public void LeaveItem(IItem _item)
        {
            leftItems.Add(_item);
        }

        public void AddPanels(List<IPanel> _panels)
        {
            panels.AddRange(_panels);
        }

        public IArrangement NewBranch()
        {
            IArrangement branch = new Arrangement();
            foreach(IItem item in leftItems)
            {
                branch.LeaveItem(item);
            }
            foreach(IPanel panel in panels)
            {
                branch.AddPanel(panel.Copy());
            }
            return branch;
        }

        public IPanel GetBestPanel()
        {
            panels.Sort(DefaultFactory.PanelComparer);
            panels.Reverse();
            return panels[0];
        }

        public IPanel GetWorstPanel()
        {
            panels.Sort(DefaultFactory.PanelComparer);
            return panels[0];
        }
    }
}
