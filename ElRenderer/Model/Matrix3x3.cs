namespace ElRenderer.Model
{
    public struct Matrix3x3
    {
        private float[,] _m;

        public Matrix3x3(float m00, float m01, float m02,
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

        public Matrix3x3(float[,] rawData)
        {
            this._m = rawData;
        }

        public Vector3 this[int i]
        {
            get
            {
                return new Vector3(_m[i, 0], _m[i, 1], _m[i, 2]);
            }

            set
            {
                _m[i, 0] = value.x;
                _m[i, 1] = value.y;
                _m[i, 2] = value.z;
            }
        }

        public Vector3[] Columns
        {
            get
            {
                return new Vector3[3] { new Vector3(_m[0,0], _m[1,0], _m[2,0]),
                                        new Vector3(_m[0,1], _m[1,1], _m[2,1]),
                                        new Vector3(_m[0,2], _m[1,2], _m[2,2])
                                      };
            }
        }

        public Vector3 mul(Vector3 v)
        {
            Vector3[] columns = this.Columns;
            return new Vector3(v.dot(columns[0]), v.dot(columns[1]), v.dot(columns[2]));
        }

        public Matrix3x3 transpose()
        {
            float[,] result = new float[3, 3];

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    result[i, j] = _m[j, i];

            return new Matrix3x3(result);
        }

        public override string ToString()
        {
            return string.Format("{0}\\n{1}\\n{2}", this[0], this[1], this[2]);
        }
    }
}
