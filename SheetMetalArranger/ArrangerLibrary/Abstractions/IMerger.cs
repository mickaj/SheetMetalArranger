using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary.Abstractions
{
    public interface IMerger
    {
        List<IBox> GetMerged(List<IBox> _list);
    }
}
