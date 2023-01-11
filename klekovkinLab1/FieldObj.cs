using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace klekovkinLab1
{
    internal class FieldObj:ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }


        public enum Shapes : int
        {
            NONE,
            ROUND,
            TRIANGLGE,
            SQUARE,
            HEXAGON            
        }

        protected PointF center = new PointF(0 , 0);
        protected SpeedVect speedVect = new SpeedVect(0, 0);
        protected float alpha = 0;
        protected Shapes shape = Shapes.NONE;
        protected bool movable = false;
        protected float rad = 0;

        public FieldObj() 
        {
        }

        public FieldObj(FieldObj obj)
        {
            this.Center = obj.Center;
            this.speedVect = obj.speedVect;
            this.alpha = obj.alpha;
            this.shape = obj.shape;
            this.movable = obj.movable;
            this.rad = obj.rad;            
        }

        //public FieldObj() { }
        public FieldObj(PointF _center, float _alpha, Shapes _shape, float _rad, bool _movable)
        {
            center = _center;
            alpha = _alpha;
            shape = _shape;
            rad = _rad;
            movable = _movable;
        }

        public FieldObj(float _x, float _y, float _alpha, Shapes _shape, float _rad, bool _movable)
        {
            center.X = _x;
            center.Y = _y;
            alpha = _alpha;
            shape = _shape;
            rad = _rad;
            movable = _movable;
        }

        public PointF Center { get => center; set => center = value; }
        public SpeedVect Speed { get => speedVect; set => speedVect = value; }
        public float Alpha { get => alpha; set => alpha = value; }
        public Shapes Shape { get => shape; }
        public float Rad { get => rad; }

        public bool IsMovable() { return movable; }
        public void MoveTo(PointF _center, float _alpha)
        {
            center = _center;
            alpha = _alpha;
        }
        public void MoveTo(float _x, float _y, float _alpha)
        {
            center.X = _x;
            center.Y = _y;
            alpha = _alpha;
        }
        public void ShiftTo(float _x, float _y, float _alpha)
        {
            center.X += _x;
            center.Y += _y;
            alpha += _alpha;
        }


    }
}
