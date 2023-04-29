using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuilder
{
    internal interface IGenerator
    {
        public String CreateField(String type, String name);
        public String CreateStruct(String name);
        public String StartBracket { get; }
        public String EndBracket { get; }
    }
}
