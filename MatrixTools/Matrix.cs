using System.Numerics;
using static System.MathF;

namespace MatrixTools;

public static class Matrix
{
    public static class Scale
    {
        public static Matrix4x4 FromVector(Vector3 XYZvector) => new Matrix4x4
        {
            M11 = XYZvector.X, M12 = 0,           M13 = 0,           M14 = 0,
            M21 = 0,           M22 = XYZvector.Y, M23 = 0,           M24 = 0,
            M31 = 0,           M32 = 0,           M33 = XYZvector.Z, M34 = 0,
            M41 = 0,           M42 = 0,           M43 = 0,           M44 = 1,
        };
    }

    public static class Translation
    {
        public static Matrix4x4 FromVector(Vector3 XYZvector) => new Matrix4x4 
        { 
            M11 = 1, M12 = 0, M13 = 0, M14 = XYZvector.X,
            M21 = 0, M22 = 1, M23 = 0, M24 = XYZvector.Y,
            M31 = 0, M32 = 0, M33 = 1, M34 = XYZvector.Z,
            M41 = 0, M42 = 0, M43 = 0, M44 = 1,
        };
    }

    public static class Rotation
    {
        public static class X
        {
            public static Matrix4x4 FromAngle(float f) => new Matrix4x4
            { 
                M11 = 1, M12 = 0,      M13 = 0,       M14 = 0,
                M21 = 0, M22 = Cos(f), M23 = -Sin(f), M24 = 0,
                M31 = 0, M32 = Sin(f), M33 = Cos(f),  M34 = 0,
                M41 = 0, M42 = 0,      M43 = 0,       M44 = 1,
            };
        }

        public static class Y
        {
            public static Matrix4x4 FromAngle(float f) => new Matrix4x4
            { 
                M11 = Cos(f),  M12 = 0, M13 = Sin(f), M14 = 0,
                M21 = 0,       M22 = 1, M23 = 0,      M24 = 0,
                M31 = -Sin(f), M32 = 0, M33 = Cos(f), M34 = 0,
                M41 = 0,       M42 = 0, M43 = 0,      M44 = 1,
            };
        }

        public static class Z
        {
            public static Matrix4x4 FromAngle(float f) => new Matrix4x4
            { 
                M11 = Cos(f), M12 = -Sin(f), M13 = 0, M14 = 0,
                M21 = Sin(f), M22 = Cos(f),  M23 = 0, M24 = 0,
                M31 = 0,      M32 = 0,       M33 = 1, M34 = 0,
                M41 = 0,      M42 = 0,       M43 = 0, M44 = 1,
            };
        }
    }

    public static class Projection
    {
        public static Matrix4x4 FromFOV(float aspect, float FOV, float zNear, float zFar) => new Matrix4x4
        {
                M11 = 1 / (aspect * Tan(FOV / 2)), M12 = 0,                  M13 = 0,                     M14 = 0,
                M21 = 0,                           M22 = 1 / (Tan(FOV / 2)), M23 = 0,                     M24 = 0,
                M31 = 0,                           M32 = 0,                  M33 = zFar / (zNear - zFar), M34 = (zNear * zFar) / (zNear - zFar),
                M41 = 0,                           M42 = 0,                  M43 = -1,                    M44 = 0,
        };
    }

    public static class Viewport
    {
        public static Matrix4x4 FromDimesions(float xMin, float yMin, float width, float height) => new Matrix4x4
        {
                M11 = width / 2, M12 = 0,            M13 = 0, M14 = xMin + width / 2,
                M21 = 0,         M22 = - height / 2, M23 = 0, M24 = yMin - height / 2,
                M31 = 0,         M32 = 0,            M33 = 1, M34 = 0,
                M41 = 0,         M42 = 0,            M43 = 0, M44 = 1,
        };
    }
}
