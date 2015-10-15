using System;
using static System.Math;

namespace ElRenderer.Algebraic
{
    public struct Float4x4
    {
        private float[,] _m;

        #region Constructors
        public Float4x4(float m00, float m01, float m02, float m03,
                         float m10, float m11, float m12, float m13,
                         float m20, float m21, float m22, float m23,
                         float m30, float m31, float m32, float m33)
        {
            _m = new float[4, 4];
            _m[0, 0] = m00;
            _m[0, 1] = m01;
            _m[0, 2] = m02;
            _m[0, 3] = m03;
            _m[1, 0] = m10;
            _m[1, 1] = m11;
            _m[1, 2] = m12;
            _m[1, 3] = m13;
            _m[2, 0] = m20;
            _m[2, 1] = m21;
            _m[2, 2] = m22;
            _m[2, 3] = m23;
            _m[3, 0] = m30;
            _m[3, 1] = m31;
            _m[3, 2] = m32;
            _m[3, 3] = m33;
        }

        public Float4x4(float[,] rawData)
        {
            this._m = rawData;
        }

        public Float4x4(Float4[] rows)
            :this(rows[0].x, rows[0].y, rows[0].z, rows[0].w,
                 rows[1].x, rows[1].y, rows[1].z, rows[1].w,
                 rows[2].x, rows[2].y, rows[2].z, rows[2].w,
                 rows[3].x, rows[3].y, rows[3].z, rows[4].w)
        { }
        #endregion
    }
}
