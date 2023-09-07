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
    public static (Vector4, Vector4)[] getModelLines(this DotObjDOM dom)
    {
        HashSet<(Vector4, Vector4)> modelLines = new HashSet<(Vector4, Vector4)>(3 * (dom.Faces_V.Length + dom.Faces_V_Vt.Length + dom.Faces_V_Vn.Length + dom.Faces_V_Vt_Vn.Length));
        
        foreach (FaceItem_V[] face in dom.Faces_V)
        {
            for (int i = 0; i < face.Length; i++)
            {
                modelLines.Add
                ((
                    dom.GeometricVertices[face[i].IndexV],
                    dom.GeometricVertices[face[(i + 1) % face.Length].IndexV]
                ));
            }
        }

        throw new NotImplementedException();
    }
}