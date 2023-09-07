using DotObjTools.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotObjTools.Parser;

public interface IDotObjParser
{
    DotObjDOM Parse(Stream stream, bool leaveOpen);
    DotObjDOM Parse(string fileName);
}
