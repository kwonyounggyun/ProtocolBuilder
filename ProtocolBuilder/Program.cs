// See https://aka.ms/new-console-template for more information
using ProtocolBuilder;
using System.IO;
using System.Text;
using System.Xml;

Console.WriteLine("Hello, World!");


var str = XMLReader.Parsing("D:\\Test.xml");
Console.WriteLine(str);