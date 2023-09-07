using DotObjTools.Entities;
using DotObjTools.Enums;
using System.Numerics;

namespace DotObjTools.DOM;

internal class DotObjDOMBuilder
{
    private bool indexesAdjusted = false;

    private List<Vector4> _geometricVertices = new List<Vector4>();

    private List<Vector3> _textureVertices = new List<Vector3>();

    private List<Vector3> _vertexNormals = new List<Vector3>();

    private List<List<FaceItem_V>> _faces_V = new List<List<FaceItem_V>>();

    private List<List<FaceItem_V_Vn>> _faces_V_Vn = new List<List<FaceItem_V_Vn>>();

    private List<List<FaceItem_V_Vt>> _faces_V_Vt = new List<List<FaceItem_V_Vt>>();

    private List<List<FaceItem_V_Vt_Vn>> _faces_V_Vt_Vn = new List<List<FaceItem_V_Vt_Vn>>();

    public DotObjDOMBuilder()
    {

    }

    public DotObjDOM Build()
    {
        if (!indexesAdjusted)
        {
            AdjustIndexes();
            indexesAdjusted = true;
        }

        Validate();
        return new DotObjDOM(_geometricVertices, _textureVertices, _vertexNormals, _faces_V, _faces_V_Vt, _faces_V_Vn, _faces_V_Vt_Vn);
    }

    public void AddGeometricVertex(float x, float y, float z, float? w)
    {
        _geometricVertices.Add(new Vector4(x, y, z, w ?? 1.0f));
    }

    public void AddTextureVertex(float u, float? v, float? w)
    {
        _textureVertices.Add(new Vector3(u, v ?? 0.0f, w ?? 0.0f));
    }

    public void AddVertexNormal(float i, float j, float k)
    {
        _vertexNormals.Add(new Vector3(i, j, k));
    }

    public void AddFace(params (int V, int? Vt, int? Vn)[] items)
    {
        if (items.Length < 3)
        {
            throw new ArgumentOutOfRangeException(message: "minimum 3 vertices required", paramName: nameof(items));
        }

        FaceTypeEnum result = items.Select(item =>
        {
            FaceTypeEnum result = FaceTypeEnum.V;
            if (item.Vt.HasValue)
            {
                result |= FaceTypeEnum.Vt;
            }

            if (item.Vn.HasValue)
            {
                result |= FaceTypeEnum.Vn;
            }

            return result;
        }).Distinct().Single();

        switch (result)
        {
            case FaceTypeEnum.V:
                List<FaceItem_V> face_V = new List<FaceItem_V>(items.Length);
                foreach ((int V, int?, int?) item in items)
                {
                    face_V.Add(new FaceItem_V(item.V));
                }

                _faces_V.Add(face_V);
                break;

            case FaceTypeEnum.V | FaceTypeEnum.Vt:
                List<FaceItem_V_Vt> face_V_Vt = new List<FaceItem_V_Vt>(items.Length);
                foreach ((int V, int? Vt, int?) item in items)
                {
                    face_V_Vt.Add(new FaceItem_V_Vt(item.V, item.Vt!.Value));
                }

                _faces_V_Vt.Add(face_V_Vt);
                break;

            case FaceTypeEnum.V | FaceTypeEnum.Vn:
                List<FaceItem_V_Vn> face_V_Vn = new List<FaceItem_V_Vn>(items.Length);
                foreach ((int V, int?, int? Vn) item in items)
                {
                    face_V_Vn.Add(new FaceItem_V_Vn(item.V, item.Vn!.Value));
                }

                _faces_V_Vn.Add(face_V_Vn);
                break;

            case FaceTypeEnum.V | FaceTypeEnum.Vt | FaceTypeEnum.Vn:
                List<FaceItem_V_Vt_Vn> face_V_Vt_Vn = new List<FaceItem_V_Vt_Vn>(items.Length);
                foreach ((int V, int? Vt, int? Vn) item in items)
                {
                    face_V_Vt_Vn.Add(new FaceItem_V_Vt_Vn(item.V, item.Vt!.Value, item.Vn!.Value));
                }

                _faces_V_Vt_Vn.Add(face_V_Vt_Vn);
                break;

            default:
                throw new ArgumentException(message: $"How that is possible in {nameof(AddFace)} type of face is not match any of possible?");
        }
    }

    private void Validate()
    {
        //TODO: Add validation if needed
    }

    private void AdjustIndexes()
    {
        for (int polygonIterator = 0; polygonIterator < _faces_V.Count; polygonIterator++)
        {
            List<FaceItem_V> polygon = _faces_V[polygonIterator];
            for (int vertixIndexIterator = 0; vertixIndexIterator < polygon.Count; vertixIndexIterator++)
            {
                polygon[vertixIndexIterator] = new FaceItem_V(polygon[vertixIndexIterator].IndexV - 1);
            }
        }

        for (int polygonIterator = 0; polygonIterator < _faces_V_Vt.Count; polygonIterator++)
        {
            List<FaceItem_V_Vt> polygon = _faces_V_Vt[polygonIterator];
            for (int vertixIndexIterator = 0; vertixIndexIterator < polygon.Count; vertixIndexIterator++)
            {
                polygon[vertixIndexIterator] = new FaceItem_V_Vt(polygon[vertixIndexIterator].IndexV - 1, polygon[vertixIndexIterator].IndexVt - 1);
            }
        }

        for (int polygonIterator = 0; polygonIterator < _faces_V_Vn.Count; polygonIterator++)
        {
            List<FaceItem_V_Vn> polygon = _faces_V_Vn[polygonIterator];
            for (int vertixIndexIterator = 0; vertixIndexIterator < polygon.Count; vertixIndexIterator++)
            {
                polygon[vertixIndexIterator] = new FaceItem_V_Vn(polygon[vertixIndexIterator].IndexV - 1, polygon[vertixIndexIterator].IndexVn - 1);
            }
        }

        for (int polygonIterator = 0; polygonIterator < _faces_V_Vt_Vn.Count; polygonIterator++)
        {
            List<FaceItem_V_Vt_Vn> polygon = _faces_V_Vt_Vn[polygonIterator];
            for (int vertixIndexIterator = 0; vertixIndexIterator < polygon.Count; vertixIndexIterator++)
            {
                polygon[vertixIndexIterator] = new FaceItem_V_Vt_Vn(polygon[vertixIndexIterator].IndexV - 1, polygon[vertixIndexIterator].IndexVt - 1, polygon[vertixIndexIterator].IndexVn - 1);
            }
        }

        //TODO: CHECK FOR NEGATIVE VALUES AND 0s
    }
}