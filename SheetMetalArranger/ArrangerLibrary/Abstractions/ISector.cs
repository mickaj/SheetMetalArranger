using System.Collections.Generic;

namespace ArrangerLibrary.Abstractions
{
    public interface ISector
    {
        List<IBox> DoSection(IBox _container, IItem _item);
    }
}
