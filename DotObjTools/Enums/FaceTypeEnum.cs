using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotObjTools.Enums;

[Flags]
internal enum FaceTypeEnum
{
    None = 0,
    V = 4,
    Vt = 2,
    Vn = 1
}
