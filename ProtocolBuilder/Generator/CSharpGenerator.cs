using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuilder.Generator
{
    internal class CSharpGenerator : IGenerator
    {
        public string Start { get { return ""; } }

        public string StartBracket { get { return "{"; } }

        public string EndBracket { get { return "}"; } }

        public string End { get { return ""; } }

        public string CreateField(string type, string name)
        {
            return string.Format("\tpublic {0} {1};", type, name);
        }

        public string CreateStruct(string name)
        {
            return string.Format("[StructLayout(LayoutKind.Sequential, Pack = 1)]\r\npublic class {0}", name);
        }
    }
}
