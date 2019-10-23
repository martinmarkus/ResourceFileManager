using FactorySupporter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class DisplayerAttribute : IdentifierAttribute
    {
        public int id { get; set; }

        public DisplayerAttribute(int id)
        {
            this.id = id;
        }
    }
}
