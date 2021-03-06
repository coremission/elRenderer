﻿using System;

namespace ElRenderer.Algebraic
{
    public struct Float2
    {
        public float x;
        public float y;

        public float getLength()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public Float2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Float2 operator -(Float2 a, Float2 b)
        {
            return new Float2(a.x - b.x, a.y - b.y);
        }

        public static Float2 operator +(Float2 a, Float2 b)
        {
            return new Float2(a.x + b.x, a.y + b.y);
        }

        public static Float2 operator *(Float2 a, float scalar)
        {
            return new Float2(a.x * scalar, a.y * scalar);
        }

        public static Float2 operator *(float scalar, Float2 a)
        {
            return a * scalar;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }

        public static Float2 lerp(Float2 start, Float2 end, float delta)
        {
            return start + (end - start) * delta;
        }
    }
}
