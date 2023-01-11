using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace klekovkinLab1
{
    internal class Ray:ICloneable
    {
        public const float maxLengh = 10000;
        public const int maxSamples = 50;
        private PointF startP = new Point(0, 0);
        private float alpha = 0;
        private float lengh = 0;

        public Ray() { }
        public Ray(Point startP, float alpha, float lengh)
        {
            this.startP = startP;
            this.alpha = alpha;
            this.lengh = lengh;
        }

        public Ray(float zeroOffset, float alpha, float lengh)
        {
            this.startP = (Vector2.Multiply(Vector2.Transform(new Vector2(1, 0), getRotationM(alpha)), zeroOffset)).toPointF();
            this.alpha = alpha;
            this.lengh = lengh;
        }
        public PointF StartP
        {
            get { return startP; }
            set { startP = value; }
        }


        public PointF EndP
        {
            get
            {
                return (startP.ToVector2() + Vector2.Multiply(Vector2.Transform(new Vector2(1, 0), getRotationM(alpha)), lengh)).toPointF();
            }
        }

        public float Lengh
        {
            get => lengh;

            set
            {
                if(MathF.Abs(value) <= maxLengh)
                {
                    lengh = value;
                }
                else
                {
                    lengh = maxLengh;
                }
            }
        }

        public float Alpha
        {
            get => alpha;
            set
            {
                if (value >= MathF.PI * 2)
                {
                    alpha = value - MathF.PI * 2;
                }
                else if (value <= -MathF.PI * 2)
                {
                    alpha = value + MathF.PI * 2;
                }
                else
                {
                    alpha = value;
                }
            }
        }

        private Matrix3x2 getRotationM(float alpha)
        {
            return new Matrix3x2(MathF.Cos(alpha), MathF.Sin(alpha),
                                -MathF.Sin(alpha), MathF.Cos(alpha),
                                0, 0);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
