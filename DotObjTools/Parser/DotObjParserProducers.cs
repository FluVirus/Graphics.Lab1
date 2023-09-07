using DotObjTools.Consts;
using DotObjTools.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotObjTools.Parser;

public partial class DotObjParser: IDotObjParser
{
    private void GeometricVertexProducer(string[] words)
    {
        if (words[0] != DotObjLexems.GeometricVertex)
        {
            throw new ArgumentOutOfRangeException(message: $"Unappropriate word set in {nameof(GeometricVertexProducer)}", paramName: nameof(words));
        }

        if (words.Length != 4 && words.Length != 5)
        {
            throw new ArgumentOutOfRangeException(message: $"Unappropriate number of arguments in {DotObjLexems.GeometricVertex} element", paramName: nameof(words));
        }

        float x = float.Parse(words[1], NumberStyles.Float, CultureInfo.InvariantCulture);
        float y = float.Parse(words[2], NumberStyles.Float, CultureInfo.InvariantCulture);
        float z = float.Parse(words[3], NumberStyles.Float, CultureInfo.InvariantCulture);
        float w = words.Length == 5 ? float.Parse(words[4]) : 1.0f;

        _builder.AddGeometricVertex(x, y, z, w);
    }

    private void TextureVertexProducer(string[] words) 
    {
        if (words[0] != DotObjLexems.TextureVertex)
        {
            throw new ArgumentOutOfRangeException(message: $"Unappropriate word set in {nameof(TextureVertexProducer)}", paramName: nameof(words));
        }

        if (words.Length < 2 || words.Length > 4)
        {
            throw new ArgumentOutOfRangeException(message: $"Unappropriate number of arguments in {DotObjLexems.TextureVertex} element", paramName: nameof(words));
        }

        float u = float.Parse(words[1], NumberStyles.Float, CultureInfo.InvariantCulture);
        float v = words.Length >= 3 ? float.Parse(words[2], NumberStyles.Float, CultureInfo.InvariantCulture) : 0.0f;
        float w = words.Length == 4? float.Parse(words[3], NumberStyles.Float, CultureInfo.InvariantCulture) : 0.0f;

        _builder.AddTextureVertex(u, v, w);
    }

    private void VertexNormalProducer(string[] words)
    {
        if (words[0] != DotObjLexems.VertexNormal)
        {
            throw new ArgumentOutOfRangeException(message: $"Unappropriate word set in {nameof(VertexNormalProducer)}", paramName: nameof(words));
        }

        if (words.Length != 4)
        {
            throw new ArgumentOutOfRangeException(message: $"Unappropriate number of arguments in {DotObjLexems.VertexNormal} element", paramName: nameof(words));
        }

        float i = float.Parse(words[1], NumberStyles.Float, CultureInfo.InvariantCulture);
        float j = float.Parse(words[2], NumberStyles.Float, CultureInfo.InvariantCulture);
        float k = float.Parse(words[3], NumberStyles.Float, CultureInfo.InvariantCulture);

        _builder.AddVertexNormal(i, j, k);
    }

    //Adds native indexes
    private void FaceProducer(string[] words)
    {
        if (words[0] != DotObjLexems.Face)
        {
            throw new ArgumentOutOfRangeException(message: $"Unappropriate word set in {nameof(FaceProducer)}", paramName: nameof(words));
        }

        if (words.Length < 4)
        {
            throw new ArgumentOutOfRangeException(message: $"Unappropriate number of arguments in {DotObjLexems.Face} element", paramName: nameof(words));
        }

        List<(int, int?, int?)> face = new (words.Length - 1);

        foreach (string element in words.Skip(1))
        {
            string[] components = element.Split('/');

            int indexV = int.Parse(components[0]);
            int? indexVt = null;
            int? indexVn = null;

            switch (components.Length)
            {
                case 2:
                    indexVt = int.Parse(components[1]);
                    break;

                case 3:
                    indexVt = components[1] == string.Empty ? null : int.Parse(components[1]);
                    indexVn = int.Parse(components[2]);
                    break;
            }

            face.Add(new(indexV, indexVt, indexVn));
        }

        _builder.AddFace(face.ToArray());
    }
}
