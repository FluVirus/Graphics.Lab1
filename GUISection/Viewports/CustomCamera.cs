using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GUISection.Viewports;

internal class CustomCamera
{
    public float ZNear {  get; set; }
    public float ZFar { get; set; }
    public Vector3 WorldPosition { get; set; }

    public Vector3 Up { get; set; }

    public Vector3 Target { get; set; }
}
