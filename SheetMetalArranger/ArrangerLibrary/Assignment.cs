using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArrangerLibrary.Abstractions;

namespace ArrangerLibrary
{
    public class Assignment:IAssignment
    {
        public IBox Key { get; private set; }
        public IItem Value { get; private set; }
        public bool Rotated { get; private set; }

        public Assignment(IBox _box, IItem _item, bool _rotated)
        {
            Key = _box;
            Value = _item;
            Rotated = _rotated;
        }
    }
}
