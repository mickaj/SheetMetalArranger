using ArrangerLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    public class DefaultFactory : IFactory
    {
        private readonly ISector hSector = new HSector();
        private readonly ISector vSector = new VSector();
        public IMerger NewMerger()
        {
            return new Merger();
        }

        public IAssignment NewAssignment(IBox _box, IItem _item, bool _rotated)
        {
            return new Assignment(_box, _item, _rotated);
        }

        public ISector HSector
        {
            get { return hSector; }
        }

        public ISector VSector
        {
            get { return vSector; }
        }
    }
}
