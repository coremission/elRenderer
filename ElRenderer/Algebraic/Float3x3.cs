using System;
using static System.Math;

namespace ElRenderer.Algebraic
{
    public struct Float3x3
    {
        private float[,] _m;

        #region Constructors
        public Float3x3(float m00, float m01, float m02,
                         float m10, float m11, float m12,
                         float m20, float m21, float m22)
        {
            _m = new float[3, 3];
            _m[0, 0] = m00;
            _m[0, 1] = m01;
            _m[0, 2] = m02;
            _m[1, 0] = m10;
            _m[1, 1] = m11;
            _m[1, 2] = m12;
            _m[2, 0] = m20;
            _m[2, 1] = m21;
            _m[2, 2] = m22;
        }

        public Float3x3(float[,] rawData)
        {
            this._m = rawData;
        }

        public Float3x3(Float3[] rows)
            :this(rows[0].x, rows[0].y, rows[0].z,
                 rows[1].x, rows[1].y, rows[1].z,
                 rows[2].x, rows[2].y, rows[2].z)
        {}
        #endregion

        #region Operator overlodes
        public static Float3x3 operator *(Float3x3 M, float a)
        {
            return new Float3x3(a * M[0, 0], a * M[0, 1], a * M[0, 2],
                                a * M[1, 0], a * M[1, 1], a * M[1, 2],
                                a * M[2, 0], a * M[2, 1], a * M[2, 2]);
        }
        public static Float3x3 operator *(float a, Float3x3 M)
        {
            return M*a;
        }

        public static Float3x3 operator *(Float3x3 A, Float3x3 B)
        {
            return new Float3x3(A[0].dot(B.Columns[0]), A[0].dot(B.Columns[1]), A[0].dot(B.Columns[2]),
                                A[1].dot(B.Columns[0]), A[1].dot(B.Columns[1]), A[1].dot(B.Columns[2]),
                                A[2].dot(B.Columns[0]), A[2].dot(B.Columns[1]), A[2].dot(B.Columns[2]));
        }
        #endregion
        public Float3 this[int i]
        {
            get
            {
                return new Float3(_m[i, 0], _m[i, 1], _m[i, 2]);
            }

            set
            {
                _m[i, 0] = value.x;
                _m[i, 1] = value.y;
                _m[i, 2] = value.z;
            }
        }

        public float this[int i, int j]
        {
            get { return _m[i, j]; }
            set { _m[i, j] = value; }
        }

        public Float3[] Columns
        {
            get
            {
                return new Float3[3] { new Float3(_m[0,0], _m[1,0], _m[2,0]),
                                        new Float3(_m[0,1], _m[1,1], _m[2,1]),
                                        new Float3(_m[0,2], _m[1,2], _m[2,2])
                                      };
            }
        }

        public static Float3x3 identity
        {
            get
            {
                return new Float3x3(1, 0, 0,
                                    0, 1, 0,
                                    0, 0, 1);
            }
        }

        public static Float3x3 getRotationMatrix(float xAngle, float yAngle, float zAngle)
        {
            xAngle *= ((float)Math.PI / 180.0f);
            yAngle *= ((float)Math.PI / 180.0f);
            zAngle *= ((float)Math.PI / 180.0f);

            Float3x3 aboutX = new Float3x3(1, 0, 0,
                                            0, (float)Cos(xAngle), (float)Sin(xAngle),
                                            0, (float)(-Sin(xAngle)), (float)Cos(xAngle));
            Float3x3 aboutY = new Float3x3((float)Cos(yAngle), 0, (float)(-Sin(yAngle)),
                                           0, 1, 0,
                                           (float)Sin(yAngle), 0, (float)Cos(yAngle));
            return aboutX * aboutY;
        }

        public Float3x3 transpose()
        {
            float[,] result = new float[3, 3];

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    result[i, j] = _m[j, i];

            return new Float3x3(result);
        }

        public override string ToString()
        {
            return string.Format("{0}\n{1}\n{2}", this[0], this[1], this[2]);
        }
    }
}
