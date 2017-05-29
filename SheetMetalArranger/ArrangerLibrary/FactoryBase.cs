using ArrangerLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangerLibrary
{
    public class FactoryBase
    {
        private IFactory defaultFactory = new DefaultFactory();
        public IFactory DefaultFactory
        {
            get { return defaultFactory; }
            protected set { defaultFactory = value; }
        }
    }
}
