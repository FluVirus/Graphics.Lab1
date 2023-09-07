using System.Collections.Immutable;

namespace DotObjTools.Consts;

internal static class DotObjLexems
{
    //-----------------Vertex data---------------------
    public const string GeometricVertex = "v";
    public const string TextureVertex = "vt";
    public const string VertexNormal = "vn";
    public const string ParameterSpaceVertex = "vp";

    //-------------Free-Form attributes----------------
    public const string CurveOrSurfaceTypes = "cstype";
    public const string Degree = "deg";
    public const string BasicMatrix = "bmat";
    public const string StepSize = "step";

    //-------------------Elements----------------------
    public const string Point = "p";
    public const string Line = "l";
    public const string Face = "f";
    public const string Curve = "curv";
    public const string Curve2D = "curv2";
    public const string Surface = "surf";

    //-----Free-form curve/surface body statements-----
    public const string ParameterValues = "parm";
    public const string OuterTrimmingLoop = "trim";
    public const string InnerTrimmingLoop = "hole";
    public const string SpecialCurve = "scrv";
    public const string SpecialPoint = "sp";
    public const string EndStatement = "end";

    //-----Connectivity between free-form surfaces-----
    public const string Connect = "con";

    //--------------------Grouping---------------------
    public const string GroupName = "g";
    public const string SmoothingGroup = "s";
    public const string MergingGroup = "mg";
    public const string ObjectName = "o";

    //------------Display/render attributes------------
    public const string BevelInterpolation = "bevel";
    public const string ColorInterpolation = "c_interp";
    public const string DissolveInterpolation = "d_interp";
    public const string LevelOfDetail = "lod";
    public const string MaterialName = "usemtl";
    public const string MaterialLibrary = "mtllib";
    public const string ShadowCasting = "shadow_obj";
    public const string RayTracing = "trace_obj";
    public const string CurveApproximationTechnique = "ctech";
    public const string SurfaceApproximationTechnique = "stech";

    //-------------------------------------------------
    public const string LineComment = "#";
    public const string LineContinualtion = "\\";

    //-------------------------------------------------

    public static readonly ImmutableList<string> KeyWords = new List<string>()
    {
        GeometricVertex,
        TextureVertex,
        VertexNormal,
        ParameterSpaceVertex,
        CurveOrSurfaceTypes,
        Degree,
        BasicMatrix,
        StepSize,
        Point,
        Line,
        Face,
        Curve,
        Curve2D,
        Surface,
        ParameterValues,
        OuterTrimmingLoop,
        InnerTrimmingLoop,
        SpecialCurve,
        SpecialPoint,
        EndStatement,
        Connect,
        GroupName,
        SmoothingGroup,
        MergingGroup,
        ObjectName,
        BevelInterpolation,
        ColorInterpolation,
        DissolveInterpolation,
        LevelOfDetail,
        MaterialName,
        MaterialLibrary,
        ShadowCasting,
        RayTracing,
        CurveApproximationTechnique,
        SurfaceApproximationTechnique
    }.ToImmutableList();

    public static readonly ImmutableList<string> All = KeyWords.AddRange(new string[] { LineComment, LineContinualtion });
}
