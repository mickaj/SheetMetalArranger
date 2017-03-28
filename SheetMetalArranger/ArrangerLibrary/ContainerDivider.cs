using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    public class ContainerDivider
    {
        public enum SectionCondition
        {
            Highest,
            Widest,
            Area
        }

        private IContainer container;
        private IRectangle item;

        public ContainerDivider(IContainer _container, IRectangle _item)
        {
            container = _container;
            item = _item;
        }
        
        public List<IContainer> Section(SectionCondition _condition)
        {
            return null;
        }

    }
}
