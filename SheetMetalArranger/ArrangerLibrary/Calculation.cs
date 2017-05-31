using System;
using System.Collections.Generic;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public class Calculation : FactoryBase, ICalculation
    {
        private IBatch inputBatch;
        private List<IArrangement> arrangements;

        private bool allowNewPanels;
        private int defaultHeight;
        private int defaultWidth;

        private readonly PossibleFitAreaComparer fitComparer = new PossibleFitAreaComparer();

        public Calculation(IBatch _batch, int _newHeight, int _newWidth)
        {
            inputBatch = _batch;
            arrangements = new List<IArrangement>();
            allowNewPanels = true;
            defaultHeight = _newHeight;
            defaultWidth = _newWidth;
            arrangements.Add(DefaultFactory.NewArrangement());
            arrangements[0].AddPanel(DefaultFactory.NewPanel(defaultHeight, defaultWidth));
        }

        public Calculation(IBatch _batch, IPanel _defaultPanel)
        {
            inputBatch = _batch;
            arrangements = new List<IArrangement>();
            allowNewPanels = true;
            defaultHeight = _defaultPanel.Height;
            defaultWidth = _defaultPanel.Width;
            arrangements.Add(DefaultFactory.NewArrangement());
            arrangements[0].AddPanel(DefaultFactory.NewPanel(defaultHeight, defaultWidth));
        }

        public Calculation(IBatch _batch, List<IPanel> _panels)
        {
            inputBatch = _batch;
            arrangements = new List<IArrangement>();
            allowNewPanels = false;
            defaultHeight = 0;
            defaultWidth = 0;
            arrangements.Add(DefaultFactory.NewArrangement());
            arrangements[0].AddPanels(_panels);
        }

        public Calculation(IBatch _batch, List<IPanel> _panels, int _newHeight, int _newWidth)
        {
            inputBatch = _batch;
            arrangements = new List<IArrangement>();
            allowNewPanels = true;
            defaultHeight = _newHeight;
            defaultWidth = _newWidth;
            arrangements.Add(DefaultFactory.NewArrangement());
            arrangements[0].AddPanels(_panels);
        }

        public void Calculate(IComparer<IItem> _item1comparer, IComparer<IItem> _item2comparer, IComparer<IItem> _item3comparer, ISector _sector, Action<int> _notifier)
        {
            int itemsProcessed = 0;
            IItem currentItem;
            List<PossibleFit> fits = new List<PossibleFit>();
            List<IArrangement> newArrangements = new List<IArrangement>();
            while (inputBatch.Remaining > 0)
            {
                currentItem = inputBatch.GetFirst(_item1comparer, _item2comparer, _item3comparer);
                foreach(IArrangement currentArrangement in arrangements)
                {
                    fits.Clear();
                    newArrangements.Clear();
                    fits.AddRange(findPossibleFits(currentItem, currentArrangement));
                    //if there are fits possible
                    if (fits.Count >0)
                    {
                        //sort possible fits according to area ratio
                        fits.Sort(fitComparer);
                        //look for equally good fits
                        for (int i = 1; i < fits.Count; i++)
                        {
                            //if (fits[0].AreaRatio == fits[i].AreaRatio)
                            //{
                                //if such a fit is found do:
                                //1) create new barrangement branch
                                IArrangement newBranch = currentArrangement.NewBranch();
                                //2) do the assignment in new branch
                                newBranch.GetPanel(fits[i].PanelIndex).Assign(fits[i].UsedBox, currentItem, _sector, fits[i].Rotated);
                                //3) add new branch to new arrangements list
                                newArrangements.Add(newBranch);
                                //throw new InvalidOperationException("there are some equally good fits");
                            //}
                        }
                        //use best fit (area ratio-wise) and do the assignment
                        fits[0].MotherPanel.Assign(fits[0].UsedBox, currentItem, _sector, fits[0].Rotated);
                    }
                    //if no fit is possible
                    else
                    {
                        // if it is allowed to create new panles and new panel would hold the item
                        IBox tempBox = DefaultFactory.NewBox(0, 0, defaultHeight, defaultWidth);
                        if ((allowNewPanels)&&(tempBox.CanHold(currentItem)>0))
                        {
                            IPanel newPanel = DefaultFactory.NewPanel(defaultHeight, defaultWidth);
                            if(tempBox.CanHold(currentItem)==1) { newPanel.Assign(newPanel.GetBox(0), currentItem, _sector, false); }
                            if(tempBox.CanHold(currentItem)==2) { newPanel.Assign(newPanel.GetBox(0), currentItem, _sector, true); }
                            currentArrangement.AddPanel(newPanel);
                        }
                        else
                        //if new panels are not allowed or item is to big for new panel
                        {
                            currentArrangement.LeaveItem(currentItem);
                        }
                    }
                }
                //add new arrangement branches to current calculation branches
                arrangements.AddRange(newArrangements);
                //remove current item from input batch
                inputBatch.RemoveItem(currentItem);
                _notifier(++itemsProcessed);
            }
        }

        public string OutputString()
        {
            string output = "Calculation results:\n";
            int arrangementNo = 1;
            foreach(IArrangement arr in arrangements)
            {
                int panelNo = 1;
                output += String.Format("--------------------------------------\n***Arrangement no: {0}({1})***\n",arrangementNo,arr.Utilisation);
                foreach(IPanel pnl in arr.GetPanels())
                {
                    output += String.Format("---Panel no: {0}({1})\n", panelNo, pnl.Utilisation);
                    foreach (IAssignment asg in pnl.Assignments)
                    {
                        output += String.Format("Height={0}; Width={1}; PosX={2}; PosY={3}; Rotated={4}\n", asg.Value.Height, asg.Value.Width, asg.Key.PosX, asg.Key.PosY, asg.Rotated);
                    }
                    panelNo++;
                }
                output += String.Format("---Left items: {0}\n", arr.GetLeftItems().Count);
                foreach(IItem item in arr.GetLeftItems())
                {
                    output += String.Format("Height={0}; Width={1}\n", item.Height, item.Width);
                }
                arrangementNo++;
            }
            return output;
        }

        public string OutputBest()
        {
            string output = "BEST RESULT: \n";
            arrangements.Sort(DefaultFactory.ArrangementRatioComparer);
            IArrangement arr = arrangements[0];
            int panelNo = 1;
            output += String.Format("--------------------------------------\n***Utilisation: {0}***\n",arr.Utilisation);
            foreach (IPanel pnl in arr.GetPanels())
            {
                output += String.Format("---Panel no: {0}({1})\n", panelNo, pnl.Utilisation);
                //foreach (Assignment asg in pnl.Assignments)
                //{
                //    output += String.Format("Height={0}; Width={1}; PosX={2}; PosY={3}; Rotated={4}\n", asg.Value.Height, asg.Value.Width, asg.Key.PosX, asg.Key.PosY, asg.Rotated);
                //}
                panelNo++;
            }
            output += String.Format("---Left items: {0}\n", arr.GetLeftItems().Count);
            //foreach (IItem item in arr.GetLeftItems())
            //{
            //    output += String.Format("Height={0}; Width={1}\n", item.Height, item.Width);
            //}
            return output;
        }

        public IArrangement GetBestArrangement()
        {
            arrangements.Sort(DefaultFactory.ArrangementRatioComparer);
            return arrangements[0];
        }

        private List<PossibleFit> findPossibleFits(IItem _item, IArrangement _arrangement)
        {
            List<PossibleFit> possibleFits = new List<PossibleFit>();
            List<IPanel> panelsInArrangement = _arrangement.GetPanels();
            foreach (IPanel currentPanel in panelsInArrangement)
            {
                List<IBox> boxes = new List<IBox>(currentPanel.GetBoxes());
                foreach (IBox box in boxes)
                {
                    if (box.CanHold(_item) == 1) { possibleFits.Add(new PossibleFit(box, _item, currentPanel, false, _arrangement.GetPanels().IndexOf(currentPanel))); }
                    if (box.CanHold(_item) == 2) { possibleFits.Add(new PossibleFit(box, _item, currentPanel, true, _arrangement.GetPanels().IndexOf(currentPanel))); }
                }
            }
            return possibleFits;
        }

        private sealed class PossibleFit
        {
            public IBox UsedBox { get; private set; }
            public IItem PlacedItem { get; private set; }
            public IPanel MotherPanel { get; private set; }
            public double AreaRatio { get { return Convert.ToSingle(PlacedItem.Area / (double)UsedBox.Area); } }
            public bool Rotated { get; private set; }
            public int PanelIndex { get; private set; }

            public PossibleFit(IBox _box, IItem _item, IPanel _mother, bool _rotated, int _panelIndex)
            {
                UsedBox = _box;
                PlacedItem = _item;
                Rotated = _rotated;
                PanelIndex = _panelIndex;
                MotherPanel = _mother;
            }
        }

        private sealed class PossibleFitAreaComparer : IComparer<PossibleFit>
        {
            public int Compare(PossibleFit _fit1, PossibleFit _fit2)
            {
                if (_fit1 == null)
                {
                    if (_fit2 == null) { return 0; }
                    else { return -1; }
                }
                else
                {
                    if (_fit2 == null) { return 1; }
                    else { return (_fit1.AreaRatio.CompareTo(_fit2.AreaRatio)); }
                }
            }
        }
    }
}

