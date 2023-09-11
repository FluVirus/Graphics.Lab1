using DotObjTools.DOM;
using DotObjTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace GUISection.Extensions;

internal static class DotObjDomExtenions
{
    public static (int, int)[] getLines(this DotObjDOM dom)
    {
        HashSet<(int, int)> modelLines = new HashSet<(int, int)>(3 * dom.Faces.Length);
        
        foreach (FaceItem[] face in dom.Faces)
        {
            for (int i = 0; i < face.Length; i++)
            {
                modelLines.Add
                ((
                    face[i].V,
                    face[(i + 1) % face.Length].V
                ));
            }
        }

        return modelLines.ToArray();
    }
}