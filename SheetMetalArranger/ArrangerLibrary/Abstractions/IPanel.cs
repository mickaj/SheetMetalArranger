﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary.Abstractions
{
    public interface IPanel
    {
        int Height { get; set; }
        int Width { get; set; }
        int Area { get; }
        int AvailableBoxes { get; }
        int Assigned { get; }

        double Utilisation { get; }

        List<IAssignment> Assignments { get; }

        void Assign(IBox _box, IItem _item, ISector _sector, bool _rotated);

        List<IBox> GetBoxes();

        IBox GetBox(int _index);

        IPanel Copy();
    }
}
