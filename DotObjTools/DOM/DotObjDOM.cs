using DotObjTools.Entities;
using DotObjTools.Extensions;
using System.Numerics;

namespace DotObjTools.DOM;

/// <summary>
/// The class represents structure of .obj document
/// </summary>
public class DotObjDOM
{
    /// <summary>
    /// <c>GeometricVertices</c> is the list of all vertices (.obj "v") mentioned in .obj document.
    /// </summary>
    /// <remarks> 
    ///     <para>
    ///         The format of "v" is <b>x</b>, <b>y</b>, <b>z</b> [,<b>w</b>]
    ///         <list type="bullet">
    ///             <item>
    ///                 <term><b>x</b>, <b>y</b>, <b>z</b></term>
    ///                 <description><i>[Mandatory]</i> Coordinates of the vertix.</description>
    ///             </item>
    ///             <item>
    ///                 <term><b>w</b></term>
    ///                 <description><i>[Optional]</i> Weight. Default value - <b>1.0f</b>.</description>
    ///             </item>
    ///         </list>
    ///     </para>
    ///     <para>Represented as <typeparamref name="Vector4" /> (x,y,z,w).</para>
    ///     <para>Source document enumeration order is preserved.</para>
    /// </remarks>
    public readonly Vector4[] GeometricVertices;

    /// <summary>
    /// <c>TextureVertices</c> is the list of all texture vertices (.obj "vt") mentioned in .obj document.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The format of "vt" is <b>u</b> [,<b>v</b>, <b>w</b>]
    ///         <list type="bullet">
    ///             <item>
    ///                 <term><b>u</b></term>
    ///                 <description><i>[Mandatory]</i> The value for horizontal direction of the texture. 1D requires only this.</description>
    ///             </item>
    ///             <item>
    ///                 <term><b>v</b></term>
    ///                 <description><i>[Optional]</i> The value for vertical direction of the texture. 2D requires this too. Default value - <b>0.0f</b>.</description>
    ///             </item>
    ///             <item>
    ///                 <term><b>w</b></term>
    ///                 <description><i>[Optional]</i> The depth of the texture. Default value - <b>0.0f</b>.</description>
    ///             </item>
    ///         </list>
    ///     </para>
    ///     <para>Represented as <typeparamref name="Vector3" /> (u,v,w).</para>
    ///     <para>Source document enumeration order is preserved.</para>    
    /// </remarks>
    public readonly Vector3[] TextureVertices;

    /// <summary>
    /// <c>VertexNormals</c> is the list of all normal vectors for vertices (.obj "vn") mentioned in .obj document.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The format of "vn" is <b>i</b> ,<b>j</b>, <b>k</b>
    ///         <list type="bullet">
    ///             <item>
    ///                 <term><b>i</b>, <b>j</b>, <b>k</b></term>
    ///                 <description><i>[Mandatory]</i> The coordinates for the vertex normal</description>
    ///             </item>
    ///         </list>
    ///     </para>
    ///     <para>Represented as <typeparamref name="Vector3" /> (i,j,k).</para>
    ///     <para>Source document enumeration order is preserved.</para>    
    /// </remarks>
    public readonly Vector3[] VertexNormals;

    /// <summary>
    /// Only V
    /// </summary>
    public readonly FaceItem[][] Faces;

    internal DotObjDOM
    (
        List<Vector4> geometricVertices,
        List<Vector3> textureVertices,
        List<Vector3> vertexNormals,
        List<List<FaceItem>> faces
    )
    {
        GeometricVertices = geometricVertices.ToArray();
        TextureVertices = textureVertices.ToArray();
        VertexNormals = vertexNormals.ToArray();
        Faces = faces.ToTwoDimensionalArray();

    }
}
