using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuilder.Generator
{
    internal class CPPGenerator : IGenerator
    {
        public string StartBracket { get { return "{"; } }

        public string EndBracket { get { return "}"; } }

        public string CreateField(string type, string name)
        {
            return string.Format("\t{0} {1};", type, name);
        }

        public string CreateStruct(string name)
        {
            return string.Format("struct {0}", name);
        }

        public string End
        {
            get { return "#pragma pack(pop)"; }
        }

        public string Start
        {
            get { return "#pragma once\r\n#pragma pack(push,1)\r\n\r\n"; }
        }
    }
}
