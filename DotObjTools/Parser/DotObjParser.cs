using DotObjTools.Consts;
using DotObjTools.DOM;
using DotObjTools.Exceptions;
using DotObjTools.Extensions;
using System.Collections.Immutable;
using System.Text;

namespace DotObjTools.Parser;

public partial class DotObjParser : IDotObjParser
{
    private readonly IReadOnlyDictionary<string, Action<string[]>> _oneLineProducers;

    //Since DotObjParser will not parse all the content properly, but in order not to throw on correct .obj
    private readonly string[] _toPass;

    public DotObjParser()
    { 
        _oneLineProducers = new Dictionary<string, Action<string[]>>()
        {
            { DotObjLexems.GeometricVertex, GeometricVertexProducer },
            { DotObjLexems.TextureVertex, TextureVertexProducer },
            { DotObjLexems.VertexNormal, VertexNormalProducer },
            { DotObjLexems.Face, FaceProducer }
        }.ToImmutableDictionary();

        _toPass = DotObjLexems.KeyWords.Except(_oneLineProducers.Keys).ToArray();
    }

    private string? _line;
    private uint _physicalLineNumber = 0;
    private DotObjDOMBuilder _builder = new DotObjDOMBuilder();

    public DotObjDOM Parse(Stream stream, bool leaveOpen = true)
    {

        using StreamReader textReader = new StreamReader(stream, leaveOpen: leaveOpen);

        while ((_line = textReader.readTrimmedLogicalLine(DotObjLexems.LineContinualtion, ref _physicalLineNumber)) is not null)
        {
            if (_line.StartsWith(DotObjLexems.LineComment) || _line == string.Empty)
            {
                continue;
            }

            string[] words = _line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (_toPass.Contains(words[0]))
            {
                continue;
            }

            bool somethingFound = false;

            if (_oneLineProducers.TryGetValue(words[0], out Action<string[]>? producer))
            {
                somethingFound = true;
                try 
                {
                    producer(words);
                }
                catch (ArgumentException e)
                {
                    throw new InvalidDotObjException(message: e.Message, line: _physicalLineNumber); 
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidDotObjException(message: e.Message, line: _physicalLineNumber);
                }
            }
            
            if (!somethingFound)
            {
                throw new InvalidDotObjException(message: "Unknown lexem", line: _physicalLineNumber);
            }
        }

        return _builder.Build();
    }

    public DotObjDOM Parse(string fileName)
    {
        using FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        return Parse(fs, leaveOpen: false);
    }
}