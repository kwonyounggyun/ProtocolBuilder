using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProtocolBuilder
{
    internal class XMLReader
    {
        static IGenerator gen;

        public static String Parsing(String path)
        {
            StringBuilder sb = new StringBuilder();

            var xml_doc = new XmlDocument();
            xml_doc.Load(path);
            if (xml_doc == null)
                return sb.ToString();

            var root = xml_doc.GetElementsByTagName("packet");
            gen = new CPPGenerator();

            Parsing(sb, root[0].ChildNodes);

            return sb.ToString();
        }

        enum Type_Order
        {
            STRUCT = 0,
            START_BRACKET = 1,
            FIELD = 2,
            FUCNTION = 3,
            END_BRACKET = 4,
        }

        private static Int32 seq = 0;
        static UInt64 CreateKey(Type_Order order, bool init = false)
        {
            if (init)
                seq = 0;

            UInt64 key = (UInt64)order << 32 | (UInt64)seq;
            seq++;

            return key;
        }

        static Dictionary<UInt64, String> dic_struct = new Dictionary<UInt64, String>();
        static int st = 0;
        private static void Parsing(StringBuilder sb, XmlNode node, Func<String, String> func = null)
        {
            /*Case data type select*/
            if(node.Name.Equals("field"))
            {
                var type_attr = node.Attributes["type"];
                var name_attr = node.Attributes["name"];
                dic_struct.Add(CreateKey(Type_Order.FIELD), gen.CreateField(type_attr.Value, name_attr.Value));
            }
            else if(node.Name.Equals("request") || node.Name.Equals("response") || node.Name.Equals("information"))
            {
                var str_name = String.Format("{0}_{1}", node.Name, func(node.Name));
                dic_struct.Add(CreateKey(Type_Order.STRUCT, true), gen.CreateStruct(str_name));
                dic_struct.Add(CreateKey(Type_Order.START_BRACKET), gen.StartBracket);
                dic_struct.Add(CreateKey(Type_Order.END_BRACKET), gen.EndBracket);
                Parsing(sb, node.ChildNodes);

                var order = from item in dic_struct
                            orderby item.Key
                            select item;

                var new_dic = order.ToDictionary(x => x.Key, x => x.Value);
                foreach (var pair in new_dic)
                {
                    sb.AppendLine(pair.Value);
                }
                sb.AppendLine();

                dic_struct.Clear();
            }
            else if (node.Name.Equals("packet_dual") || node.Name.Equals("packet_one"))
            {
                var name = node.Attributes["name"].Value;
                var src = node.Attributes["src"].Value;
                var dst = node.Attributes["dst"].Value;

                Func<String, String> name_prefix = (String node_name) =>
                {
                    if(node_name.Equals("response"))
                        return String.Format("{0}_{1}_{2}", dst, src, name);

                    return String.Format("{0}_{1}_{2}", src, dst, name);
                };
                Parsing(sb, node.ChildNodes, name_prefix);
            }
        }

        private static void Parsing(StringBuilder sb, XmlNodeList nodes, Func<String, String> func = null)
        {
            foreach (XmlNode node in nodes)
            {
                Parsing(sb, node, func);
            }
        }
    }
}
