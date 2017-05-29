using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary.Abstractions
{
    public interface IFactory
    {
        IMerger NewMerger();
        IAssignment NewAssignment(IBox _box, IItem _item, bool _rotated);
        ISector HSector { get; }
        ISector VSector { get; }
    }
}
