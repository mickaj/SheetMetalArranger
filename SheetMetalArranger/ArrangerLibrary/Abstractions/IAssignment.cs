using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary.Abstractions
{
    public interface IAssignment
    {
        IBox Key { get; }
        IItem Value { get; }
        bool Rotated { get; }
    }
}
