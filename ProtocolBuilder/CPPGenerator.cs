using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuilder
{
    internal class CPPGenerator : IGenerator
    {
        public string StartBracket { get { return "{"; }  }

        public string EndBracket { get { return "}"; } }

        public string CreateField(string type, string name)
        {
            return String.Format("\t{0} {1};", type, name);
        }

        public string CreateStruct(string name)
        {
            return String.Format("struct {0}", name);
        }
    }
}
