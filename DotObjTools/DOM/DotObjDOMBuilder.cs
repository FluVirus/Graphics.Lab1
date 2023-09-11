using DotObjTools.Entities;
using DotObjTools.Enums;
using DotObjTools.Exceptions;
using System.Numerics;

namespace DotObjTools.DOM;

internal class DotObjDOMBuilder
{
    private bool indexesAdjusted = false;

    private List<Vector4> _geometricVertices = new List<Vector4>();

    private List<Vector3> _textureVertices = new List<Vector3>();

    private List<Vector3> _vertexNormals = new List<Vector3>();

    private List<List<FaceItem>> _faces = new List<List<FaceItem>>();

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
        return new DotObjDOM(_geometricVertices, _textureVertices, _vertexNormals, _faces);
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

        List<FaceItem> face = new List<FaceItem>(items.Length);
        foreach ((int V, int? Vt, int? Vn) item in items)
        {
            face.Add(new FaceItem(item.V, item.Vt, item.Vn));
        }

        _faces.Add(face);

    }

    private void Validate()
    {
        //TODO: Add validation if needed
    }

    private void AdjustIndexes()
    {
        for (int polygonIterator = 0; polygonIterator < _faces.Count; polygonIterator++)
        {
            List<FaceItem> polygon = _faces[polygonIterator];
            for (int vertixIndexIterator = 0; vertixIndexIterator < polygon.Count; vertixIndexIterator++)
            {
                FaceItem oldFaceItem = polygon[vertixIndexIterator];
                polygon[vertixIndexIterator] = new FaceItem(
                            AdjustIndex(oldFaceItem.V)!.Value,
                            AdjustIndex(oldFaceItem.Vt),
                            AdjustIndex(oldFaceItem.Vn)
                );
            }
        }
    }

    private int? AdjustIndex(int? index)
    {
        if (!index.HasValue)
            return null;

        if (index.Value == 0)
            throw new ArgumentOutOfRangeException(paramName: nameof(index), message: "0 vertex index spotted");

        if (index.Value > 0)
            return index.Value - 1;

        int toRet = _geometricVertices.Count - index.Value;
        if (toRet <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(index), message: "Negative out of boundary vertex index spotted");
        }

        return toRet;
    }
}