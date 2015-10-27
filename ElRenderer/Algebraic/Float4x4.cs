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

        /// <summary>
        /// Creates 4x4 affine transform matrix from linear transform
        /// matrix 3x3
        /// </summary>
        /// <param name="lM">Linear transform matrix</param>
        public Float4x4(Float3x3 lM)
            :this(lM[0, 0], lM[0, 1], lM[0, 2], 0,
                  lM[1, 0], lM[1, 1], lM[1, 2], 0,
                  lM[2, 0], lM[2, 1], lM[2, 2], 0,
                         0,        0,        0, 1
                 )
        {}
        #endregion

        public Float4 getRow(int i)
        {
            return new Float4(_m[i, 0], _m[i, 1], _m[i, 2], _m[i, 3]);
        }

        public Float4 getColumn(int i)
        {
            return new Float4(_m[0, i], _m[1, i], _m[2, i], _m[3, i]);
        }

        public Float4 mul(Float4 v)
        {
            return new Float4(v.dot(getColumn(0)),
                              v.dot(getColumn(1)),
                              v.dot(getColumn(2)),
                              v.dot(getColumn(3))
                );
        }

        public Float3 transformPoint(Float3 p)
        {
            return this.mul(new Float4(p, 1)).xyz;
        }
    }
}
