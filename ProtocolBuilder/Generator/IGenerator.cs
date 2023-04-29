using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuilder.Generator
{
    internal interface IGenerator
    {
        public string Start { get; }
        public string CreateField(string type, string name);
        public string CreateStruct(string name);
        public string StartBracket { get; }
        public string EndBracket { get; }
        public string End { get; }
    }
}
