using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace klekovkinLab1
{
    class SpeedVect
    {
        private float linSpeed = 0;
        private float angSpeed = 0;

        public SpeedVect()
        {
            this.linSpeed = 0;
            this.angSpeed = 0;
        }

        public SpeedVect(float _linSpeed, float _angSpeed)
        {
            this.linSpeed = _linSpeed;
            this.angSpeed = _angSpeed;
        }

        public SpeedVect(Vector2 _vec)
        {
            linSpeed = _vec.X;
            angSpeed = _vec.Y;
        }

        public SpeedVect(SpeedVect _vec)
        {
            this.linSpeed = _vec.LinSpeed;
            this.angSpeed = _vec.AngSpeed;
        }

        public float LinSpeed
        {
            set => linSpeed = value;
            get => linSpeed;
        }

        public float AngSpeed
        {
            set => angSpeed = value;
            get => angSpeed;
        }

        
        public void setLinSpeed(float _linSpeed, float _angSpeed)
        {
            this.linSpeed = _linSpeed;
            this.angSpeed = _angSpeed;
        }

        public Vector2 GetVector2()
        {
            return new Vector2(linSpeed, angSpeed);
        }

        public void setFromVec2(Vector2 _vec) {
            this.linSpeed = _vec.X;
            this.angSpeed = _vec.Y;
        }
        
    }
}

public static class Vector2Ext
{
    public static PointF toPointF(this Vector2 _vec)
    {
        return new PointF(_vec.X, _vec.Y);
    }

    public static Point toPoint(this Vector2 _vec)
    {
        return new Point((int)_vec.X, (int)_vec.Y);
    }
}

//public static class PointExt
//{
//    public static Vector2 toVector2(this Point _pt)
//    {
//        return new Vector2(_pt.X, _pt.Y);
//    }

//    public static Vector2 toVector2(this PointF _pt)
//    {
//        return new Vector2(_pt.X, _pt.Y);
//    }
//}
