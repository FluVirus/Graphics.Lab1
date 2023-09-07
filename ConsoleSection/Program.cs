using DotObjTools.DOM;
using DotObjTools.Parser;

namespace ConsoleSection
{
    internal class Program
    {
        static string Path = "../../../../../example.obj";
        
        static void Main(string[] args)
        {
            IDotObjParser parser = new DotObjParser();
            DotObjDOM dom = parser.Parse(Path);
            Console.WriteLine(dom);
        }
    }
}